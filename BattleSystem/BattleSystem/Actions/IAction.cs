using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Actions.Results;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Interface for an action.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Applies the effects of the action and returns the results of its application to its targets.
        /// </summary>
        /// <param name="user">The user of the action.</param>
        /// <param name="otherCharacters">The other characters.</param>
        IEnumerable<IActionResult> Use(Character user, IEnumerable<Character> otherCharacters);
    }
}
