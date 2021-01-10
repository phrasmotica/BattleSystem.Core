using System;
using BattleSystem.Core.Actions;

namespace BattleSystem.Core.Abilities
{
    /// <summary>
    /// Builder class for abilities.
    /// </summary>
    public class AbilityBuilder
    {
        /// <summary>
        /// The ability to build.
        /// </summary>
        private readonly Ability _ability;

        /// <summary>
        /// Whether the built ability has a name.
        /// </summary>
        private bool _hasName;

        /// <summary>
        /// Whether the built ability has a description.
        /// </summary>
        private bool _hasDescription;

        /// <summary>
        /// Creates a new <see cref="AbilityBuilder"/> instance.
        /// </summary>
        public AbilityBuilder()
        {
            _ability = new Ability();
        }

        /// <summary>
        /// Names the built ability.
        /// </summary>
        /// <param name="name">The built ability's name.</param>
        public AbilityBuilder Name(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Ability name must be non-null and non-whitespace!", nameof(name));
            }

            _ability.Name = name;
            _hasName = true;
            return this;
        }

        /// <summary>
        /// Describes the built ability.
        /// </summary>
        /// <param name="description">The built ability's description.</param>
        public AbilityBuilder Describe(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Ability description must be non-null and non-whitespace!", nameof(description));
            }

            _ability.Description = description;
            _hasDescription = true;
            return this;
        }

        /// <summary>
        /// Sets the given action container for the built ability.
        /// </summary>
        /// <param name="transform">The action container for the built ability.</param>
        public AbilityBuilder WithActionContainer(ActionContainer container)
        {
            _ability.ActionContainer = container;
            return this;
        }

        /// <summary>
        /// Returns the built ability.
        /// </summary>
        public Ability Build()
        {
            if (!_hasName)
            {
                throw new InvalidOperationException("Cannot build an ability with no name set!");
            }

            if (!_hasDescription)
            {
                throw new InvalidOperationException("Cannot build an ability with no description set!");
            }

            return _ability;
        }
    }
}
