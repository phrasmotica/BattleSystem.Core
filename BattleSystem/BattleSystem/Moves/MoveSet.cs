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
        /// Gets or sets the first move.
        /// </summary>
        public Move Move1 { get; set; }

        /// <summary>
        /// Gets or sets the second move.
        /// </summary>
        public Move Move2 { get; set; }

        /// <summary>
        /// Gets or sets the third move.
        /// </summary>
        public Move Move3 { get; set; }

        /// <summary>
        /// Gets or sets the fourth move.
        /// </summary>
        public Move Move4 { get; set; }

        /// <summary>
        /// Returns the choices in this move set as a string.
        /// </summary>
        public string GetChoices()
        {
            var choices = new List<string>();

            if (Move1 is not null)
            {
                choices.Add($"1: {Move1.Summary}");
            }

            if (Move2 is not null)
            {
                choices.Add($"2: {Move2.Summary}");
            }

            if (Move3 is not null)
            {
                choices.Add($"3: {Move3.Summary}");
            }

            if (Move4 is not null)
            {
                choices.Add($"4: {Move4.Summary}");
            }

            return string.Join("\n", choices);
        }

        /// <summary>
        /// Returns the choices of index in this move set.
        /// </summary>
        public IEnumerable<int> GetIndexes()
        {
            var indexes = new List<int>();

            if (Move1 is not null)
            {
                indexes.Add(1);
            }

            if (Move2 is not null)
            {
                indexes.Add(2);
            }

            if (Move3 is not null)
            {
                indexes.Add(3);
            }

            if (Move4 is not null)
            {
                indexes.Add(4);
            }

            return indexes;
        }

        /// <summary>
        /// Returns a random move in the set.
        /// </summary>
        public Move ChooseRandom()
        {
            var moves = new[] { Move1, Move2, Move3, Move4 };
            var indexes = moves.Select((m, i) => m is not null ? i : -1).Where(i => i > -1).ToArray();
            var indexOfIndex = new Random().Next(indexes.Length);
            return GetMove(indexes[indexOfIndex]);
        }

        /// <summary>
        /// Returns the move for the given index.
        /// </summary>
        /// <param name="index">The index.</param>
        public Move GetMove(int index)
        {
            return index switch
            {
                0 => Move1,
                1 => Move2,
                2 => Move3,
                3 => Move4,
                _ => throw new ArgumentException($"No move matches index {index}!")
            };
        }
    }
}
