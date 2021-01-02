using System;
using BattleSystem.Damage;
using BattleSystem.Actions.Targets;

namespace BattleSystem.Actions
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
        /// Whether the action target calculator of the attack has been set.
        /// </summary>
        private bool _isactionTargetCalculatorSet;

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
        /// Sets the built attack's action target calculator.
        /// </summary>
        /// <param name="name">The built attack's action target calculator.</param>
        public AttackBuilder WithActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
        {
            if (actionTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(actionTargetCalculator));
            }

            _attack.SetActionTargetCalculator(actionTargetCalculator);
            _isactionTargetCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built attack to target all characters including the user.
        /// </summary>
        public AttackBuilder TargetsAll()
        {
            return WithActionTargetCalculator(new AllActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built attack to target all other characters.
        /// </summary>
        public AttackBuilder TargetsOthers()
        {
            return WithActionTargetCalculator(new OthersActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built attack to target all enemies.
        /// </summary>
        public AttackBuilder TargetsEnemies()
        {
            return WithActionTargetCalculator(new EnemiesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built attack to target all characters on the user's team.
        /// </summary>
        public AttackBuilder TargetsTeam()
        {
            return WithActionTargetCalculator(new TeamActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built attack to target all other characters on the user's team.
        /// </summary>
        public AttackBuilder TargetsAllies()
        {
            return WithActionTargetCalculator(new AlliesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built attack to target the user.
        /// </summary>
        public AttackBuilder TargetsUser()
        {
            return WithActionTargetCalculator(new UserActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built attack to target the first enemy.
        /// </summary>
        public AttackBuilder TargetsFirstEnemy()
        {
            return WithActionTargetCalculator(new FirstEnemyActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built attack to target the first ally.
        /// </summary>
        public AttackBuilder TargetsFirstAlly()
        {
            return WithActionTargetCalculator(new FirstAllyActionTargetCalculator());
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

            if (!_isactionTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build an attack with no action target calculator!");
            }

            return _attack;
        }
    }
}
