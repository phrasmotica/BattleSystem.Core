using BattleSystem.Characters;
using BattleSystem.Moves.Actions;

namespace BattleSystem.Damage
{
    /// <summary>
    /// Calculates damage equal to the attack's power.
    /// </summary>
    public class AbsoluteDamageCalculator : IDamageCalculator
    {
        /// <inheritdoc/>
        public int Calculate(Character user, Attack attack, Character target)
        {
            return attack.Power;
        }
    }
}
