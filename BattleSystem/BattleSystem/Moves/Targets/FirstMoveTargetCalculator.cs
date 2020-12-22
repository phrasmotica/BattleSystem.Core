using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;

namespace BattleSystem.Moves.Targets
{
    /// <summary>
    /// Calculates the move target as the first of the other characters.
    /// </summary>
    public class FirstMoveTargetCalculator : IMoveTargetCalculator
    {
        // TODO: split this into a FirstAllyTargetCalculator and FirstEnemyTargetCalculator

        /// <inheritdoc />
        public IEnumerable<Character> Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            if (!otherCharacters.Any())
            {
                throw new ArgumentException("No other characters to choose as a target!");
            }

            return otherCharacters.Take(1);
        }
    }
}
