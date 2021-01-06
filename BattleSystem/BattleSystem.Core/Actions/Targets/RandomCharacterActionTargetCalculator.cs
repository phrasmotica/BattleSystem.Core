using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Actions.Targets
{
    /// <summary>
    /// Calculates the action target as one of the characters at random.
    /// </summary>
    public class RandomCharacterActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public bool IsReactive => false;

        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            var targets = otherCharacters.Prepend(user).ToArray();
            var r = new Random().Next(targets.Length);
            return (true, new[] { targets[r] });
        }
    }
}
