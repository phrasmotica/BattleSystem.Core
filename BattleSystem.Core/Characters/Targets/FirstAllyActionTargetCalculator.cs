using System.Collections.Generic;
using System.Linq;

namespace BattleSystem.Core.Characters.Targets
{
    /// <summary>
    /// Calculates the action target as the first of the move user's allies.
    /// </summary>
    public class FirstAllyActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public bool IsReactive => false;

        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            var allies = otherCharacters.Where(c => c.Team == user.Team);
            if (!allies.Any())
            {
                return (false, Enumerable.Empty<Character>());
            }

            return (true, allies.Take(1));
        }
    }
}
