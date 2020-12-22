using System;
using BattleSystem.Damage;
using BattleSystem.Moves.Targets;

namespace BattleSystem.Moves.Actions
{
    /// <summary>
    /// Builder class for attacks.
    /// </summary>
    public class AttackBuilder
    {
        /// <summary>
        /// The attack to build.
        /// </summary>
        private readonly Attack _attack;

        /// <summary>
        /// Whether the power of the attack has been set.
        /// </summary>
        private bool _isPowerSet;

        /// <summary>
        /// Whether the damage calculator of the attack has been set.
        /// </summary>
        private bool _isDamageCalculatorSet;

        /// <summary>
        /// Whether the move target calculator of the attack has been set.
        /// </summary>
        private bool _isMoveTargetCalculatorSet;

        /// <summary>
        /// Creates a new <see cref="AttackBuilder"/> instance.
        /// </summary>
        public AttackBuilder()
        {
            _attack = new Attack();
        }

        /// <summary>
        /// Sets the built attack's power.
        /// </summary>
        /// <param name="power">The power.</param>
        public AttackBuilder WithPower(int power)
        {
            _attack.Power = power;
            _isPowerSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built attack's damage calculator.
        /// </summary>
        /// <param name="damageCalculator">The built attack's damage calculator.</param>
        public AttackBuilder WithDamageCalculator(IDamageCalculator damageCalculator)
        {
            if (damageCalculator is null)
            {
                throw new ArgumentNullException(nameof(damageCalculator));
            }

            _attack.SetDamageCalculator(damageCalculator);
            _isDamageCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built attack to use absolute damage.
        /// </summary>
        public AttackBuilder AbsoluteDamage()
        {
            return WithDamageCalculator(new AbsoluteDamageCalculator());
        }

        /// <summary>
        /// Sets the built attack to use percentage damage.
        /// </summary>
        public AttackBuilder PercentageDamage()
        {
            return WithDamageCalculator(new PercentageDamageCalculator());
        }

        /// <summary>
        /// Sets the built attack to use damage based on the user and target's stat difference.
        /// </summary>
        public AttackBuilder StatDifferenceDamage()
        {
            return WithDamageCalculator(new StatDifferenceDamageCalculator());
        }

        /// <summary>
        /// Sets the built attack's move target calculator.
        /// </summary>
        /// <param name="name">The built attack's move target calculator.</param>
        public AttackBuilder WithMoveTargetCalculator(IMoveTargetCalculator moveTargetCalculator)
        {
            if (moveTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(moveTargetCalculator));
            }

            _attack.SetMoveTargetCalculator(moveTargetCalculator);
            _isMoveTargetCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built attack to target all characters including the user.
        /// </summary>
        public AttackBuilder TargetsAll()
        {
            return WithMoveTargetCalculator(new AllMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built attack to target all other characters.
        /// </summary>
        public AttackBuilder TargetsOthers()
        {
            return WithMoveTargetCalculator(new OthersMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built attack to target the first enemy.
        /// </summary>
        public AttackBuilder TargetsFirstEnemy()
        {
            return WithMoveTargetCalculator(new FirstEnemyMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built attack to target the user.
        /// </summary>
        public AttackBuilder TargetsUser()
        {
            return WithMoveTargetCalculator(new UserMoveTargetCalculator());
        }

        /// <summary>
        /// Returns the built attack.
        /// </summary>
        public Attack Build()
        {
            if (!_isPowerSet)
            {
                throw new InvalidOperationException("Cannot build an attack with no power set!");
            }

            if (!_isDamageCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build an attack with no damage calculator!");
            }

            if (!_isMoveTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build an attack with no move target calculator!");
            }

            return _attack;
        }
    }
}
