using System;
using BattleSystem.Moves;
using BattleSystem.Stats;

namespace BattleSystem.Items
{
    /// <summary>
    /// Builder class for items.
    /// </summary>
    public class ItemBuilder
    {
        /// <summary>
        /// The item to build.
        /// </summary>
        private readonly Item _item;

        /// <summary>
        /// Whether the built item has a name.
        /// </summary>
        private bool _hasName;

        /// <summary>
        /// Whether the built item has a description.
        /// </summary>
        private bool _hasDescription;

        /// <summary>
        /// Creates a new <see cref="ItemBuilder"/> instance.
        /// </summary>
        public ItemBuilder()
        {
            _item = new Item();
        }

        /// <summary>
        /// Names the built item.
        /// </summary>
        /// <param name="name">The built item's name.</param>
        public ItemBuilder Name(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Item name must be non-null and non-whitespace!", nameof(name));
            }

            _item.SetName(name);
            _hasName = true;
            return this;
        }

        /// <summary>
        /// Describes the built item.
        /// </summary>
        /// <param name="description">The built item's description.</param>
        public ItemBuilder Describe(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Item description must be non-null and non-whitespace!", nameof(description));
            }

            _item.SetDescription(description);
            _hasDescription = true;
            return this;
        }

        /// <summary>
        /// Adds the given stats transform to the built item.
        /// </summary>
        /// <param name="transform">The stats transform for the built item.</param>
        public ItemBuilder WithStatsTransform(Func<StatSet, StatSet> transform)
        {
            _item.AddStatsTransform(transform);
            return this;
        }

        /// <summary>
        /// Adds a stats transform for increasing attack by the given factor to the built item.
        /// </summary>
        public ItemBuilder WithIncreaseAttack(double factor = 0.1)
        {
            return WithStatsTransform(ss =>
            {
                return new StatSet
                {
                    Attack = new Stat((int) (ss.Attack.BaseValue * (1 + factor)), ss.Defence.Multiplier),
                    Defence = new Stat(ss.Defence.BaseValue, ss.Defence.Multiplier),
                    Speed = new Stat(ss.Speed.BaseValue, ss.Speed.Multiplier),
                };
            });
        }

        /// <summary>
        /// Adds the given move use transform to the built item.
        /// </summary>
        /// <param name="transform">The move use transform for the built item.</param>
        public ItemBuilder WithMoveUseTransform(Func<MoveUse, MoveUse> transform)
        {
            _item.AddMoveUseTransform(transform);
            return this;
        }

        /// <summary>
        /// Returns the built item.
        /// </summary>
        public Item Build()
        {
            if (!_hasName)
            {
                throw new InvalidOperationException("Cannot build an item with no name set!");
            }

            if (!_hasDescription)
            {
                throw new InvalidOperationException("Cannot build an item with no description set!");
            }

            return _item;
        }
    }
}
