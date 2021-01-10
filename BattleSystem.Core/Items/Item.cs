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
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the description of the item.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets or sets this item's action container.
        /// </summary>
        public ActionContainer ActionContainer { get; } = new ActionContainer();

        /// <summary>
        /// Sets the name for this item.
        /// </summary>
        /// <param name="name">The name.</param>
        public void SetName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Sets the description for this item.
        /// </summary>
        /// <param name="description">The description.</param>
        public void SetDescription(string description)
        {
            Description = description;
        }
    }
}
