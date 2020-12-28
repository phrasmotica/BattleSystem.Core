using System;
using BattleSystem.Moves.Targets;

namespace BattleSystem.Moves.Actions
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
        /// Whether the move target calculator of the protect action has been set.
        /// </summary>
        private bool _isMoveTargetCalculatorSet;

        /// <summary>
        /// Creates a new <see cref="ProtectBuilder"/> instance.
        /// </summary>
        public ProtectBuilder()
        {
            _protect = new Protect();
        }

        /// <summary>
        /// Sets the built protect action's move target calculator.
        /// </summary>
        /// <param name="name">The built protect action's move target calculator.</param>
        public ProtectBuilder WithMoveTargetCalculator(IMoveTargetCalculator moveTargetCalculator)
        {
            if (moveTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(moveTargetCalculator));
            }

            _protect.SetMoveTargetCalculator(moveTargetCalculator);
            _isMoveTargetCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built protect action to target all characters including the user.
        /// </summary>
        public ProtectBuilder TargetsAll()
        {
            return WithMoveTargetCalculator(new AllMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target all other characters.
        /// </summary>
        public ProtectBuilder TargetsOthers()
        {
            return WithMoveTargetCalculator(new OthersMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target all enemies.
        /// </summary>
        public ProtectBuilder TargetsEnemies()
        {
            return WithMoveTargetCalculator(new EnemiesMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target all characters on the user's team.
        /// </summary>
        public ProtectBuilder TargetsTeam()
        {
            return WithMoveTargetCalculator(new TeamMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target all other characters on the user's team.
        /// </summary>
        public ProtectBuilder TargetsAllies()
        {
            return WithMoveTargetCalculator(new AlliesMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target the user.
        /// </summary>
        public ProtectBuilder TargetsUser()
        {
            return WithMoveTargetCalculator(new UserMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target the first enemy.
        /// </summary>
        public ProtectBuilder TargetsFirstEnemy()
        {
            return WithMoveTargetCalculator(new FirstEnemyMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built protect action to target the first ally.
        /// </summary>
        public ProtectBuilder TargetsFirstAlly()
        {
            return WithMoveTargetCalculator(new FirstAllyMoveTargetCalculator());
        }

        /// <summary>
        /// Returns the built protect action.
        /// </summary>
        public Protect Build()
        {
            if (!_isMoveTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a protect action with no move target calculator!");
            }

            return _protect;
        }
    }
}
