using System;
using BattleSystem.Core.Actions;
using BattleSystem.Core.Moves.Success;
using BattleSystem.Core.Random;
using static BattleSystem.Core.Moves.Move;

namespace BattleSystem.Core.Moves
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
        private bool _hasSuccessCalculatorFactory;

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
        /// Sets the built move's success calculator factory.
        /// </summary>
        /// <param name="accuracy">The built move's success calculator factory.</param>
        public MoveBuilder WithSuccessCalculatorFactory(MoveSuccessCalculatorFactory successCalculatorFactory)
        {
            if (successCalculatorFactory is null)
            {
                throw new ArgumentNullException(nameof(successCalculatorFactory));
            }

            _move.SetSuccessCalculatorFactory(successCalculatorFactory);
            _hasSuccessCalculatorFactory = true;
            return this;
        }

        /// <summary>
        /// Sets the built move's accuracy.
        /// </summary>
        /// <param name="accuracy">The built move's accuracy.</param>
        /// <param name="random">The random number generator.</param>
        public MoveBuilder WithAccuracy(int accuracy, IRandom random)
        {
            return WithSuccessCalculatorFactory((_, __) => new AccuracyMoveSuccessCalculator(accuracy, random));
        }

        /// <summary>
        /// Sets the built move to always succeed.
        /// </summary>
        public MoveBuilder AlwaysSucceeds()
        {
            return WithSuccessCalculatorFactory((_, __) => new AlwaysMoveSuccessCalculator());
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

            if (!_hasSuccessCalculatorFactory)
            {
                throw new InvalidOperationException("Cannot build a move with no success calculator factory!");
            }

            return _move;
        }
    }
}
