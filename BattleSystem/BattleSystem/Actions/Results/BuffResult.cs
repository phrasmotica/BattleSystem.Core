using System.Collections.Generic;
using BattleSystem.Extensions;
using BattleSystem.Stats;

namespace BattleSystem.Actions.Results
{
    /// <summary>
    /// Class for the result of a buff being applied to a character.
    /// </summary>
    public class BuffResult : IMoveActionResult
    {
        /// <summary>
        /// Gets or sets whether the buff was applied to the character.
        /// </summary>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who was the target of the buff.
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// Gets or sets whether the character was protected from the buff.
        /// </summary>
        public bool TargetProtected { get; set; }

        /// <summary>
        /// Gets or sets the ID of the character who protected the target from the buff, if applicable.
        /// </summary>
        public string ProtectUserId { get; set; }

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
