using System;
using BattleSystem.Core.Actions;

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
        /// Sets the given action container for the built item.
        /// </summary>
        /// <param name="transform">The action container for the built item.</param>
        public ItemBuilder WithActionContainer(ActionContainer container)
        {
            _item.ActionContainer = container;
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
