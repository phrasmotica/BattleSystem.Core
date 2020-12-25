namespace BattleSystem.Moves.Actions.Results
{
    /// <summary>
    /// Class for the result of an attack being applied to a character.
    /// </summary>
    public class AttackResult : IMoveActionResult
    {
        /// <summary>
        /// Gets or sets whether the damage was applied to the character.
        /// </summary>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who was the target of the attack.
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// Gets or sets whether the character was protected from the attack's damage.
        /// </summary>
        public bool TargetProtected { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who protected the target from the attack, if applicable.
        /// </summary>
        public string ProtectUserId { get; set; }
    }
}
