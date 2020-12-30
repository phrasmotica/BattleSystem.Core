using System.Collections.Generic;
using BattleSystem.Actions;
using BattleSystem.Moves;
using BattleSystem.Stats;
using static BattleSystem.Actions.Attack;

namespace BattleSystem.Items
{
    /// <summary>
    /// Represents an item that a character holds in battle.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Delegate for a function that transforms the given stat set.
        /// </summary>
        /// <param name="stats">The stat set.</param>
        public delegate StatSet StatSetTransform(StatSet stats);

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the description of the item.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The stats transform functions.
        /// </summary>
        private readonly List<StatSetTransform> _statsTransforms;

        /// <summary>
        /// The attack transform functions.
        /// </summary>
        public List<PowerTransform> AttackPowerTransforms { get; private set; }

        /// <summary>
        /// Creates a new <see cref="Item"/> instance.
        /// </summary>
        public Item()
        {
            _statsTransforms = new List<StatSetTransform>();
            AttackPowerTransforms = new List<PowerTransform>();
        }

        /// <summary>
        /// Sets the name for this item.
        /// </summary>
        /// <param name="name">The name.</param>
        public void SetName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Sets the description for this item.
        /// </summary>
        /// <param name="description">The description.</param>
        public void SetDescription(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Adds a stats transform function for this item.
        /// </summary>
        /// <param name="transform">The stats transform function to add.</param>
        public void AddStatsTransform(StatSetTransform transform)
        {
            _statsTransforms.Add(transform);
        }

        /// <summary>
        /// Adds an attack power transform function for this item.
        /// </summary>
        /// <param name="transform">The attack power transform function to add.</param>
        public void AddAttackPowerTransform(PowerTransform transform)
        {
            AttackPowerTransforms.Add(transform);
        }

        /// <summary>
        /// Returns a transformed copy of the given stat set.
        /// </summary>
        /// <param name="stats">The stat set to transform.</param>
        public StatSet TransformStats(StatSet stats)
        {
            var transformedStats = stats;

            foreach (var t in _statsTransforms)
            {
                transformedStats = t(transformedStats);
            }

            return transformedStats;
        }
    }
}
