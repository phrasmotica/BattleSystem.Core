using BattleSystem.Characters;

namespace BattleSystem.Actions.Damage.Calulators
{
    /// <summary>
    /// Calculates damage equal to the damage action's power.
    /// </summary>
    public class AbsoluteDamageCalculator : IDamageCalculator
    {
        /// <inheritdoc/>
        public int Calculate(Character user, Damage damage, Character target)
        {
            return damage.Power;
        }
    }
}
