using System;
using BattleSystem.Healing;
using BattleSystem.Moves.Targets;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Builder class for heals.
    /// </summary>
    public class HealBuilder
    {
        /// <summary>
        /// The heal to build.
        /// </summary>
        private readonly Heal _heal;

        /// <summary>
        /// Whether the amount of the buff has been set.
        /// </summary>
        private bool _isAmountSet;

        /// <summary>
        /// Whether the healing calculator of the buff has been set.
        /// </summary>
        private bool _isHealingCalculatorSet;

        /// <summary>
        /// Whether the move target calculator of the heal has been set.
        /// </summary>
        private bool _isMoveTargetCalculatorSet;

        /// <summary>
        /// Creates a new <see cref="HealBuilder"/> instance.
        /// </summary>
        public HealBuilder()
        {
            _heal = new Heal();
        }

        /// <summary>
        /// Sets the built heal's amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public HealBuilder WithAmount(int amount)
        {
            _heal.Amount = amount;
            _isAmountSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built heal's healing calculator.
        /// </summary>
        /// <param name="healingCalculator">The built heal's healing calculator.</param>
        public HealBuilder WithHealingCalculator(IHealingCalculator moveTargetCalculator)
        {
            if (moveTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(moveTargetCalculator));
            }

            _heal.SetHealingCalculator(moveTargetCalculator);
            _isHealingCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built heal to use absolute healing.
        /// </summary>
        public HealBuilder AbsoluteHealing()
        {
            return WithHealingCalculator(new AbsoluteHealingCalculator());
        }

        /// <summary>
        /// Sets the built heal to use percentage healing.
        /// </summary>
        public HealBuilder PercentageHealing()
        {
            return WithHealingCalculator(new PercentageHealingCalculator());
        }

        /// <summary>
        /// Sets the built heal's move target calculator.
        /// </summary>
        /// <param name="name">The built heal's move target calculator.</param>
        public HealBuilder WithMoveTargetCalculator(IMoveTargetCalculator moveTargetCalculator)
        {
            if (moveTargetCalculator is null)
            {
                throw new ArgumentNullException(nameof(moveTargetCalculator));
            }

            _heal.SetMoveTargetCalculator(moveTargetCalculator);
            _isMoveTargetCalculatorSet = true;
            return this;
        }

        /// <summary>
        /// Sets the built buff to target all characters including the user.
        /// </summary>
        public HealBuilder TargetsAll()
        {
            return WithMoveTargetCalculator(new AllMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target all other characters.
        /// </summary>
        public HealBuilder TargetsOthers()
        {
            return WithMoveTargetCalculator(new OthersMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target all enemies.
        /// </summary>
        public HealBuilder TargetsEnemies()
        {
            return WithMoveTargetCalculator(new EnemiesMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target all characters on the user's team.
        /// </summary>
        public HealBuilder TargetsTeam()
        {
            return WithMoveTargetCalculator(new TeamMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target all other characters on the user's team.
        /// </summary>
        public HealBuilder TargetsAllies()
        {
            return WithMoveTargetCalculator(new AlliesMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target the user.
        /// </summary>
        public HealBuilder TargetsUser()
        {
            return WithMoveTargetCalculator(new UserMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target the first enemy.
        /// </summary>
        public HealBuilder TargetsFirstEnemy()
        {
            return WithMoveTargetCalculator(new FirstAllyMoveTargetCalculator());
        }

        /// <summary>
        /// Sets the built heal to target the first ally.
        /// </summary>
        public HealBuilder TargetsFirstAlly()
        {
            return WithMoveTargetCalculator(new FirstAllyMoveTargetCalculator());
        }

        /// <summary>
        /// Returns the built heal.
        /// </summary>
        public Heal Build()
        {
            if (!_isAmountSet)
            {
                throw new InvalidOperationException("Cannot build a heal with no amount set!");
            }

            if (!_isHealingCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a heal with no healing calculator!");
            }

            if (!_isMoveTargetCalculatorSet)
            {
                throw new InvalidOperationException("Cannot build a heal with no move target calculator!");
            }

            return _heal;
        }
    }
}
