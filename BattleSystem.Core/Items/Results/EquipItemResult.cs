namespace BattleSystem.Core.Items.Results
{
    /// <summary>
    /// Represents the result of equipping an item to a character.
    /// </summary>
    public class EquipItemResult
    {
        /// <summary>
        /// Gets or sets whether the equipping was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the item that was previously equipped to the character.
        /// </summary>
        public Item PreviousItem { get; set; }

        /// <summary>
        /// Gets or sets whether the character had an item before the new one was equipped.
        /// </summary>
        public bool HadPreviousItem => PreviousItem is not null;
    }
}
