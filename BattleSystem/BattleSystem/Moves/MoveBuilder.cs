using System;
using BattleSystem.Actions;
using BattleSystem.Moves.Success;

namespace BattleSystem.Moves
{
    /// <summary>
    /// Builder class for moves.
    /// </summary>
    public class MoveBuilder
    {
        /// <summary>
        /// The move to build.
        /// </summary>
        private readonly Move _move;

        /// <summary>
        /// Whether the built move has a name.
        /// </summary>
        private bool _hasName;

        /// <summary>
        /// Whether the built move has a description.
        /// </summary>
        private bool _hasDescription;

        /// <summary>
        /// Whether the built move has max uses.
        /// </summary>
        private bool _hasMaxUses;

        /// <summary>
        /// Whether the built move has a success calculator.
        /// </summary>
        private bool _hasSuccessCalculator;

        /// <summary>
        /// Creates a new <see cref="MoveBuilder"/> instance.
        /// </summary>
        public MoveBuilder()
        {
            _move = new Move();
        }

        /// <summary>
        /// Names the built move.
        /// </summary>
        /// <param name="name">The built move's name.</param>
        public MoveBuilder Name(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Move name must be non-null and non-whitespace!", nameof(name));
            }

            _move.SetName(name);
            _hasName = true;
            return this;
        }

        /// <summary>
        /// Describes the built move.
        /// </summary>
        /// <param name="description">The built move's description.</param>
        public MoveBuilder Describe(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Move description must be non-null and non-whitespace!", nameof(description));
            }

            _move.SetDescription(description);
            _hasDescription = true;
            return this;
        }

        /// <summary>
        /// Sets the built move's max uses.
        /// </summary>
        /// <param name="maxUses">The built move's max uses.</param>
        public MoveBuilder WithMaxUses(int maxUses)
        {
            _move.SetMaxUses(maxUses);
            _hasMaxUses = true;
            return this;
        }

        /// <summary>
        /// Sets the built move's priority.
        /// </summary>
        /// <param name="priority">The built move's priority.</param>
        public MoveBuilder WithPriority(int priority)
        {
            _move.SetPriority(priority);
            return this;
        }

        /// <summary>
        /// Sets the built move's success calculator.
        /// </summary>
        /// <param name="accuracy">The built move's success calculator.</param>
        public MoveBuilder WithSuccessCalculator(ISuccessCalculator successCalculator)
        {
            if (successCalculator is null)
            {
                throw new ArgumentNullException(nameof(successCalculator));
            }

            _move.SetSuccessCalculator(successCalculator);
            _hasSuccessCalculator = true;
            return this;
        }

        /// <summary>
        /// Sets the built move's accuracy.
        /// </summary>
        /// <param name="accuracy">The built move's accuracy.</param>
        public MoveBuilder WithAccuracy(int accuracy)
        {
            return WithSuccessCalculator(new AccuracySuccessCalculator(accuracy));
        }

        /// <summary>
        /// Sets the built move to always succeed.
        /// </summary>
        public MoveBuilder AlwaysSucceeds()
        {
            return WithSuccessCalculator(new AlwaysSuccessCalculator());
        }

        /// <summary>
        /// Adds the given action to the built move.
        /// </summary>
        /// <param name="action">The action to add to the built move.</param>
        public MoveBuilder WithAction(IAction action)
        {
            _move.AddAction(action);
            return this;
        }

        /// <summary>
        /// Returns the built move.
        /// </summary>
        public Move Build()
        {
            if (!_hasName)
            {
                throw new InvalidOperationException("Cannot build a move with no name set!");
            }

            if (!_hasDescription)
            {
                throw new InvalidOperationException("Cannot build a move with no description set!");
            }

            if (!_hasMaxUses)
            {
                throw new InvalidOperationException("Cannot build a move with no max uses set!");
            }

            if (!_hasSuccessCalculator)
            {
                throw new InvalidOperationException("Cannot build a move with no success calculator!");
            }

            return _move;
        }
    }
}
