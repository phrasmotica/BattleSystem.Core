using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Targets
{
    /// <summary>
    /// Calculates the action targets as all characters on the user's team.
    /// </summary>
    public class TeamActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public IEnumerable<Character> Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            return otherCharacters.Where(c => c.Team == user.Team).Prepend(user);
        }
    }
}
