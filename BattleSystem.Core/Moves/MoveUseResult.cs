namespace BattleSystem.Core.Moves
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
        /// The move failed because the user flinched.
        /// </summary>
        Flinched,

        /// <summary>
        /// The move failed for some reason.
        /// </summary>
        Failure,
    }
}
