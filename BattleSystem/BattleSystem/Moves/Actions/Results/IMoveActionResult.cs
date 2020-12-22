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
    }
}
