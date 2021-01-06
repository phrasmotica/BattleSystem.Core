using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Actions;
using BattleSystem.Core.Moves.Success;

namespace BattleSystem.Core.Moves
{
    /// <summary>
    /// Represents a move that may comprise multiple actions in order.
    /// </summary>
    public class Move
    {
        /// <summary>
        /// The success calculator.
        /// </summary>
        private ISuccessCalculator _successCalculator;

        /// <summary>
        /// The actions this move will apply in order.
        /// </summary>
        private readonly IList<IAction> _moveActions;

        /// <summary>
        /// Gets or sets the name of the move.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the description of the move.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets or sets the max uses of the move.
        /// </summary>
        public int MaxUses { get; private set; }

        /// <summary>
        /// Gets or sets the remaining uses of the move.
        /// </summary>
        public int RemainingUses { get; private set; }

        /// <summary>
        /// Gets or sets the priority of the move.
        /// </summary>
        public int Priority { get; private set; }

        /// <summary>
        /// Gets or sets a summary of the move.
        /// </summary>
        public string Summary => $"{Name} ({RemainingUses}/{MaxUses} uses) - {Description}";

        /// <summary>
        /// Creates a new <see cref="Move"/> instance.
        /// </summary>
        public Move()
        {
            _moveActions = new List<IAction>();
        }

        /// <summary>
        /// Sets the name for this move.
        /// </summary>
        /// <param name="name">The name.</param>
        public void SetName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Sets the description for this move.
        /// </summary>
        /// <param name="description">The description.</param>
        public void SetDescription(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Sets the max uses for this move. Optionally ignores the value of the remaining uses.
        /// </summary>
        /// <param name="maxUses"></param>
        /// <param name="ignoreRemainingUses"></param>
        public void SetMaxUses(int maxUses, bool ignoreRemainingUses = false)
        {
            MaxUses = maxUses;

            if (!ignoreRemainingUses || RemainingUses > maxUses)
            {
                // ensure remaining uses is capped if new max uses is lower
                RemainingUses = maxUses;
            }
        }

        /// <summary>
        /// Sets the priority for this move.
        /// </summary>
        /// <param priority="priority">The priority.</param>
        public void SetPriority(int priority)
        {
            Priority = priority;
        }

        /// <summary>
        /// Sets the success calculator for this move.
        /// </summary>
        /// <param name="successCalculator">The success calculator.</param>
        public void SetSuccessCalculator(ISuccessCalculator successCalculator)
        {
            _successCalculator = successCalculator;
        }

        /// <summary>
        /// Adds the given action to the move.
        /// </summary>
        /// <param name="action">The action to add.</param>
        public void AddAction(IAction action)
        {
            _moveActions.Add(action);
        }

        /// <summary>
        /// Returns whether the move can be used.
        /// </summary>
        public bool CanUse()
        {
            return RemainingUses > 0;
        }

        /// <summary>
        /// Ensures each move action has its targets set.
        /// </summary>
        /// <param name="user">The user of the move.</param>
        /// <param name="otherCharacters">The other characters.</param>
        public void SetTargets(Character user, IEnumerable<Character> otherCharacters)
        {
            foreach (var action in _moveActions)
            {
                action.SetTargets(user, otherCharacters);
            }
        }

        /// <summary>
        /// Applies the actions of the move, if it succeeds.
        /// </summary>
        /// <param name="user">The user of the move.</param>
        /// <param name="otherCharacters">The other characters.</param>
        public (MoveUseResult, IEnumerable<ActionUseResult<Move>>) Use(Character user, IEnumerable<Character> otherCharacters)
        {
            var moveUseResult = _successCalculator.Calculate(user, this, otherCharacters);

            var actionsResults = new List<ActionUseResult<Move>>();

            var targets = otherCharacters.ToArray();

            if (moveUseResult == MoveUseResult.Success)
            {
                foreach (var action in _moveActions)
                {
                    var result = action.Use<Move>(user, targets);
                    foreach (var r in result.Results)
                    {
                        r.Source = this;
                    }

                    actionsResults.Add(result);

                    if (!result.Success)
                    {
                        // don't execute further actions if this one failed
                        break;
                    }

                    // only certain characters should be considered as targets
                    // for subsequent actions
                    targets = GetTargetsToConsider(targets, result).ToArray();
                }
            }

            RemainingUses--;

            return (moveUseResult, actionsResults);
        }

        /// <summary>
        /// Returns the characters that should be considered for subsequent actions
        /// based on the results of the action use.
        /// </summary>
        /// <param name="targets">The targets of the action use.</param>
        /// <param name="actionUseResult">The result of the action use.</param>
        private static IEnumerable<Character> GetTargetsToConsider(
            IEnumerable<Character> targets,
            ActionUseResult<Move> actionUseResult)
        {
            var targetedCharacters = actionUseResult.Results.Select(ar => ar.Target);

            var untargetedCharacters = targets.Except(targetedCharacters);

            var affectedCharacters = actionUseResult.Results.Where(ar => ar.Applied)
                                                            .Select(ar => ar.Target);

            // characters who have a) not yet been targeted or b) were affected
            // by the previous action should be considered as targets for
            // subsequent actions
            return untargetedCharacters.Union(affectedCharacters);
        }
    }
}
