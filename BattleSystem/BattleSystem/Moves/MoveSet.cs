using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleSystem.Moves
{
    /// <summary>
    /// Represents a set of moves.
    /// </summary>
    public class MoveSet
    {
        /// <summary>
        /// Gets or sets the moves in the set.
        /// </summary>
        public IList<Move> Moves { get; private set; }

        /// <summary>
        /// Creates a new <see cref="MoveSet"/> instance.
        /// </summary>
        public MoveSet()
        {
            Moves = new List<Move>();
        }

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
        /// Returns the choices in this move set as a string.
        /// </summary>
        public string GetChoices()
        {
            var choices = Moves.Select((move, index) => $"{index + 1}: {move.Summary}");
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
        public Move ChooseRandom()
        {
            return GetMove(new Random().Next(Moves.Count));
        }
    }
}
