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

        /// <summary>
        /// Gets or sets the ID of the character who was the target of the protect.
        /// </summary>
        public string TargetId { get; set; }
    }
}
