using System;
using BattleSystem.Moves.Targets;
using BattleSystem.Stats;

namespace BattleSystem.Moves.Actions
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
        /// Whether the move target calculator of the buff has been set.
        /// </summary>
        private bool _isMoveTargetCalculatorSet;

        /// <summary>
        /// Creates a new <see cref="BuffBuilder"/> instance.
        /// </summary>
        public BuffBuilder()
        {
            _buff = new Buff();
        }

        /// <summary>
        /// Sets the built buff's move target calculator.
        /// </summary>
        /// <param name="moveTargetCalculator">The built buff's move target calculator.</param>
        public BuffBuilder WithMoveTargetCalculator(IMoveTargetCalculator moveTargetCalculator)
        {
            if (moveTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(moveTargetCalculator));
            }

            _buff.SetMoveTargetCalculator(moveTargetCalculator);
            _isMoveTargetCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built buff to target all characters including the user.
        /// </summary>
        public BuffBuilder TargetsAll()
        {
            return WithMoveTargetCalculator(new AllMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target all other characters.
        /// </summary>
        public BuffBuilder TargetsOthers()
        {
            return WithMoveTargetCalculator(new OthersMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target all enemies.
        /// </summary>
        public BuffBuilder TargetsEnemies()
        {
            return WithMoveTargetCalculator(new EnemiesMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target all characters on the user's team.
        /// </summary>
        public BuffBuilder TargetsTeam()
        {
            return WithMoveTargetCalculator(new TeamMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target all other characters on the user's team.
        /// </summary>
        public BuffBuilder TargetsAllies()
        {
            return WithMoveTargetCalculator(new AlliesMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target the user.
        /// </summary>
        public BuffBuilder TargetsUser()
        {
            return WithMoveTargetCalculator(new UserMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target the first enemy.
        /// </summary>
        public BuffBuilder TargetsFirstEnemy()
        {
            return WithMoveTargetCalculator(new FirstEnemyMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built buff to target the first ally.
        /// </summary>
        public BuffBuilder TargetsFirstAlly()
        {
            return WithMoveTargetCalculator(new FirstAllyMoveTargetCalculator());
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
            if (!_isMoveTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a buff with no move target calculator!");
            }

            return _buff;
        }
    }
}
