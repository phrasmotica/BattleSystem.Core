using System;
using BattleSystem.Actions.Targets;
using BattleSystem.Actions.Heal.Calculators;

namespace BattleSystem.Actions.Heal
{
    /// <summary>
    /// Builder class for heals.
    /// </summary>
    public class HealActionBuilder
    {
        /// <summary>
        /// The heal to build.
        /// </summary>
        private readonly HealAction _heal;

        /// <summary>
        /// Whether the amount of the buff has been set.
        /// </summary>
        private bool _isAmountSet;

        /// <summary>
        /// Whether the healing calculator of the buff has been set.
        /// </summary>
        private bool _isHealingCalculatorSet;

        /// <summary>
        /// Whether the action target calculator of the heal has been set.
        /// </summary>
        private bool _isActionTargetCalculatorSet;

        /// <summary>
        /// Creates a new <see cref="HealActionBuilder"/> instance.
        /// </summary>
        public HealActionBuilder()
        {
            _heal = new HealAction();
        }

        /// <summary>
        /// Sets the built heal's amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public HealActionBuilder WithAmount(int amount)
        {
            _heal.Amount = amount;
            _isAmountSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built heal's healing calculator.
        /// </summary>
        /// <param name="healingCalculator">The built heal's healing calculator.</param>
        public HealActionBuilder WithHealingCalculator(IHealingCalculator actionTargetCalculator)
        {
            if (actionTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(actionTargetCalculator));
            }

            _heal.SetHealingCalculator(actionTargetCalculator);
            _isHealingCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built heal to use absolute healing.
        /// </summary>
        public HealActionBuilder AbsoluteHealing()
        {
            return WithHealingCalculator(new AbsoluteHealingCalculator());
        }

        /// <summary>
        /// Sets the built heal to use percentage healing.
        /// </summary>
        public HealActionBuilder PercentageHealing()
        {
            return WithHealingCalculator(new PercentageHealingCalculator());
        }

        /// <summary>
        /// Sets the built heal's action target calculator.
        /// </summary>
        /// <param name="name">The built heal's action target calculator.</param>
        public HealActionBuilder WithActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
        {
            if (actionTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(actionTargetCalculator));
            }

            _heal.SetActionTargetCalculator(actionTargetCalculator);
            _isActionTargetCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built buff to target all characters including the user.
        /// </summary>
        public HealActionBuilder TargetsAll()
        {
            return WithActionTargetCalculator(new AllActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target all other characters.
        /// </summary>
        public HealActionBuilder TargetsOthers()
        {
            return WithActionTargetCalculator(new OthersActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target all enemies.
        /// </summary>
        public HealActionBuilder TargetsEnemies()
        {
            return WithActionTargetCalculator(new EnemiesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target all characters on the user's team.
        /// </summary>
        public HealActionBuilder TargetsTeam()
        {
            return WithActionTargetCalculator(new TeamActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target all other characters on the user's team.
        /// </summary>
        public HealActionBuilder TargetsAllies()
        {
            return WithActionTargetCalculator(new AlliesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target the user.
        /// </summary>
        public HealActionBuilder TargetsUser()
        {
            return WithActionTargetCalculator(new UserActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target a random enemy.
        /// </summary>
        public HealActionBuilder TargetsRandomEnemy()
        {
            return WithActionTargetCalculator(new RandomEnemyActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target a random ally.
        /// </summary>
        public HealActionBuilder TargetsRandomAlly()
        {
            return WithActionTargetCalculator(new RandomAllyActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target the first enemy.
        /// </summary>
        public HealActionBuilder TargetsFirstEnemy()
        {
            return WithActionTargetCalculator(new FirstAllyActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target the first ally.
        /// </summary>
        public HealActionBuilder TargetsFirstAlly()
        {
            return WithActionTargetCalculator(new FirstAllyActionTargetCalculator());
        }

        /// <summary>
        /// Returns the built heal.
        /// </summary>
        public HealAction Build()
        {
            if (!_isAmountSet)
            {
                throw new InvalidOperationException("Cannot build a heal with no amount set!");
            }

            if (!_isHealingCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a heal with no healing calculator!");
            }

            if (!_isActionTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a heal with no action target calculator!");
            }

            return _heal;
        }
    }
}
