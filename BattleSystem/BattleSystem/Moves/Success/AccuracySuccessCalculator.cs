using System;
using System.Collections.Generic;
using BattleSystem.Characters;

namespace BattleSystem.Moves.Success
{
    /// <summary>
    /// Calculates whether a move succeeds based on a percentage accuracy value.
    /// </summary>
    public class AccuracySuccessCalculator : ISuccessCalculator
    {
        /// <summary>
        /// The accuracy.
        /// </summary>
        private readonly int _accuracy;

        /// <summary>
        /// Creates a new <see cref="AccuracySuccessCalculator"/> instance.
        /// </summary>
        /// <param name="accuracy">The accuracy.</param>
        public AccuracySuccessCalculator(int accuracy)
        {
            _accuracy = accuracy;
        }

        /// <inheritdoc />
        public MoveUseResult Calculate(Character user, Move move, IEnumerable<Character> otherCharacters)
        {
            var r = new Random().Next(100) + 1;
            return r <= _accuracy ? MoveUseResult.Success : MoveUseResult.Miss;
        }
    }
}
