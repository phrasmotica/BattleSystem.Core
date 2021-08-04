namespace BattleSystem.Core.Abilities.Results
{
    /// <summary>
    /// Represents the result of removing an ability from a character.
    /// </summary>
    public class RemoveAbilityResult
    {
        /// <summary>
        /// Gets or sets whether the removal was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the ability that was removed from the character.
        /// </summary>
        public Ability Ability { get; set; }
    }
}
