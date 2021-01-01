namespace BattleSystem.Actions.Results
{
    /// <summary>
    /// Class for the result of an attack being applied to a character.
    /// </summary>
    /// <typeparam name="TSource">The type of the source of the attack.</typeparam>
    public class AttackResult<TSource> : IActionResult<TSource>
    {
        /// <summary>
        /// Gets or sets whether the damage was applied to the character.
        /// </summary>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the source of the attack.
        /// </summary>
        public TSource Source { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who was the target of the attack.
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// Gets or sets the target's health before the attack.
        /// </summary>
        public int StartingHealth { get; set; }

        /// <summary>
        /// Gets or sets the target's health after the attack.
        /// </summary>
        public int EndingHealth { get; set; }

        /// <summary>
        /// Gets the damage taken by the target from the attack.
        /// </summary>
        public int Damage => StartingHealth - EndingHealth;

        /// <summary>
        /// Gets whether the target died from the attack.
        /// </summary>
        public bool TargetDied => EndingHealth <= 0;

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
