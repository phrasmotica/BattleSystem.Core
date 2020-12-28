using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Actions.Results;
using BattleSystem.Moves.Targets;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Represents a protecting move action, which nullifies all damage dealt by an attack.
    /// </summary>
    public class Protect : IMoveAction
    {
        /// <summary>
        /// The move target calculator.
        /// </summary>
        private IMoveTargetCalculator _moveTargetCalculator;

        /// <summary>
        /// Sets the move target calculator for this protect action.
        /// </summary>
        /// <param name="moveTargetCalculator">The move target calculator.</param>
        public void SetMoveTargetCalculator(IMoveTargetCalculator moveTargetCalculator)
        {
            _moveTargetCalculator = moveTargetCalculator;
        }

        /// <inheritdoc />
        public virtual IEnumerable<IMoveActionResult> Use(Character user, IEnumerable<Character> otherCharacters)
        {
            var targets = _moveTargetCalculator.Calculate(user, otherCharacters);

            var results = new List<IMoveActionResult>();

            foreach (var target in targets.Where(c => !c.IsDead))
            {
                var result = target.AddProtect(user.Id);
                results.Add(result);
            }

            return results;
        }
    }
}
