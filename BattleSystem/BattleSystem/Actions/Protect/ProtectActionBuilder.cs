using System;
using BattleSystem.Actions.Targets;

namespace BattleSystem.Actions.Protect
{
    /// <summary>
    /// Builder class for protect actions.
    /// </summary>
    public class ProtectActionBuilder
    {
        /// <summary>
        /// The protect action to build.
        /// </summary>
        private readonly ProtectAction _protect;

        /// <summary>
        /// Whether the action target calculator of the protect action has been set.
        /// </summary>
        private bool _isActionTargetCalculatorSet;

        /// <summary>
        /// Creates a new <see cref="ProtectActionBuilder"/> instance.
        /// </summary>
        public ProtectActionBuilder()
        {
            _protect = new ProtectAction();
        }

        /// <summary>
        /// Sets the built protect action's action target calculator.
        /// </summary>
        /// <param name="name">The built protect action's action target calculator.</param>
        public ProtectActionBuilder WithActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
        {
            if (actionTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(actionTargetCalculator));
            }

            _protect.SetActionTargetCalculator(actionTargetCalculator);
            _isActionTargetCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built protect action to target all characters including the user.
        /// </summary>
        public ProtectActionBuilder TargetsAll()
        {
            return WithActionTargetCalculator(new AllActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target all other characters.
        /// </summary>
        public ProtectActionBuilder TargetsOthers()
        {
            return WithActionTargetCalculator(new OthersActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target all enemies.
        /// </summary>
        public ProtectActionBuilder TargetsEnemies()
        {
            return WithActionTargetCalculator(new EnemiesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target all characters on the user's team.
        /// </summary>
        public ProtectActionBuilder TargetsTeam()
        {
            return WithActionTargetCalculator(new TeamActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target all other characters on the user's team.
        /// </summary>
        public ProtectActionBuilder TargetsAllies()
        {
            return WithActionTargetCalculator(new AlliesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target the user.
        /// </summary>
        public ProtectActionBuilder TargetsUser()
        {
            return WithActionTargetCalculator(new UserActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target the first enemy.
        /// </summary>
        public ProtectActionBuilder TargetsFirstEnemy()
        {
            return WithActionTargetCalculator(new FirstEnemyActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target the first ally.
        /// </summary>
        public ProtectActionBuilder TargetsFirstAlly()
        {
            return WithActionTargetCalculator(new FirstAllyActionTargetCalculator());
        }

        /// <summary>
        /// Returns the built protect action.
        /// </summary>
        public ProtectAction Build()
        {
            if (!_isActionTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a protect action with no action target calculator!");
            }

            return _protect;
        }
    }
}
