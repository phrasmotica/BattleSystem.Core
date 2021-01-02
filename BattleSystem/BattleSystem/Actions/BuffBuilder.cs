using System;
using BattleSystem.Actions.Targets;
using BattleSystem.Stats;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Builder class for buffs.
    /// </summary>
    public class BuffBuilder
    {
        /// <summary>
        /// The buff to build.
        /// </summary>
        private readonly Buff _buff;

        /// <summary>
        /// Whether the action target calculator of the buff has been set.
        /// </summary>
        private bool _isActionTargetCalculatorSet;

        /// <summary>
        /// Creates a new <see cref="BuffBuilder"/> instance.
        /// </summary>
        public BuffBuilder()
        {
            _buff = new Buff();
        }

        /// <summary>
        /// Sets the built buff's action target calculator.
        /// </summary>
        /// <param name="actionTargetCalculator">The built buff's action target calculator.</param>
        public BuffBuilder WithActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
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
        public BuffBuilder TargetsAll()
        {
            return WithActionTargetCalculator(new AllActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target all other characters.
        /// </summary>
        public BuffBuilder TargetsOthers()
        {
            return WithActionTargetCalculator(new OthersActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target all enemies.
        /// </summary>
        public BuffBuilder TargetsEnemies()
        {
            return WithActionTargetCalculator(new EnemiesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target all characters on the user's team.
        /// </summary>
        public BuffBuilder TargetsTeam()
        {
            return WithActionTargetCalculator(new TeamActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target all other characters on the user's team.
        /// </summary>
        public BuffBuilder TargetsAllies()
        {
            return WithActionTargetCalculator(new AlliesActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target the user.
        /// </summary>
        public BuffBuilder TargetsUser()
        {
            return WithActionTargetCalculator(new UserActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target the first enemy.
        /// </summary>
        public BuffBuilder TargetsFirstEnemy()
        {
            return WithActionTargetCalculator(new FirstEnemyActionTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target the first ally.
        /// </summary>
        public BuffBuilder TargetsFirstAlly()
        {
            return WithActionTargetCalculator(new FirstAllyActionTargetCalculator());
        }

        /// <summary>
        /// Adds the given target multiplier to the built buff.
        /// </summary>
        /// <param name="statCategory">The stat category.</param>
        /// <param name="fraction">The fraction, between 0 and 1.</param>
        public BuffBuilder WithTargetMultiplier(StatCategory statCategory, double fraction)
        {
            _buff.TargetMultipliers[statCategory] = fraction;
            return this;
        }

        /// <summary>
        /// Sets the built buff to raise the target's attack by the given fraction.
        /// </summary>
        /// <param name="fraction">The fraction, between 0 and 1.</param>
        public BuffBuilder WithRaiseAttack(double fraction)
        {
            return WithTargetMultiplier(StatCategory.Attack, fraction);
        }

        /// <summary>
        /// Sets the built buff to raise the target's defence by the given fraction.
        /// </summary>
        /// <param name="fraction">The fraction, between 0 and 1.</param>
        public BuffBuilder WithRaiseDefence(double fraction)
        {
            return WithTargetMultiplier(StatCategory.Defence, fraction);
        }

        /// <summary>
        /// Sets the built buff to lower the target's defence by the given fraction.
        /// </summary>
        /// <param name="fraction">The fraction, between 0 and 1.</param>
        public BuffBuilder WithLowerDefence(double fraction)
        {
            return WithTargetMultiplier(StatCategory.Defence, -fraction);
        }

        /// <summary>
        /// Returns the built buff.
        /// </summary>
        public Buff Build()
        {
            if (!_isActionTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a buff with no action target calculator!");
            }

            return _buff;
        }
    }
}
