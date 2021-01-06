using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Actions.Targets
{
    /// <summary>
    /// Calculates the action targets as the user's enemies.
    /// </summary>
    public class EnemiesActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public bool IsReactive => false;

        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            var enemies = otherCharacters.Where(c => c.Team != user.Team);
            if (!enemies.Any())
            {
                return (false, Enumerable.Empty<Character>());
            }

            return (true, enemies);
        }
    }
}
