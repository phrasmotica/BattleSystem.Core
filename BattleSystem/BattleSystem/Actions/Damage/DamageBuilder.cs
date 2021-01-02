using System;
using BattleSystem.Actions.Damage.Calulators;
using BattleSystem.Actions.Targets;

namespace BattleSystem.Actions.Damage
{
    /// <summary>
    /// Builder class for damage actions.
    /// </summary>
    public class DamageBuilder
    {
        /// <summary>
        /// The damage action to build.
        /// </summary>
        private readonly Damage _damage;

        /// <summary>
        /// Whether the power of the damage action has been set.
        /// </summary>
        private bool _isPowerSet;

        /// <summary>
        /// Whether the damage calculator of the damage action has been set.
        /// </summary>
        private bool _isDamageCalculatorSet;

        /// <summary>
        /// Whether the action target calculator of the damage action has been set.
        /// </summary>
        private bool _isActionTargetCalculatorSet;

        /// <summary>
        /// Creates a new <see cref="DamageBuilder"/> instance.
        /// </summary>
        public DamageBuilder()
        {
            _damage = new Damage();
        }

        /// <summary>
        /// Sets the built damage action's power.
        /// </summary>
        /// <param name="power">The power.</param>
        public DamageBuilder WithPower(int power)
        {
            _damage.Power = power;
            _isPowerSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built damage action's damage calculator.
        /// </summary>
        /// <param name="damageCalculator">The built damage action's damage calculator.</param>
        public DamageBuilder WithDamageCalculator(IDamageCalculator damageCalculator)
        {
            if (damageCalculator is null)
            {
                throw new ArgumentNullException(nameof(damageCalculator));
            }

            _damage.SetDamageCalculator(damageCalculator);
            _isDamageCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built damage action to use absolute damage.
        /// </summary>
        public DamageBuilder AbsoluteDamage()
        {
            return WithDamageCalculator(new AbsoluteDamageCalculator());
        }

        /// <summary>
        /// Sets the built damage action to use percentage damage.
        /// </summary>
        public DamageBuilder PercentageDamage()
        {
            return WithDamageCalculator(new PercentageDamageCalculator());
        }

        /// <summary>
        /// Sets the built damage action to use damage based on the user and target's stat difference.
        /// </summary>
        public DamageBuilder StatDifferenceDamage()
        {
            return WithDamageCalculator(new StatDifferenceDamageCalculator());
        }

        /// <summary>
        /// Sets the built damage action's action target calculator.
        /// </summary>
        /// <param name="name">The built damage action's action target calculator.</param>
        public DamageBuilder WithActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
        {
            if (actionTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(actionTargetCalculator));
            }

            _damage.SetActionTargetCalculator(actionTargetCalculator);
            _isActionTargetCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built damage action to target all characters including the user.
        /// </summary>
        public DamageBuilder TargetsAll()
        {
            return WithActionTargetCalculator(new AllActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target all other characters.
        /// </summary>
        public DamageBuilder TargetsOthers()
        {
            return WithActionTargetCalculator(new OthersActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target all enemies.
        /// </summary>
        public DamageBuilder TargetsEnemies()
        {
            return WithActionTargetCalculator(new EnemiesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target all characters on the user's team.
        /// </summary>
        public DamageBuilder TargetsTeam()
        {
            return WithActionTargetCalculator(new TeamActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target all other characters on the user's team.
        /// </summary>
        public DamageBuilder TargetsAllies()
        {
            return WithActionTargetCalculator(new AlliesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target the user.
        /// </summary>
        public DamageBuilder TargetsUser()
        {
            return WithActionTargetCalculator(new UserActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target the first enemy.
        /// </summary>
        public DamageBuilder TargetsFirstEnemy()
        {
            return WithActionTargetCalculator(new FirstEnemyActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target the first ally.
        /// </summary>
        public DamageBuilder TargetsFirstAlly()
        {
            return WithActionTargetCalculator(new FirstAllyActionTargetCalculator());
        }

        /// <summary>
        /// Returns the built damage action.
        /// </summary>
        public Damage Build()
        {
            if (!_isPowerSet)
            {
                throw new InvalidOperationException("Cannot build a damage action with no power set!");
            }

            if (!_isDamageCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a damage action with no damage calculator!");
            }

            if (!_isActionTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a damage action with no action target calculator!");
            }

            return _damage;
        }
    }
}
