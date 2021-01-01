﻿using BattleSystem.Characters;

namespace BattleSystem.Actions.Results
{
    /// <summary>
    /// Class for the result of a protect action being applied to a character.
    /// </summary>
    /// <typeparam name="TSource">The type of the source of the protect action.</typeparam>
    public class ProtectResult<TSource> : IActionResult<TSource>
    {
        /// <summary>
        /// Gets or sets whether the protect action was applied to the character.
        /// </summary>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the character who caused the protect action.
        /// </summary>
        public Character User { get; set; }

        /// <summary>
        /// Gets or sets the source of the protect action - for example, the user's item.
        /// </summary>
        public TSource Source { get; set; }

        /// <summary>
        /// Gets or sets the character who was the target of the protect.
        /// </summary>
        public Character Target { get; set; }

        /// <summary>
        /// Gets or sets whether the character was protected from the protect.
        /// </summary>
        public bool TargetProtected { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who protected the target from the protect, if applicable.
        /// </summary>
        public string ProtectUserId { get; set; }
    }
}
