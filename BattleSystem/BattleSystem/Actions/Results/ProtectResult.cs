namespace BattleSystem.Actions.Results
{
    /// <summary>
    /// Class for the result of a protect action being applied to a character.
    /// </summary>
    public class ProtectResult : IActionResult
    {
        /// <summary>
        /// Gets or sets whether the protect action was applied to the character.
        /// </summary>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who was the target of the protect.
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// Gets or sets whether the character was protected from the protect.
        /// </summary>
        public bool TargetProtected { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who protected the target from the protect, if applicable.
        /// </summary>
        public string ProtectUserId { get; set; }
    }
}
