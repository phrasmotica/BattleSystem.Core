using BattleSystem.Core.Actions;

namespace BattleSystem.Core.Items
{
    /// <summary>
    /// Represents an item that a character holds in battle.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets this item's action container.
        /// </summary>
        public ActionContainer ActionContainer { get; } = new ActionContainer();
    }
}
