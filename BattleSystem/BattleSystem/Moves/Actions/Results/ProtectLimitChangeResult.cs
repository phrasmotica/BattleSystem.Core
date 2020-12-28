namespace BattleSystem.Moves.Actions.Results
{
    /// <summary>
    /// Class for the result of a protect limit change action being applied to a character.
    /// </summary>
    public class ProtectLimitChangeResult : IMoveActionResult
    {
        /// <summary>
        /// Gets or sets whether the protect limit change action was applied to the character.
        /// </summary>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character whose protect limit was changed.
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// Gets or sets whether the character was protected from the protect limit change.
        /// </summary>
        public bool TargetProtected { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who protected the target from the protect limit change, if applicable.
        /// </summary>
        public string ProtectUserId { get; set; }

        /// <summary>
        /// Gets or sets the amount that the character's protect limit changed by.
        /// </summary>
        public int Amount { get; set; }
    }
}
