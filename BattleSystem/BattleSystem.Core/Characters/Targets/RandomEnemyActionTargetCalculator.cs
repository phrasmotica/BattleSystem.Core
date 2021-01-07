using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleSystem.Core.Characters.Targets
{
    /// <summary>
    /// Calculates the action target as one of the user's enemies at random.
    /// </summary>
    public class RandomEnemyActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public bool IsReactive => false;

        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            var enemies = otherCharacters.Where(c => c.Team != user.Team).ToArray();
            if (!enemies.Any())
            {
                return (false, Enumerable.Empty<Character>());
            }

            var r = new Random().Next(enemies.Length);
            return (true, new[] { enemies[r] });
        }
    }
}
