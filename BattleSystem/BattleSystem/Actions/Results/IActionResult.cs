using BattleSystem.Characters;

namespace BattleSystem.Actions.Results
{
    /// <summary>
    /// Interface for the result of a action being applied to a character.
    /// </summary>
    /// <typeparam name="TSource">The type of the source of the action.</typeparam>
    public interface IActionResult<TSource>
    {
        /// <summary>
        /// Gets or sets whether the action was applied to the character.
        /// </summary>
        bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the character who caused the action.
        /// </summary>
        Character User { get; set; }

        /// <summary>
        /// Gets or sets the source of the action - for example, the user's item.
        /// </summary>
        TSource Source { get; set; }

        /// <summary>
        /// Gets or sets the character who was the target of the action.
        /// </summary>
        Character Target { get; set; }

        /// <summary>
        /// Gets or sets whether the character was protected from the action.
        /// </summary>
        public bool TargetProtected { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who protected the target from the action, if applicable.
        /// </summary>
        public string ProtectUserId { get; set; }
    }
}
