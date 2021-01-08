using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Random;

namespace BattleSystem.Core.Characters.Targets
{
    /// <summary>
    /// Calculates the action target as one of the user's allies at random.
    /// </summary>
    public class RandomAllyActionTargetCalculator : IActionTargetCalculator
    {
        /// <summary>
        /// The random number generator.
        /// </summary>
        private readonly IRandom _random;

        /// <summary>
        /// Creates a new <see cref="RandomAllyActionTargetCalculator"/> instance.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public RandomAllyActionTargetCalculator(IRandom random)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        /// <inheritdoc />
        public bool IsReactive => false;

        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            var allies = otherCharacters.Where(c => c.Team == user.Team).ToArray();
            if (!allies.Any())
            {
                return (false, Enumerable.Empty<Character>());
            }

            var r = _random.Next(allies.Length);
            return (true, new[] { allies[r] });
        }
    }
}
