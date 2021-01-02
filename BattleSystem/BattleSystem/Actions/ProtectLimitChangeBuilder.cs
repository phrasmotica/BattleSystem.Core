using System;
using BattleSystem.Actions.Targets;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Builder class for protect limit change actions.
    /// </summary>
    public class ProtectLimitChangeBuilder
    {
        /// <summary>
        /// The protect limit change action to build.
        /// </summary>
        private readonly ProtectLimitChange _protectLimitChange;

        /// <summary>
        /// Whether the action target calculator of the protect limit change action has been set.
        /// </summary>
        private bool _isActionTargetCalculatorSet;

        /// <summary>
        /// Whether the amount of the protect limit change action has been set.
        /// </summary>
        private bool _isAmountSet;

        /// <summary>
        /// Creates a new <see cref="ProtectLimitChangeBuilder"/> instance.
        /// </summary>
        public ProtectLimitChangeBuilder()
        {
            _protectLimitChange = new ProtectLimitChange();
        }

        /// <summary>
        /// Sets the built protect limit change action's action target calculator.
        /// </summary>
        /// <param name="name">The built protect limit change action's action target calculator.</param>
        public ProtectLimitChangeBuilder WithActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
        {
            if (actionTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(actionTargetCalculator));
            }

            _protectLimitChange.SetActionTargetCalculator(actionTargetCalculator);
            _isActionTargetCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built protect limit change action's amount.
        /// </summary>
        /// <param name="amount">The built protect limit change action's amount.</param>
        public ProtectLimitChangeBuilder WithAmount(int amount)
        {
            _protectLimitChange.Amount = amount;
            _isAmountSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built protect limit change action to target all characters including the user.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsAll()
        {
            return WithActionTargetCalculator(new AllActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target all other characters.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsOthers()
        {
            return WithActionTargetCalculator(new OthersActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target all enemies.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsEnemies()
        {
            return WithActionTargetCalculator(new EnemiesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target all characters on the user's team.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsTeam()
        {
            return WithActionTargetCalculator(new TeamActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target all other characters on the user's team.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsAllies()
        {
            return WithActionTargetCalculator(new AlliesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target the user.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsUser()
        {
            return WithActionTargetCalculator(new UserActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target the first enemy.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsFirstEnemy()
        {
            return WithActionTargetCalculator(new FirstEnemyActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target the first ally.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsFirstAlly()
        {
            return WithActionTargetCalculator(new FirstAllyActionTargetCalculator());
        }

        /// <summary>
        /// Returns the built protect limit change action.
        /// </summary>
        public ProtectLimitChange Build()
        {
            if (!_isActionTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a protect limit change action with no action target calculator!");
            }

            if (!_isAmountSet)
            {
                throw new InvalidOperationException("Cannot build a protect limit change action with no amount set!");
            }

            return _protectLimitChange;
        }
    }
}
