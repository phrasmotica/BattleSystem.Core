using System.Collections.Generic;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Extensions;
using BattleSystem.Core.Stats;

namespace BattleSystem.Core.Actions.Buff
{
    /// <summary>
    /// Class for the result of a buff being applied to a character.
    /// </summary>
    /// <typeparam name="TSource">The type of the source of the buff.</typeparam>
    public class BuffActionResult<TSource> : IActionResult<TSource>
    {
        /// <summary>
        /// Gets or sets the buff that was executed.
        /// </summary>
        public IAction Action { get; set; }

        /// <summary>
        /// Gets or sets whether the buff was applied to the character.
        /// </summary>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the character who caused the buff.
        /// </summary>
        public Character User { get; set; }

        /// <summary>
        /// Gets or sets the source of the buff - for example, the user's item.
        /// </summary>
        public TSource Source { get; set; }

        /// <summary>
        /// Gets or sets the character who was the target of the buff.
        /// </summary>
        public Character Target { get; set; }

        /// <summary>
        /// Gets whether the buff was self-inflicted.
        /// </summary>
        public bool IsSelfInflicted => User != null && Target != null && User.Id == Target.Id;

        /// <summary>
        /// Gets or sets whether the character was protected from the buff.
        /// </summary>
        public bool TargetProtected { get; set; }

        /// <summary>
        /// Gets or sets the character who protected the target from the buff, if applicable.
        /// </summary>
        public Character ProtectUser { get; set; }

        /// <summary>
        /// Gets or sets the tags associated with this buff.
        /// </summary>
        public HashSet<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the changes in stat multipliers resulting from this buff.
        /// </summary>
        public IDictionary<StatCategory, double> StartingStatMultipliers { get; set; } = new Dictionary<StatCategory, double>();

        /// <summary>
        /// Gets or sets the changes in stat multipliers resulting from this buff.
        /// </summary>
        public IDictionary<StatCategory, double> EndingStatMultipliers { get; set; } = new Dictionary<StatCategory, double>();

        /// <summary>
        /// Gets or sets the changes in stat multipliers resulting from this buff.
        /// </summary>
        public IDictionary<StatCategory, double> StatMultiplierChanges => EndingStatMultipliers.Subtract(StartingStatMultipliers);
    }
}
