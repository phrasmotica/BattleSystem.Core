using System;
using BattleSystem.Actions;
using BattleSystem.Characters;
using static BattleSystem.Actions.Attack;
using static BattleSystem.Items.Item;

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
        /// Adds the given attack base value transform to the built item.
        /// </summary>
        /// <param name="transform">The attack base value transform for the built item.</param>
        public ItemBuilder WithAttackBaseValueTransform(StatBaseValueTransform transform)
        {
            _item.AddAttackBaseValueTransform(transform);
            return this;
        }

        /// <summary>
        /// Adds the given defence base value transform to the built item.
        /// </summary>
        /// <param name="transform">The defence base value transform for the built item.</param>
        public ItemBuilder WithDefenceBaseValueTransform(StatBaseValueTransform transform)
        {
            _item.AddDefenceBaseValueTransform(transform);
            return this;
        }

        /// <summary>
        /// Adds the given speed base value transform to the built item.
        /// </summary>
        /// <param name="transform">The speed base value transform for the built item.</param>
        public ItemBuilder WithSpeedBaseValueTransform(StatBaseValueTransform transform)
        {
            _item.AddSpeedBaseValueTransform(transform);
            return this;
        }

        /// <summary>
        /// Adds a stats transform for increasing attack by the given factor to the built item.
        /// </summary>
        public ItemBuilder WithIncreaseAttack(double factor = 0.1)
        {
            return WithAttackBaseValueTransform(a => (int) (a * (1 + factor)));
        }

        /// <summary>
        /// Adds the given attack power transform to the built item.
        /// </summary>
        /// <param name="transform">The attack power transform for the built item.</param>
        public ItemBuilder WithAttackPowerTransform(PowerTransform transform)
        {
            _item.AddAttackPowerTransform(transform);
            return this;
        }

        /// <summary>
        /// Adds a tagged action to the built item.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="tags">The tags.</param>
        public ItemBuilder WithTaggedAction(IAction action, params string[] tags)
        {
            _item.AddTaggedAction(action, tags);
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
