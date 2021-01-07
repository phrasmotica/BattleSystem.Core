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

            var r = new Random().Next(targets.Length);
            return (true, new[] { targets[r] });
        }
    }
}
