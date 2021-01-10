using System;
using BattleSystem.Core.Actions;
using static BattleSystem.Core.Actions.ActionContainer;
using static BattleSystem.Core.Actions.Damage.Calculators.BasePowerDamageCalculator;
using static BattleSystem.Core.Items.Item;

namespace BattleSystem.Core.Items
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

            _item.Name = name;
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

            _item.Description = description;
            _hasDescription = true;
            return this;
        }

        /// <summary>
        /// Adds the given attack value transform to the built item.
        /// </summary>
        /// <param name="transform">The attack value transform for the built item.</param>
        public ItemBuilder WithAttackValueTransform(StatValueTransform transform)
        {
            _item.ActionContainer.AddAttackValueTransform(transform);
            return this;
        }

        /// <summary>
        /// Adds the given defence value transform to the built item.
        /// </summary>
        /// <param name="transform">The defence value transform for the built item.</param>
        public ItemBuilder WithDefenceValueTransform(StatValueTransform transform)
        {
            _item.ActionContainer.AddDefenceValueTransform(transform);
            return this;
        }

        /// <summary>
        /// Adds the given speed value transform to the built item.
        /// </summary>
        /// <param name="transform">The speed value transform for the built item.</param>
        public ItemBuilder WithSpeedValueTransform(StatValueTransform transform)
        {
            _item.ActionContainer.AddSpeedValueTransform(transform);
            return this;
        }

        /// <summary>
        /// Adds a stats transform for increasing attack by the given factor to the built item.
        /// </summary>
        public ItemBuilder WithIncreaseAttack(double factor = 0.1)
        {
            return WithAttackValueTransform(a => (int) (a * (1 + factor)));
        }

        /// <summary>
        /// Adds the given damage power transform to the built item.
        /// </summary>
        /// <param name="transform">The damage power transform for the built item.</param>
        public ItemBuilder WithDamagePowerTransform(PowerTransform transform)
        {
            _item.ActionContainer.AddDamagePowerTransform(transform);
            return this;
        }

        /// <summary>
        /// Adds a tagged action to the built item.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="tags">The tags.</param>
        public ItemBuilder WithTaggedAction(IAction action, params string[] tags)
        {
            _item.ActionContainer.AddTaggedAction(action, tags);
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
