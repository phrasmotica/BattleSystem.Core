using System.Collections.Generic;
using System.Linq;

namespace BattleSystem.Core.Characters.Targets
{
    /// <summary>
    /// Calculates the action target as the first of the user's enemies.
    /// </summary>
    public class FirstEnemyActionTargetCalculator : IActionTargetCalculator
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

            return (true, enemies.Take(1));
        }
    }
}
