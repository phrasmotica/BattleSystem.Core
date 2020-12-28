namespace BattleSystem.Actions.Results
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

        /// <summary>
        /// Gets or sets whether the character was protected from the move action.
        /// </summary>
        public bool TargetProtected { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who protected the target from the move action, if applicable.
        /// </summary>
        public string ProtectUserId { get; set; }
    }
}
