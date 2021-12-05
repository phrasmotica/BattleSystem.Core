using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleSystem.Core.Characters.Targets
{
    /// <summary>
    /// Calculates the action target as one of the other characters at random.
    /// </summary>
    public class RandomOtherActionTargetCalculator : IActionTargetCalculator
    {
        /// <summary>
        /// The random number generator.
        /// </summary>
        private readonly Random _random;

        /// <summary>
        /// Creates a new <see cref="RandomOtherActionTargetCalculator"/> instance.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public RandomOtherActionTargetCalculator(Random random)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        /// <inheritdoc />
        public bool IsReactive => false;

        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            var targets = otherCharacters.ToArray();
            if (!targets.Any())
            {
                return (false, Enumerable.Empty<Character>());
            }

            var r = _random.Next(targets.Length);
            return (true, new[] { targets[r] });
        }
    }
}
