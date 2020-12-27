namespace BattleSystem.Moves.Actions.Results
{
    /// <summary>
    /// Interface for the result of a move action being applied to a character.
    /// </summary>
    public interface IMoveActionResult
    {
        /// <summary>
        /// Gets or sets whether the move action was applied to the character.
        /// </summary>
        bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who was the target of the move action.
        /// </summary>
        string TargetId { get; set; }
    }
}
