using BattleSystem.Actions.Damage;
using BattleSystem.Actions.Damage.Calculators;
using BattleSystem.Characters;

namespace BattleSystemExample.Actions.Damage.Calculators
{
    /// <summary>
    /// Calculates damage by a base power that is equal to the starting amount
    /// plus a linear factor multiplied by the amount of times this action has
    /// been used successfully since it last failed.
    /// </summary>
    public class BasePowerIncreasesLinearlyWithUsesDamageCalculator : IDamageCalculator
    {
        /// <summary>
        /// The starting base power.
        /// </summary>
        private readonly int _startingBasePower;

        /// <summary>
        /// The linear factor.
        /// </summary>
        private readonly int _linearFactor;

        /// <summary>
        /// The action history.
        /// </summary>
        private readonly ActionHistory _actionHistory;

        /// <summary>
        /// Creates a new <see cref="BasePowerIncreasesLinearlyWithUsesDamageCalculator"/> instance.
        /// </summary>
        /// <param name="startingBasePower">The starting amount of damage to deal.</param>
        /// <param name="linearFactor">The linear factor.</param>
        /// <param name="actionHistory">The action history.</param>
        public BasePowerIncreasesLinearlyWithUsesDamageCalculator(
            int startingBasePower,
            int linearFactor,
            ActionHistory actionHistory)
        {
            _startingBasePower = startingBasePower;
            _linearFactor = linearFactor;
            _actionHistory = actionHistory;
        }

        /// <inheritdoc/>
        public DamageCalculation Calculate(Character user, DamageAction damage, Character target)
        {
            var count = _actionHistory.GetMoveDamageCount(damage, user);
            var basePower = _startingBasePower + _linearFactor * count;

            return new BasePowerDamageCalculator(basePower).Calculate(user, damage, target);
        }
    }
}
