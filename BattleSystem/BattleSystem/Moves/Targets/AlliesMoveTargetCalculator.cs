using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;

namespace BattleSystem.Moves.Targets
{
    /// <summary>
    /// Calculates the move targets as the user's allies.
    /// </summary>
    public class AlliesMoveTargetCalculator : IMoveTargetCalculator
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
