using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Moves.Actions.Results;
using BattleSystem.Moves.Targets;

namespace BattleSystem.Moves.Actions
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
        /// Creates a new <see cref="Protect"/>.
        /// </summary>
        public Protect() { }

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

            foreach (var target in targets.Where(c => !c.IsDead).ToArray())
            {
                var result = target.Protect();
                results.Add(result);
            }

            return results;
        }
    }
}
