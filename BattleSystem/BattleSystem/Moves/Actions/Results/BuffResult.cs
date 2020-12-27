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

        /// <summary>
        /// Gets or sets the ID of the character who was the target of the buff.
        /// </summary>
        public string TargetId { get; set; }
    }
}
