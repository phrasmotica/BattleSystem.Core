using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Items
{
    /// <summary>
    /// Represents a character's item slot.
    /// </summary>
    public class ItemSlot
    {
        private readonly Slot<Item> _slot;

        public ItemSlot(Slot<Item> slot = null)
        {
            _slot = slot ?? new Slot<Item>();
        }

        public bool HasItem => _slot.HasAsset;
        public Item Current => _slot.Current;

        public void Set(Item item) => _slot.Set(item);
        public void Remove() => _slot.Remove();
    }
}
