using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Actions;
using BattleSystem.Core.Success;

namespace BattleSystem.Core.Moves
{
    /// <summary>
    /// Represents a move that may comprise multiple actions in order.
    /// </summary>
    public class Move
    {
        /// <summary>
        /// Delegate for a function that creates a move success calculator.
        /// </summary>
        public delegate ISuccessCalculator<Move, MoveUseResult> MoveSuccessCalculatorFactory();

        /// <summary>
        /// The actions this move will apply in order.
        /// </summary>
        private readonly IList<IAction> _moveActions;

        /// <summary>
        /// Creates a new <see cref="Move"/> instance.
        /// </summary>
        public Move()
        {
            _moveActions = new List<IAction>();
        }

        /// <summary>
        /// The factory for constructing a success calculator.
        /// </summary>
        public MoveSuccessCalculatorFactory SuccessCalculatorFactory { get; set; }

        /// <summary>
        /// Gets or sets the name of the move.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the move.
        /// </summary>
        public string Description { get; set; }

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
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets a summary of the move.
        /// </summary>
        public string Summary => $"{Name} ({RemainingUses}/{MaxUses} uses) - {Description}";

        /// <summary>
        /// Gets whether the move can be used.
        /// </summary>
        public bool CanUse => RemainingUses > 0;

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
        /// Adds the given action to the move.
        /// </summary>
        /// <param name="action">The action to add.</param>
        public void AddAction(IAction action)
        {
            _moveActions.Add(action);
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
            var moveUseResult = SuccessCalculatorFactory().Calculate(this);

            if (user.WillFlinch)
            {
                moveUseResult = MoveUseResult.Flinched;
                user.WillFlinch = false;
            }

            var actionsResults = new List<ActionUseResult<Move>>();

            if (moveUseResult == MoveUseResult.Success)
            {
                var targets = otherCharacters.ToArray();

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
