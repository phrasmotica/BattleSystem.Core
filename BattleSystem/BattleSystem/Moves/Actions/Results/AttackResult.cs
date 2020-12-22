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
        /// Gets or sets whether the target protected itself from the attack's damage.
        /// </summary>
        public bool TargetProtected { get; set; }
    }
}
