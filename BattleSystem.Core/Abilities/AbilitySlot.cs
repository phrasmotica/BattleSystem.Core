using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Abilities
{
    /// <summary>
    /// Represents a character's ability slot.
    /// </summary>
    public class AbilitySlot : Slot<Ability>
    {
        /// <summary>
        /// Gets whether there is an active ability in this ability slot.
        /// </summary>
        public bool HasAbility => HasAsset;
    }
}
