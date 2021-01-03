using System.Collections.Generic;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Heal
{
    /// <summary>
    /// Class for the result of a heal being applied to a character.
    /// </summary>
    /// <typeparam name="TSource">The type of the source of the heal.</typeparam>
    public class HealActionResult<TSource> : IActionResult<TSource>
    {
        /// <summary>
        /// Gets or sets whether the heal was applied to the character.
        /// </summary>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the character who caused the heal.
        /// </summary>
        public Character User { get; set; }

        /// <summary>
        /// Gets or sets the source of the heal - for example, the user's item.
        /// </summary>
        public TSource Source { get; set; }

        /// <summary>
        /// Gets or sets the character who was the target of the heal.
        /// </summary>
        public Character Target { get; set; }

        /// <summary>
        /// Gets whether the heal was self-inflicted.
        /// </summary>
        public bool IsSelfInflicted => User is not null && Target is not null && User.Id == Target.Id;

        /// <summary>
        /// Gets or sets whether the character was protected from the heal.
        /// </summary>
        public bool TargetProtected { get; set; }

        /// <summary>
        /// Gets or sets the character who protected the target from the heal, if applicable.
        /// </summary>
        public Character ProtectUser { get; set; }

        /// <summary>
        /// Gets or sets the tags associated with this heal.
        /// </summary>
        public HashSet<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the target's health before the heal.
        /// </summary>
        public int StartingHealth { get; set; }

        /// <summary>
        /// Gets or sets the target's health after the heal.
        /// </summary>
        public int EndingHealth { get; set; }

        /// <summary>
        /// Gets or sets whether the heal was applied to the character.
        /// </summary>
        public int Amount => EndingHealth - StartingHealth;
    }
}
