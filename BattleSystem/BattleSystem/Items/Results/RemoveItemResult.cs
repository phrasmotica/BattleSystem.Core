namespace BattleSystem.Items.Results
{
    /// <summary>
    /// Represents the result of removing an item from a character.
    /// </summary>
    public class RemoveItemResult
    {
        /// <summary>
        /// Gets or sets whether the removal was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the item that was removed from the character.
        /// </summary>
        public IItem Item { get; set; }
    }
}
