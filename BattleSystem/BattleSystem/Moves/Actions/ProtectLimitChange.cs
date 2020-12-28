using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Moves.Actions.Results;
using BattleSystem.Moves.Targets;

namespace BattleSystem.Moves.Actions
{
    /// <summary>
    /// Represents a move action that changes the protect limit of the target character.
    /// </summary>
    public class ProtectLimitChange : IMoveAction
    {
        /// <summary>
        /// The move target calculator.
        /// </summary>
        private IMoveTargetCalculator _moveTargetCalculator;

        /// <summary>
        /// Gets or sets the amount to change the target's protect limit by.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Sets the move target calculator for this protect limit change.
        /// </summary>
        /// <param name="moveTargetCalculator">The move target calculator.</param>
        public void SetMoveTargetCalculator(IMoveTargetCalculator moveTargetCalculator)
        {
            _moveTargetCalculator = moveTargetCalculator;
        }

        /// <inheritdoc />
        public IEnumerable<IMoveActionResult> Use(Character user, IEnumerable<Character> otherCharacters)
        {
            var targets = _moveTargetCalculator.Calculate(user, otherCharacters);

            var results = new List<IMoveActionResult>();

            foreach (var target in targets.Where(c => !c.IsDead))
            {
                var result = target.ChangeProtectLimit(Amount, user.Id);
                results.Add(result);
            }

            return results;
        }
    }
}
