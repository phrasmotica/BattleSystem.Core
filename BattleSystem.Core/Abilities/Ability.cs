using BattleSystem.Core.Actions;

namespace BattleSystem.Core.Abilities
{
    /// <summary>
    /// Represents an ability that a character possesses.
    /// </summary>
    public class Ability
    {
        /// <summary>
        /// Gets or sets the name of the ability.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the ability.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets this item's action container.
        /// </summary>
        public ActionContainer ActionContainer { get; } = new ActionContainer();
    }
}
