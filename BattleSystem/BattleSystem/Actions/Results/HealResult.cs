namespace BattleSystem.Actions.Results
{
    /// <summary>
    /// Class for the result of a heal being applied to a character.
    /// </summary>
    /// <typeparam name="TSource">The type of the source of the heal.</typeparam>
    public class HealResult<TSource> : IActionResult<TSource>
    {
        /// <summary>
        /// Gets or sets whether the heal was applied to the character.
        /// </summary>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the source of the attack.
        /// </summary>
        public TSource Source { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who was the target of the heal.
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// Gets or sets whether the character was protected from the heal.
        /// </summary>
        public bool TargetProtected { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who protected the target from the heal, if applicable.
        /// </summary>
        public string ProtectUserId { get; set; }

        /// <summary>
        /// Gets or sets the target's health before the heal.
        /// </summary>
        public int StartingHealth { get; set; }

        /// <summary>
        /// Gets or sets the target's health after the heal.
        /// </summary>
        public int EndingHealth { get; set; }

        /// <summary>
        /// Gets or sets whether the heal was applied to the character.
        /// </summary>
        public int Amount => EndingHealth - StartingHealth;
    }
}
