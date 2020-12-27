using System;
using BattleSystem.Moves.Targets;

namespace BattleSystem.Moves.Actions
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
        /// Whether the move target calculator of the protect limit change action has been set.
        /// </summary>
        private bool _isMoveTargetCalculatorSet;

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
        /// Sets the built protect limit change action's move target calculator.
        /// </summary>
        /// <param name="name">The built protect limit change action's move target calculator.</param>
        public ProtectLimitChangeBuilder WithMoveTargetCalculator(IMoveTargetCalculator moveTargetCalculator)
        {
            if (moveTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(moveTargetCalculator));
            }

            _protectLimitChange.SetMoveTargetCalculator(moveTargetCalculator);
            _isMoveTargetCalculatorSet = true;
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
            return WithMoveTargetCalculator(new AllMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target all other characters.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsOthers()
        {
            return WithMoveTargetCalculator(new OthersMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target all enemies.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsEnemies()
        {
            return WithMoveTargetCalculator(new EnemiesMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target all characters on the user's team.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsTeam()
        {
            return WithMoveTargetCalculator(new TeamMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target all other characters on the user's team.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsAllies()
        {
            return WithMoveTargetCalculator(new AlliesMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target the user.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsUser()
        {
            return WithMoveTargetCalculator(new UserMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target the first enemy.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsFirstEnemy()
        {
            return WithMoveTargetCalculator(new FirstEnemyMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect limit change action to target the first ally.
        /// </summary>
        public ProtectLimitChangeBuilder TargetsFirstAlly()
        {
            return WithMoveTargetCalculator(new FirstAllyMoveTargetCalculator());
        }

        /// <summary>
        /// Returns the built protect limit change action.
        /// </summary>
        public ProtectLimitChange Build()
        {
            if (!_isMoveTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a protect limit change action with no move target calculator!");
            }

            if (!_isAmountSet)
            {
                throw new InvalidOperationException("Cannot build a protect limit change action with no amount set!");
            }

            return _protectLimitChange;
        }
    }
}
