using System.Collections.Generic;
using BattleSystem.Characters;

namespace BattleSystem.Moves.Targets
{
    /// <summary>
    /// Calculates the move target as the user.
    /// </summary>
    public class UserMoveTargetCalculator : IMoveTargetCalculator
    {
        /// <inheritdoc />
        public IEnumerable<Character> Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            return new[] { user };
        }
    }
}
