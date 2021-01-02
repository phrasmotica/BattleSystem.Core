using System;
using BattleSystem.Actions.Targets;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Builder class for protect actions.
    /// </summary>
    public class ProtectBuilder
    {
        /// <summary>
        /// The protect action to build.
        /// </summary>
        private readonly Protect _protect;

        /// <summary>
        /// Whether the action target calculator of the protect action has been set.
        /// </summary>
        private bool _isactionTargetCalculatorSet;

        /// <summary>
        /// Creates a new <see cref="ProtectBuilder"/> instance.
        /// </summary>
        public ProtectBuilder()
        {
            _protect = new Protect();
        }

        /// <summary>
        /// Sets the built protect action's action target calculator.
        /// </summary>
        /// <param name="name">The built protect action's action target calculator.</param>
        public ProtectBuilder WithActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
        {
            if (actionTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(actionTargetCalculator));
            }

            _protect.SetActionTargetCalculator(actionTargetCalculator);
            _isactionTargetCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built protect action to target all characters including the user.
        /// </summary>
        public ProtectBuilder TargetsAll()
        {
            return WithActionTargetCalculator(new AllActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target all other characters.
        /// </summary>
        public ProtectBuilder TargetsOthers()
        {
            return WithActionTargetCalculator(new OthersActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target all enemies.
        /// </summary>
        public ProtectBuilder TargetsEnemies()
        {
            return WithActionTargetCalculator(new EnemiesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target all characters on the user's team.
        /// </summary>
        public ProtectBuilder TargetsTeam()
        {
            return WithActionTargetCalculator(new TeamActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target all other characters on the user's team.
        /// </summary>
        public ProtectBuilder TargetsAllies()
        {
            return WithActionTargetCalculator(new AlliesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target the user.
        /// </summary>
        public ProtectBuilder TargetsUser()
        {
            return WithActionTargetCalculator(new UserActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target the first enemy.
        /// </summary>
        public ProtectBuilder TargetsFirstEnemy()
        {
            return WithActionTargetCalculator(new FirstEnemyActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target the first ally.
        /// </summary>
        public ProtectBuilder TargetsFirstAlly()
        {
            return WithActionTargetCalculator(new FirstAllyActionTargetCalculator());
        }

        /// <summary>
        /// Returns the built protect action.
        /// </summary>
        public Protect Build()
        {
            if (!_isactionTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a protect action with no action target calculator!");
            }

            return _protect;
        }
    }
}
