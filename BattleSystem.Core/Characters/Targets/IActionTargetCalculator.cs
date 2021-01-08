using System.Collections.Generic;

namespace BattleSystem.Core.Characters.Targets
{
    /// <summary>
    /// Interface for calculating the targets of an action.
    /// </summary>
    public interface IActionTargetCalculator
    {
        /// <summary>
        /// Gets whether the action target calculator is reactive, i.e. whether
        /// the targets must be computed just before the action is used rather
        /// than them being determined ahead of time.
        /// </summary>
        bool IsReactive { get; }

        /// <summary>
        /// Returns the characters that should be targeted from the given
        /// characters, along with a flag indicating whether the target
        /// calculation was successful.
        /// </summary>
        /// <param name="user">The user of the move.</param>
        /// <param name="otherCharacters">The other characters.</param>
        (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters);
    }
}
