using System;
using BattleSystem.Core.Characters.Targets;
using BattleSystem.Core.Random;
using BattleSystem.Core.Stats;

namespace BattleSystem.Core.Actions.Buff
{
    /// <summary>
    /// Builder class for buffs.
    /// </summary>
    public class BuffActionBuilder
    {
        /// <summary>
        /// The buff to build.
        /// </summary>
        private readonly BuffAction _buff;

        /// <summary>
        /// Whether the action target calculator of the buff has been set.
        /// </summary>
        private bool _isActionTargetCalculatorSet;

        /// <summary>
        /// Creates a new <see cref="BuffActionBuilder"/> instance.
        /// </summary>
        public BuffActionBuilder()
        {
            _buff = new BuffAction();
        }

        /// <summary>
        /// Sets the built buff's action target calculator.
        /// </summary>
        /// <param name="actionTargetCalculator">The built buff's action target calculator.</param>
        public BuffActionBuilder WithActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
        {
            if (actionTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(actionTargetCalculator));
            }

            _buff.SetActionTargetCalculator(actionTargetCalculator);
            _isActionTargetCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built buff to target all characters including the user.
        /// </summary>
        public BuffActionBuilder TargetsAll()
        {
            return WithActionTargetCalculator(new AllActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target all other characters.
        /// </summary>
        public BuffActionBuilder TargetsOthers()
        {
            return WithActionTargetCalculator(new OthersActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target all enemies.
        /// </summary>
        public BuffActionBuilder TargetsEnemies()
        {
            return WithActionTargetCalculator(new EnemiesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target all characters on the user's team.
        /// </summary>
        public BuffActionBuilder TargetsTeam()
        {
            return WithActionTargetCalculator(new TeamActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target all other characters on the user's team.
        /// </summary>
        public BuffActionBuilder TargetsAllies()
        {
            return WithActionTargetCalculator(new AlliesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target the user.
        /// </summary>
        public BuffActionBuilder TargetsUser()
        {
            return WithActionTargetCalculator(new UserActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target a random enemy.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public BuffActionBuilder TargetsRandomEnemy(IRandom random)
        {
            return WithActionTargetCalculator(new RandomEnemyActionTargetCalculator(random));
        }

        /// <summary>
        /// Sets the built buff to target a random ally.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public BuffActionBuilder TargetsRandomAlly(IRandom random)
        {
            return WithActionTargetCalculator(new RandomAllyActionTargetCalculator(random));
        }

        /// <summary>
        /// Sets the built buff to target a random character.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public BuffActionBuilder TargetsRandomCharacter(IRandom random)
        {
            return WithActionTargetCalculator(new RandomCharacterActionTargetCalculator(random));
        }

        /// <summary>
        /// Sets the built buff to target a random other character.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public BuffActionBuilder TargetsRandomOther(IRandom random)
        {
            return WithActionTargetCalculator(new RandomOtherActionTargetCalculator(random));
        }

        /// <summary>
        /// Sets the built buff to target the first enemy.
        /// </summary>
        public BuffActionBuilder TargetsFirstEnemy()
        {
            return WithActionTargetCalculator(new FirstEnemyActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target the first ally.
        /// </summary>
        public BuffActionBuilder TargetsFirstAlly()
        {
            return WithActionTargetCalculator(new FirstAllyActionTargetCalculator());
        }

        /// <summary>
        /// Adds the given target multiplier to the built buff.
        /// </summary>
        /// <param name="statCategory">The stat category.</param>
        /// <param name="fraction">The fraction, between 0 and 1.</param>
        public BuffActionBuilder WithTargetMultiplier(StatCategory statCategory, double fraction)
        {
            _buff.TargetMultipliers[statCategory] = fraction;
            return this;
        }

        /// <summary>
        /// Sets the built buff to raise the target's attack by the given fraction.
        /// </summary>
        /// <param name="fraction">The fraction, between 0 and 1.</param>
        public BuffActionBuilder WithRaiseAttack(double fraction)
        {
            return WithTargetMultiplier(StatCategory.Attack, fraction);
        }

        /// <summary>
        /// Sets the built buff to raise the target's defence by the given fraction.
        /// </summary>
        /// <param name="fraction">The fraction, between 0 and 1.</param>
        public BuffActionBuilder WithRaiseDefence(double fraction)
        {
            return WithTargetMultiplier(StatCategory.Defence, fraction);
        }

        /// <summary>
        /// Sets the built buff to lower the target's defence by the given fraction.
        /// </summary>
        /// <param name="fraction">The fraction, between 0 and 1.</param>
        public BuffActionBuilder WithLowerDefence(double fraction)
        {
            return WithTargetMultiplier(StatCategory.Defence, -fraction);
        }

        /// <summary>
        /// Returns the built buff.
        /// </summary>
        public BuffAction Build()
        {
            if (!_isActionTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a buff with no action target calculator!");
            }

            return _buff;
        }
    }
}
