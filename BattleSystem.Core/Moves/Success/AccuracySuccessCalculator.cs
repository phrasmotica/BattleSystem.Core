using System;
using System.Collections.Generic;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Random;

namespace BattleSystem.Core.Moves.Success
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
        /// The random number generator.
        /// </summary>
        private readonly IRandom _random;

        /// <summary>
        /// Creates a new <see cref="AccuracySuccessCalculator"/> instance.
        /// </summary>
        /// <param name="accuracy">The accuracy.</param>
        /// <param name="random">The random number generator.</param>
        public AccuracySuccessCalculator(int accuracy, IRandom random)
        {
            _accuracy = accuracy;
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        /// <inheritdoc />
        public MoveUseResult Calculate(Character user, Move move, IEnumerable<Character> otherCharacters)
        {
            var r = _random.Next(100) + 1;
            return r <= _accuracy ? MoveUseResult.Success : MoveUseResult.Miss;
        }
    }
}
