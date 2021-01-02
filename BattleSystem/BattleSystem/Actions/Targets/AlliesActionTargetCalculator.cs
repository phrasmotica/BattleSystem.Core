using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Targets
{
    /// <summary>
    /// Calculates the action targets as the user's allies.
    /// </summary>
    public class AlliesActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            var allies = otherCharacters.Where(c => c.Team == user.Team);
            if (!allies.Any())
            {
                return (false, Enumerable.Empty<Character>());
            }

            return (true, allies);
        }
    }
}
