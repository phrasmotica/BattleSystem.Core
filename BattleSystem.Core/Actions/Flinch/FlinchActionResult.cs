using System.Collections.Generic;
using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Actions.Flinch
{
    /// <summary>
    /// Class for the result of a flinch action being applied to a character.
    /// </summary>
    /// <typeparam name="TSource">The type of the source of the flinch action.</typeparam>
    public class FlinchActionResult<TSource> : IActionResult<TSource>
    {
        /// <summary>
        /// Gets or sets the flinch action that was executed.
        /// </summary>
        public IAction Action { get; set; }

        /// <summary>
        /// Gets or sets whether the flinch action was applied to the character.
        /// </summary>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the character who caused the flinch action.
        /// </summary>
        public Character User { get; set; }

        /// <summary>
        /// Gets or sets the source of the flinch action - for example, the user's item.
        /// </summary>
        public TSource Source { get; set; }

        /// <summary>
        /// Gets or sets the character who was the target of the flinch action.
        /// </summary>
        public Character Target { get; set; }

        /// <summary>
        /// Gets or sets whether the character was protected from the flinch action.
        /// </summary>
        public bool TargetProtected { get; set; }

        /// <summary>
        /// Gets or sets the character who protected the target from the flinch action, if applicable.
        /// </summary>
        public Character ProtectUser { get; set; }

        /// <summary>
        /// Gets or sets the tags associated with this flinch action.
        /// </summary>
        public HashSet<string> Tags { get; set; }
    }
}
