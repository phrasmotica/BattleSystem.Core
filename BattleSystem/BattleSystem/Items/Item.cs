using System;
using System.Collections.Generic;
using BattleSystem.Stats;

namespace BattleSystem.Items
{
    /// <summary>
    /// Represents an item that a character holds in battle.
    /// </summary>
    public class Item
    {
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
        private List<Func<StatSet, StatSet>> _statsTransforms;

        /// <summary>
        /// Creates a new <see cref="Item"/> instance.
        /// </summary>
        public Item()
        {
            _statsTransforms = new List<Func<StatSet, StatSet>>();
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
        public void AddStatsTransform(Func<StatSet, StatSet> transform)
        {
            _statsTransforms.Add(transform);
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
