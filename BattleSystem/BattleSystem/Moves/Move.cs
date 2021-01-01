using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Actions;
using BattleSystem.Actions.Results;
using BattleSystem.Moves.Success;

namespace BattleSystem.Moves
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
        /// Applies the effects of the move, if it lands.
        /// </summary>
        /// <param name="user">The user of the move.</param>
        /// <param name="otherCharacters">The other characters.</param>
        public (MoveUseResult, IEnumerable<IEnumerable<IActionResult<Move>>>) Use(Character user, IEnumerable<Character> otherCharacters)
        {
            var result = _successCalculator.Calculate(user, this, otherCharacters);

            var actionsResults = new List<IEnumerable<IActionResult<Move>>>();

            var targets = otherCharacters.ToArray();

            if (result == MoveUseResult.Success)
            {
                foreach (var action in _moveActions)
                {
                    if (user.HasItem)
                    {
                        (action as ITransformable)?.ReceiveTransforms(user.Item);
                    }

                    var actionResults = action.Use<Move>(user, targets);
                    foreach (var r in actionResults)
                    {
                        r.Source = this;
                    }

                    actionsResults.Add(actionResults);

                    // ensure targets can't be affected by subsequent actions
                    // if they weren't affected by the previous one
                    var affectedCharacters = actionResults.Where(ar => ar.Applied)
                                                          .Select(ar => ar.TargetId);
                    targets = targets.Where(t => affectedCharacters.Contains(t.Id)).ToArray();

                    (action as ITransformable)?.ClearTransforms();
                }
            }

            RemainingUses--;

            return (result, actionsResults);
        }
    }
}
