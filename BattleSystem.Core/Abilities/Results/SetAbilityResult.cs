namespace BattleSystem.Core.Abilities.Results
{
    /// <summary>
    /// Represents the result of setting a character's ability.
    /// </summary>
    public class SetAbilityResult
    {
        /// <summary>
        /// Gets or sets whether the setting of the ability was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets whether the character had an ability before the new one
        /// was set.
        /// </summary>
        public bool HadPreviousAbility { get; set; }

        /// <summary>
        /// Gets or sets the ability that was previously equipped to the
        /// character.
        /// </summary>
        public Ability PreviousAbility { get; set; }
    }
}
