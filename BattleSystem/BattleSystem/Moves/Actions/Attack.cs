using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Damage;
using BattleSystem.Moves.Targets;

namespace BattleSystem.Moves.Actions
{
    /// <summary>
    /// Represents an attacking move action.
    /// </summary>
    public class Attack : IMoveAction
    {
        /// <summary>
        /// The damage calculator.
        /// </summary>
        private IDamageCalculator _damageCalculator;

        /// <summary>
        /// The move target calculator.
        /// </summary>
        private IMoveTargetCalculator _moveTargetCalculator;

        /// <summary>
        /// Gets or sets the attack's power.
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// Creates a new <see cref="Attack"/>.
        /// </summary>
        public Attack() { }

        /// <summary>
        /// Sets the damage calculator for this attack.
        /// </summary>
        /// <param name="damageCalculator">The damage calculator.</param>
        public void SetDamageCalculator(IDamageCalculator damageCalculator)
        {
            _damageCalculator = damageCalculator;
        }

        /// <summary>
        /// Sets the move target calculator for this attack.
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
                var damage = _damageCalculator.Calculate(user, this, target);
                target.ReceiveDamage(damage);
            }

            return applied;
        }
    }
}
