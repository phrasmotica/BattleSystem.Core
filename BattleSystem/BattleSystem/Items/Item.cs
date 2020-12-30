using System;
using System.Collections.Generic;
using BattleSystem.Moves;
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
        private readonly List<Func<StatSet, StatSet>> _statsTransforms;

        /// <summary>
        /// The move use transform functions.
        /// </summary>
        private readonly List<Func<MoveUse, MoveUse>> _moveUseTransforms;

        /// <summary>
        /// Creates a new <see cref="Item"/> instance.
        /// </summary>
        public Item()
        {
            _statsTransforms = new List<Func<StatSet, StatSet>>();
            _moveUseTransforms = new List<Func<MoveUse, MoveUse>>();
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
        /// Adds a move use transform function for this item.
        /// </summary>
        /// <param name="transform">The move use transform function to add.</param>
        public void AddMoveUseTransform(Func<MoveUse, MoveUse> transform)
        {
            _moveUseTransforms.Add(transform);
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

        /// <summary>
        /// Returns a transformed copy of the given move use.
        /// </summary>
        /// <param name="moveUse">The move use to transform.</param>
        public MoveUse TransformMoveUse(MoveUse moveUse)
        {
            var transformedMoveUse = moveUse;

            foreach (var t in _moveUseTransforms)
            {
                transformedMoveUse = t(transformedMoveUse);
            }

            return transformedMoveUse;
        }
    }
}
