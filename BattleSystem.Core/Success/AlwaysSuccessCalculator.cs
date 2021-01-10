using System.Collections.Generic;
using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Moves.Success
{
    /// <summary>
    /// Calculates a move to always succeed.
    /// </summary>
    public class AlwaysSuccessCalculator : ISuccessCalculator
    {
        /// <inheritdoc />
        public MoveUseResult Calculate(Character user, Move move, IEnumerable<Character> otherCharacters)
        {
            return MoveUseResult.Success;
        }
    }
}
