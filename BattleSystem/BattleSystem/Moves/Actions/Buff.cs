using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Moves.Targets;
using BattleSystem.Stats;

namespace BattleSystem.Moves.Actions
{
    /// <summary>
    /// Represents a buffing move action.
    /// </summary>
    public class Buff : IMoveAction
    {
        /// <summary>
        /// The move target calculator.
        /// </summary>
        private IMoveTargetCalculator _moveTargetCalculator;

        /// <summary>
        /// Gets or sets the buff's stat multipliers for the target.
        /// </summary>
        public IDictionary<StatCategory, double> TargetMultipliers { get; private set; }

        /// <summary>
        /// Creates a new <see cref="Buff"/>.
        /// </summary>
        public Buff()
        {
            TargetMultipliers = new Dictionary<StatCategory, double>();
        }

        /// <summary>
        /// Sets the move target calculator for this buff.
        /// </summary>
        /// <param name="moveTargetCalculator">The move target calculator.</param>
        public void SetMoveTargetCalculator(IMoveTargetCalculator moveTargetCalculator)
        {
            _moveTargetCalculator = moveTargetCalculator;
        }

        /// <inheritdoc />
        public virtual bool Use(Character user, IEnumerable<Character> otherCharacters)
        {
            var targets = _moveTargetCalculator.Calculate(user, otherCharacters);
            var applied = false;

            foreach (var target in targets.Where(c => !c.IsDead).ToArray())
            {
                applied = true;
                target.ReceiveBuff(TargetMultipliers);
            }

            return applied;
        }
    }
}
