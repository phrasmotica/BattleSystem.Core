using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Abilities
{
    /// <summary>
    /// Represents a character's ability slot.
    /// </summary>
    public class AbilitySlot
    {
        private readonly Slot<Ability> _slot;

        public AbilitySlot(Slot<Ability> slot = null)
        {
            _slot = slot ?? new Slot<Ability>();
        }

        public bool HasAbility => _slot.HasAsset;
        public Ability Current => _slot.Current;

        public void Set(Ability ability) => _slot.Set(ability);
        public void Remove() => _slot.Remove();
    }
}
