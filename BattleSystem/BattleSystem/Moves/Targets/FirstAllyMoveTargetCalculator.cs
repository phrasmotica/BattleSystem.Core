using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;

namespace BattleSystem.Moves.Targets
{
    /// <summary>
    /// Calculates the move target as the first of the move user's allies.
    /// </summary>
    public class FirstAllyMoveTargetCalculator : IMoveTargetCalculator
    {
        /// <inheritdoc />
        public IEnumerable<Character> Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            var allies = otherCharacters.Where(c => c.Team == user.Team);
            if (!allies.Any())
            {
                throw new ArgumentException("No allies to choose as a target!", nameof(otherCharacters));
            }

            return allies.Take(1);
        }
    }
}
