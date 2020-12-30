using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Actions.Results;
using BattleSystem.Items;

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

        /// <summary>
        /// Receives the relevant transforms from the given item.
        /// </summary>
        /// <param name="item">The item.</param>
        void ReceiveTransforms(Item item);

        /// <summary>
        /// Clears any transforms this action may have received.
        /// </summary>
        void ClearTransforms();
    }
}
