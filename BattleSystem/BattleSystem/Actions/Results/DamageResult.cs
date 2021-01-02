using BattleSystem.Characters;

namespace BattleSystem.Actions.Results
{
    /// <summary>
    /// Class for the result of damage being applied to a character.
    /// </summary>
    /// <typeparam name="TSource">The type of the source of the damage.</typeparam>
    public class DamageResult<TSource> : IActionResult<TSource>
    {
        /// <summary>
        /// Gets or sets whether the damage was applied to the character.
        /// </summary>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the character who caused the damage.
        /// </summary>
        public Character User { get; set; }

        /// <summary>
        /// Gets or sets the source of the damage - for example, the user's item.
        /// </summary>
        public TSource Source { get; set; }

        /// <summary>
        /// Gets or sets the character who was the target of the damage.
        /// </summary>
        public Character Target { get; set; }

        /// <summary>
        /// Gets whether the damage was self-inflicted.
        /// </summary>
        public bool IsSelfInflicted => User is not null && Target is not null && User.Id == Target.Id;

        /// <summary>
        /// Gets or sets the target's health before the damage.
        /// </summary>
        public int StartingHealth { get; set; }

        /// <summary>
        /// Gets or sets the target's health after the damage.
        /// </summary>
        public int EndingHealth { get; set; }

        /// <summary>
        /// Gets the amount of damage taken by the target.
        /// </summary>
        public int Amount => StartingHealth - EndingHealth;

        /// <summary>
        /// Gets whether the target died from the damage.
        /// </summary>
        public bool TargetDied => EndingHealth <= 0;

        /// <summary>
        /// Gets or sets whether the character was protected from the damage.
        /// </summary>
        public bool TargetProtected { get; set; }

        /// <summary>
        /// Gets or sets the character who protected the target from the damage, if applicable.
        /// </summary>
        public Character ProtectUser { get; set; }
    }
}
