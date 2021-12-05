using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleSystem.Core.Moves
{
    /// <summary>
    /// Represents a set of moves.
    /// </summary>
    public class MoveSet
    {
        /// <summary>
        /// Creates a new <see cref="MoveSet"/> instance.
        /// </summary>
        public MoveSet()
        {
            Moves = new List<Move>();
        }

        /// <summary>
        /// Gets or sets the moves in the set.
        /// </summary>
        public IList<Move> Moves { get; private set; }

        /// <summary>
        /// Adds the given move to the set.
        /// </summary>
        /// <param name="move">The move to add.</param>
        public void AddMove(Move move)
        {
            if (move is null)
            {
                throw new ArgumentNullException(nameof(move));
            }

            Moves.Add(move);
        }

        /// <summary>
        /// Returns the move for the given index.
        /// </summary>
        /// <param name="index">The index.</param>
        public Move GetMove(int index)
        {
            if (index < 0)
            {
                throw new ArgumentException($"Invalid negative index {index}!");
            }

            if (index >= Moves.Count)
            {
                throw new ArgumentException($"Invalid index {index}: there are only {Moves.Count} moves in the set!");
            }

            return Moves[index];
        }

        /// <summary>
        /// Returns the choices in this move set as a string. Optionally prepends each choice with a
        /// numeric index.
        /// </summary>
        public string Summarise(bool includeIndexes = false)
        {
            var choices = Moves.Select(move => move.Summary);

            if (includeIndexes)
            {
                choices = choices.Select((choice, index) => $"{index + 1}: {choice}");
            }

            return string.Join("\n", choices);
        }

        /// <summary>
        /// Returns the choices of index in this move set.
        /// </summary>
        public IEnumerable<int> GetIndexes()
        {
            return Moves.Select((_, index) => index + 1);
        }

        /// <summary>
        /// Returns a random move in the set.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        public Move ChooseRandom(Random random)
        {
            return GetMove(random.Next(Moves.Count));
        }
    }
}
