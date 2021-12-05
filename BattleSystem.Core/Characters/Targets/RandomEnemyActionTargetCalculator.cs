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
        /// <summary>
        /// The random number generator.
        /// </summary>
        private readonly Random _random;

        /// <summary>
        /// Creates a new <see cref="RandomEnemyActionTargetCalculator"/> instance.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public RandomEnemyActionTargetCalculator(Random random)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

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

            var r = _random.Next(enemies.Length);
            return (true, new[] { enemies[r] });
        }
    }
}
