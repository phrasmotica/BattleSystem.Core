namespace BattleSystem.Moves.Actions.Results
{
    /// <summary>
    /// Class for the result of a protect action being applied to a character.
    /// </summary>
    public class ProtectResult : IMoveActionResult
    {
        /// <summary>
        /// Gets or sets whether the protect action was applied to the character.
        /// </summary>
        public bool Applied { get; set; }
    }
}
