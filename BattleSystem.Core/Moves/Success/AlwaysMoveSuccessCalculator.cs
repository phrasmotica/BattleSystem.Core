using BattleSystem.Core.Success;

namespace BattleSystem.Core.Moves.Success
{
    /// <summary>
    /// Calculates a move to always succeed.
    /// </summary>
    public class AlwaysMoveSuccessCalculator : ISuccessCalculator<Move, MoveUseResult>
    {
        /// <inheritdoc />
        public MoveUseResult Calculate(Move move)
        {
            return MoveUseResult.Success;
        }
    }
}
