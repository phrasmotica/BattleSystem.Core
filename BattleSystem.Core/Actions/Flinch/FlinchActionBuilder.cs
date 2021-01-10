using System;
using BattleSystem.Core.Characters.Targets;
using BattleSystem.Core.Random;

namespace BattleSystem.Core.Actions.Flinch
{
    /// <summary>
    /// Builder class for flinch actions.
    /// </summary>
    public class FlinchActionBuilder
    {
        /// <summary>
        /// The flinch action to build.
        /// </summary>
        private readonly FlinchAction _flinch;

        /// <summary>
        /// Whether the action target calculator of the flinch action has been set.
        /// </summary>
        private bool _isActionTargetCalculatorSet;

        /// <summary>
        /// Creates a new <see cref="FlinchActionBuilder"/> instance.
        /// </summary>
        public FlinchActionBuilder()
        {
            _flinch = new FlinchAction();
        }

        /// <summary>
        /// Sets the built flinch's action target calculator.
        /// </summary>
        /// <param name="actionTargetCalculator">The built flinch's action target calculator.</param>
        public FlinchActionBuilder WithActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
        {
            if (actionTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(actionTargetCalculator));
            }

            _flinch.SetActionTargetCalculator(actionTargetCalculator);
            _isActionTargetCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built flinch to target all characters including the user.
        /// </summary>
        public FlinchActionBuilder TargetsAll()
        {
            return WithActionTargetCalculator(new AllActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built flinch to target all other characters.
        /// </summary>
        public FlinchActionBuilder TargetsOthers()
        {
            return WithActionTargetCalculator(new OthersActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built flinch to target all enemies.
        /// </summary>
        public FlinchActionBuilder TargetsEnemies()
        {
            return WithActionTargetCalculator(new EnemiesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built flinch to target all characters on the user's team.
        /// </summary>
        public FlinchActionBuilder TargetsTeam()
        {
            return WithActionTargetCalculator(new TeamActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built flinch to target all other characters on the user's team.
        /// </summary>
        public FlinchActionBuilder TargetsAllies()
        {
            return WithActionTargetCalculator(new AlliesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built flinch to target the user.
        /// </summary>
        public FlinchActionBuilder TargetsUser()
        {
            return WithActionTargetCalculator(new UserActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built flinch to target a random enemy.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public FlinchActionBuilder TargetsRandomEnemy(IRandom random)
        {
            return WithActionTargetCalculator(new RandomEnemyActionTargetCalculator(random));
        }

        /// <summary>
        /// Sets the built flinch to target a random ally.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public FlinchActionBuilder TargetsRandomAlly(IRandom random)
        {
            return WithActionTargetCalculator(new RandomAllyActionTargetCalculator(random));
        }

        /// <summary>
        /// Sets the built flinch to target a random character.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public FlinchActionBuilder TargetsRandomCharacter(IRandom random)
        {
            return WithActionTargetCalculator(new RandomCharacterActionTargetCalculator(random));
        }

        /// <summary>
        /// Sets the built flinch to target a random other character.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public FlinchActionBuilder TargetsRandomOther(IRandom random)
        {
            return WithActionTargetCalculator(new RandomOtherActionTargetCalculator(random));
        }

        /// <summary>
        /// Sets the built flinch to target the first enemy.
        /// </summary>
        public FlinchActionBuilder TargetsFirstEnemy()
        {
            return WithActionTargetCalculator(new FirstEnemyActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built flinch to target the first ally.
        /// </summary>
        public FlinchActionBuilder TargetsFirstAlly()
        {
            return WithActionTargetCalculator(new FirstAllyActionTargetCalculator());
        }

        /// <summary>
        /// Returns the built flinch.
        /// </summary>
        public FlinchAction Build()
        {
            if (!_isActionTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a flinch with no action target calculator!");
            }

            return _flinch;
        }
    }
}
