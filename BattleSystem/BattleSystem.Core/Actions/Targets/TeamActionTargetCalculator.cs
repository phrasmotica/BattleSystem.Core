using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Actions.Targets
{
    /// <summary>
    /// Calculates the action targets as all characters on the user's team.
    /// </summary>
    public class TeamActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public bool IsReactive => false;

        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            return (true, otherCharacters.Where(c => c.Team == user.Team).Prepend(user));
        }
    }
}
