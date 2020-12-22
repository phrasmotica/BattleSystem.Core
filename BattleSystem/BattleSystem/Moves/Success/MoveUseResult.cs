namespace BattleSystem.Moves.Success
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
        /// The move failed to land on the target.
        /// </summary>
        Miss,
    }
}
