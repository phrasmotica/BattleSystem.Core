using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Targets
{
    /// <summary>
    /// Calculates the action targets as the user's allies.
    /// </summary>
    public class AlliesActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public IEnumerable<Character> Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            var allies = otherCharacters.Where(c => c.Team == user.Team);
            if (!allies.Any())
            {
                throw new ArgumentException("No allies to choose as targets!", nameof(otherCharacters));
            }

            return allies;
        }
    }
}
