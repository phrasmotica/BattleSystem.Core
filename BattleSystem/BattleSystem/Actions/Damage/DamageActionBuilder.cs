using System;
using BattleSystem.Actions.Damage.Calculators;
using BattleSystem.Actions.Targets;

namespace BattleSystem.Actions.Damage
{
    /// <summary>
    /// Builder class for damage actions.
    /// </summary>
    public class DamageActionBuilder
    {
        /// <summary>
        /// The damage action to build.
        /// </summary>
        private readonly DamageAction _damage;

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
        /// Creates a new <see cref="DamageActionBuilder"/> instance.
        /// </summary>
        public DamageActionBuilder()
        {
            _damage = new DamageAction();
        }

        /// <summary>
        /// Sets the built damage action's power.
        /// </summary>
        /// <param name="power">The power.</param>
        public DamageActionBuilder WithPower(int power)
        {
            _damage.Power = power;
            _isPowerSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built damage action's damage calculator.
        /// </summary>
        /// <param name="damageCalculator">The built damage action's damage calculator.</param>
        public DamageActionBuilder WithDamageCalculator(IDamageCalculator damageCalculator)
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
        public DamageActionBuilder AbsoluteDamage()
        {
            return WithDamageCalculator(new AbsoluteDamageCalculator());
        }

        /// <summary>
        /// Sets the built damage action to use percentage damage.
        /// </summary>
        public DamageActionBuilder PercentageDamage()
        {
            return WithDamageCalculator(new PercentageDamageCalculator());
        }

        /// <summary>
        /// Sets the built damage action to use damage based on the user and target's stat difference.
        /// </summary>
        public DamageActionBuilder StatDifferenceDamage()
        {
            return WithDamageCalculator(new StatDifferenceDamageCalculator());
        }

        /// <summary>
        /// Sets the built damage action's action target calculator.
        /// </summary>
        /// <param name="name">The built damage action's action target calculator.</param>
        public DamageActionBuilder WithActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
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
        public DamageActionBuilder TargetsAll()
        {
            return WithActionTargetCalculator(new AllActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target all other characters.
        /// </summary>
        public DamageActionBuilder TargetsOthers()
        {
            return WithActionTargetCalculator(new OthersActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target all enemies.
        /// </summary>
        public DamageActionBuilder TargetsEnemies()
        {
            return WithActionTargetCalculator(new EnemiesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target all characters on the user's team.
        /// </summary>
        public DamageActionBuilder TargetsTeam()
        {
            return WithActionTargetCalculator(new TeamActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target all other characters on the user's team.
        /// </summary>
        public DamageActionBuilder TargetsAllies()
        {
            return WithActionTargetCalculator(new AlliesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target the user.
        /// </summary>
        public DamageActionBuilder TargetsUser()
        {
            return WithActionTargetCalculator(new UserActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target the first enemy.
        /// </summary>
        public DamageActionBuilder TargetsFirstEnemy()
        {
            return WithActionTargetCalculator(new FirstEnemyActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built damage action to target the first ally.
        /// </summary>
        public DamageActionBuilder TargetsFirstAlly()
        {
            return WithActionTargetCalculator(new FirstAllyActionTargetCalculator());
        }

        /// <summary>
        /// Adds the given tag to the damage action.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public DamageActionBuilder WithTag(string tag)
        {
            _damage.Tags.Add(tag);
            return this;
        }

        /// <summary>
        /// Returns the built damage action.
        /// </summary>
        public DamageAction Build()
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
