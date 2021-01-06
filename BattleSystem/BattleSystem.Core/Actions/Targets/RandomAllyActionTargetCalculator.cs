using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Actions.Targets
{
    /// <summary>
    /// Calculates the action target as one of the user's allies at random.
    /// </summary>
    public class RandomAllyActionTargetCalculator : IActionTargetCalculator
    {
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

            var r = new Random().Next(allies.Length);
            return (true, new[] { allies[r] });
        }
    }
}
