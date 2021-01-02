using System.Collections.Generic;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Targets
{
    /// <summary>
    /// Interface for calculating the targets of an action.
    /// </summary>
    public interface IActionTargetCalculator
    {
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
