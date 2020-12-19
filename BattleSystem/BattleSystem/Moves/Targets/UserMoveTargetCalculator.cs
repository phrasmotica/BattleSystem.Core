using System.Collections.Generic;
using BattleSystem.Characters;

namespace BattleSystem.Moves.Targets
{
    /// <summary>
    /// Calculates the move target as the first target available.
    /// </summary>
    public class UserMoveTargetCalculator : IMoveTargetCalculator
    {
        /// <inheritdoc />
        public Character Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            return user;
        }
    }
}
