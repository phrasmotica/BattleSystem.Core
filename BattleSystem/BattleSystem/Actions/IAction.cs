using System.Collections.Generic;
using BattleSystem.Actions.Results;
using BattleSystem.Characters;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Interface for an action.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Sets the targets for the action.
        /// </summary>
        /// <param name="user">The user of the action.</param>
        /// <param name="otherCharacters">The other characters.</param>
        void SetTargets(Character user, IEnumerable<Character> otherCharacters);

        /// <summary>
        /// Applies the effects of the action and returns the results of its application to its targets.
        /// </summary>
        /// <param name="user">The user of the action.</param>
        /// <param name="otherCharacters">The other characters.</param>
        /// <typeparam name="TSource">The type of the source of the action.</typeparam>
        IEnumerable<IActionResult<TSource>> Use<TSource>(Character user, IEnumerable<Character> otherCharacters);
    }
}
