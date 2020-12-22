using System.Collections.Generic;
using BattleSystem.Characters;

namespace BattleSystem.Moves.Targets
{
    /// <summary>
    /// Calculates the move targets as all of the other characters.
    /// </summary>
    public class OthersMoveTargetCalculator : IMoveTargetCalculator
    {
        /// <inheritdoc />
        public IEnumerable<Character> Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            return otherCharacters;
        }
    }
}
