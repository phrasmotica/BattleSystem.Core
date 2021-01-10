using System;
using BattleSystem.Core.Actions;
using BattleSystem.Core.Random;
using BattleSystem.Core.Success;

namespace BattleSystem.Core.Actions.Success
{
    /// <summary>
    /// Calculates whether an action succeeds based on a percentage chance.
    /// </summary>
    public class AccuracyActionSuccessCalculator : ISuccessCalculator<IAction, bool>
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
        /// Creates a new <see cref="AccuracyActionSuccessCalculator"/> instance.
        /// </summary>
        /// <param name="accuracy">The accuracy.</param>
        /// <param name="random">The random number generator.</param>
        public AccuracyActionSuccessCalculator(int accuracy, IRandom random)
        {
            _accuracy = accuracy;
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        /// <inheritdoc />
        public bool Calculate(IAction input)
        {
            var r = _random.Next(100) + 1;
            return r <= _accuracy;
        }
    }
}
