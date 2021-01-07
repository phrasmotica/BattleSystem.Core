using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Random;

namespace BattleSystem.Core.Characters.Targets
{
    /// <summary>
    /// Calculates the action target as one of the characters at random.
    /// </summary>
    public class RandomCharacterActionTargetCalculator : IActionTargetCalculator
    {
        /// <summary>
        /// The random number generator.
        /// </summary>
        private readonly IRandom _random;

        /// <summary>
        /// Creates a new <see cref="RandomCharacterActionTargetCalculator"/> instance.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public RandomCharacterActionTargetCalculator(IRandom random)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        /// <inheritdoc />
        public bool IsReactive => false;

        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            var targets = otherCharacters.Prepend(user).ToArray();
            var r = _random.Next(targets.Length);
            return (true, new[] { targets[r] });
        }
    }
}
