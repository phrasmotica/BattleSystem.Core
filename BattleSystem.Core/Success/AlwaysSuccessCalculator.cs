using System.Collections.Generic;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Moves;

namespace BattleSystem.Core.Success
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
