using System;
using BattleSystem.Core.Characters.Targets;
using BattleSystem.Core.Random;

namespace BattleSystem.Core.Actions.ProtectLimitChange
{
    /// <summary>
    /// Builder class for protect limit change actions.
    /// </summary>
    public class ProtectLimitChangeActionBuilder
    {
        /// <summary>
        /// The protect limit change action to build.
        /// </summary>
        private readonly ProtectLimitChangeAction _protectLimitChange;

        /// <summary>
        /// Whether the action target calculator of the protect limit change action has been set.
        /// </summary>
        private bool _isActionTargetCalculatorSet;

        /// <summary>
        /// Whether the amount of the protect limit change action has been set.
        /// </summary>
        private bool _isAmountSet;

        /// <summary>
        /// Creates a new <see cref="ProtectLimitChangeActionBuilder"/> instance.
        /// </summary>
        public ProtectLimitChangeActionBuilder()
        {
            _protectLimitChange = new ProtectLimitChangeAction();
        }

        /// <summary>
        /// Sets the built protect limit change action's action target calculator.
        /// </summary>
        /// <param name="name">The built protect limit change action's action target calculator.</param>
        public ProtectLimitChangeActionBuilder WithActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
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
        public ProtectLimitChangeActionBuilder WithAmount(int amount)
        {
            _protectLimitChange.Amount = amount;
            _isAmountSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built protect limit change action to target all characters including the user.
        /// </summary>
        public ProtectLimitChangeActionBuilder TargetsAll()
        {
            return WithActionTargetCalculator(new AllActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target all other characters.
        /// </summary>
        public ProtectLimitChangeActionBuilder TargetsOthers()
        {
            return WithActionTargetCalculator(new OthersActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target all enemies.
        /// </summary>
        public ProtectLimitChangeActionBuilder TargetsEnemies()
        {
            return WithActionTargetCalculator(new EnemiesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target all characters on the user's team.
        /// </summary>
        public ProtectLimitChangeActionBuilder TargetsTeam()
        {
            return WithActionTargetCalculator(new TeamActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target all other characters on the user's team.
        /// </summary>
        public ProtectLimitChangeActionBuilder TargetsAllies()
        {
            return WithActionTargetCalculator(new AlliesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target the user.
        /// </summary>
        public ProtectLimitChangeActionBuilder TargetsUser()
        {
            return WithActionTargetCalculator(new UserActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target a random enemy.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public ProtectLimitChangeActionBuilder TargetsRandomEnemy(IRandom random)
        {
            return WithActionTargetCalculator(new RandomEnemyActionTargetCalculator(random));
        }

        /// <summary>
        /// Sets the built protect limit change action to target a random ally.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public ProtectLimitChangeActionBuilder TargetsRandomAlly(IRandom random)
        {
            return WithActionTargetCalculator(new RandomAllyActionTargetCalculator(random));
        }

        /// <summary>
        /// Sets the built buff to target a random character.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public ProtectLimitChangeActionBuilder TargetsRandomCharacter(IRandom random)
        {
            return WithActionTargetCalculator(new RandomCharacterActionTargetCalculator(random));
        }

        /// <summary>
        /// Sets the built buff to target a random other character.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public ProtectLimitChangeActionBuilder TargetsRandomOther(IRandom random)
        {
            return WithActionTargetCalculator(new RandomOtherActionTargetCalculator(random));
        }

        /// <summary>
        /// Sets the built protect limit change action to target the first enemy.
        /// </summary>
        public ProtectLimitChangeActionBuilder TargetsFirstEnemy()
        {
            return WithActionTargetCalculator(new FirstEnemyActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target the first ally.
        /// </summary>
        public ProtectLimitChangeActionBuilder TargetsFirstAlly()
        {
            return WithActionTargetCalculator(new FirstAllyActionTargetCalculator());
        }

        /// <summary>
        /// Returns the built protect limit change action.
        /// </summary>
        public ProtectLimitChangeAction Build()
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
