using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Actions.Results;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Interface for a move action.
    /// </summary>
    public interface IMoveAction
    {
        /// <summary>
        /// Applies the effects of the move action and returns the results of its application to its targets.
        /// </summary>
        /// <param name="user">The user of the move.</param>
        /// <param name="otherCharacters">The other characters.</param>
        IEnumerable<IMoveActionResult> Use(Character user, IEnumerable<Character> otherCharacters);
    }
}
