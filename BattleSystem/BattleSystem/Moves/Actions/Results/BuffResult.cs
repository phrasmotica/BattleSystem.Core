namespace BattleSystem.Moves.Actions.Results
{
    /// <summary>
    /// Class for the result of a buff being applied to a character.
    /// </summary>
    public class BuffResult : IMoveActionResult
    {
        /// <summary>
        /// Gets or sets whether the buff was applied to the character.
        /// </summary>
        public bool Applied { get; set; }
    }
}
