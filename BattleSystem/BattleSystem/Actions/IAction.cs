using System.Collections.Generic;
using BattleSystem.Characters;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Interface for an action.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Gets or sets the tags for the action.
        /// </summary>
        HashSet<string> Tags { get; set; }

        /// <summary>
        /// Sets the targets for the action's next use.
        /// </summary>
        /// <param name="user">The user of the action.</param>
        /// <param name="otherCharacters">The other characters.</param>
        void SetTargets(Character user, IEnumerable<Character> otherCharacters);

        /// <summary>
        /// Applies the action and returns the results of its application to its
        /// targets.
        /// </summary>
        /// <param name="user">The user of the action.</param>
        /// <param name="otherCharacters">The other characters.</param>
        /// <typeparam name="TSource">The type of the source of the action.</typeparam>
        ActionUseResult<TSource> Use<TSource>(Character user, IEnumerable<Character> otherCharacters);
    }
}
