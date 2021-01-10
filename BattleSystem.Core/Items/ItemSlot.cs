using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Items
{
    /// <summary>
    /// Represents a character's item slot.
    /// </summary>
    public class ItemSlot : Slot<Item>
    {
        /// <summary>
        /// Gets whether there is an active item in this item slot.
        /// </summary>
        public bool HasItem => HasAsset;
    }
}
