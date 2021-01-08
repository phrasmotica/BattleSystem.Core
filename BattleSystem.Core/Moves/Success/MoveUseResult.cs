namespace BattleSystem.Core.Moves.Success
{
    /// <summary>
    /// Represents the possible results of a move use.
    /// </summary>
    public enum MoveUseResult
    {
        /// <summary>
        /// The move succeeds and lands on the target.
        /// </summary>
        Success,

        /// <summary>
        /// The move was executed but missed the target.
        /// </summary>
        Miss,

        /// <summary>
        /// The move failed for some reason.
        /// </summary>
        Failure,
    }
}
