using System.Collections.Generic;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Results
{
    /// <summary>
    /// Class for the result of a protect limit change action being applied to a character.
    /// </summary>
    /// <typeparam name="TSource">The type of the source of the protect limit change action.</typeparam>
    public class ProtectLimitChangeResult<TSource> : IActionResult<TSource>
    {
        /// <summary>
        /// Gets or sets whether the protect limit change action was applied to the character.
        /// </summary>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the character who caused the protect limit change action.
        /// </summary>
        public Character User { get; set; }

        /// <summary>
        /// Gets or sets the source of the protect limit change action - for example, the user's item.
        /// </summary>
        public TSource Source { get; set; }

        /// <summary>
        /// Gets or sets the character whose protect limit was changed.
        /// </summary>
        public Character Target { get; set; }

        /// <summary>
        /// Gets whether the protect limit change action was self-inflicted.
        /// </summary>
        public bool IsSelfInflicted => User is not null && Target is not null && User.Id == Target.Id;

        /// <summary>
        /// Gets or sets whether the character was protected from the protect limit change.
        /// </summary>
        public bool TargetProtected { get; set; }

        /// <summary>
        /// Gets or sets the character who protected the target from the protect limit change, if applicable.
        /// </summary>
        public Character ProtectUser { get; set; }

        /// <summary>
        /// Gets or sets the tags associated with this protect limit change.
        /// </summary>
        public HashSet<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the amount that the character's protect limit changed by.
        /// </summary>
        public int Amount { get; set; }
    }
}
