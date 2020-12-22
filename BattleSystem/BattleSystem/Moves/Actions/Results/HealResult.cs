namespace BattleSystem.Moves.Actions.Results
{
    /// <summary>
    /// Class for the result of a heal being applied to a character.
    /// </summary>
    public class HealResult : IMoveActionResult
    {
        /// <summary>
        /// Gets or sets whether the heal was applied to the character.
        /// </summary>
        public bool Applied { get; set; }
    }
}
