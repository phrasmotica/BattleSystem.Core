using System;
using BattleSystem.Core.Random;
using BattleSystem.Core.Success;

namespace BattleSystem.Core.Moves.Success
{
    /// <summary>
    /// Calculates whether a move succeeds based on a percentage accuracy value.
    /// </summary>
    public class AccuracyMoveSuccessCalculator : ISuccessCalculator<Move, MoveUseResult>
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
        /// Creates a new <see cref="AccuracyMoveSuccessCalculator"/> instance.
        /// </summary>
        /// <param name="accuracy">The accuracy.</param>
        /// <param name="random">The random number generator.</param>
        public AccuracyMoveSuccessCalculator(int accuracy, IRandom random)
        {
            _accuracy = accuracy;
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        /// <inheritdoc />
        public MoveUseResult Calculate(Move move)
        {
            var r = _random.Next(100) + 1;
            return r <= _accuracy ? MoveUseResult.Success : MoveUseResult.Miss;
        }
    }
}
