using System.Collections.Generic;

namespace BattleSystem.Core.Items
{
    /// <summary>
    /// Represents a character's item slot.
    /// </summary>
    public class ItemSlot
    {
        /// <summary>
        /// The items that have occupied this item slot.
        /// </summary>
        private readonly Stack<Item> Items;

        /// <summary>
        /// Gets whether there is an active item in this item slot.
        /// </summary>
        public bool HasItem => Items.Count > 0 && Current != null;

        /// <summary>
        /// Gets the current item.
        /// </summary>
        public Item Current => Items.Peek();

        /// <summary>
        /// Creates a new <see cref="ItemSlot"/> instance.
        /// </summary>
        public ItemSlot()
        {
            Items = new Stack<Item>();
        }

        /// <summary>
        /// Sets the current item to the given item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Set(Item item)
        {
            Items.Push(item);
        }

        /// <summary>
        /// Removes the current item.
        /// </summary>
        public void Remove()
        {
            Items.Push(null);
        }
    }
}
