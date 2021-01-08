using System.Collections.Generic;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Moves;

namespace BattleSystem.Abstractions.Control
{
    /// <summary>
    /// Interface for ways a user can control a player.
    /// </summary>
    public interface IUserInput
    {
        /// <summary>
        /// Returns an integer index chosen by the user.
        /// </summary>
        int SelectIndex();

        /// <summary>
        /// Returns one of the given options, chosen by the user.
        /// </summary>
        /// <param name="prompt">The prompt.</param>
        /// <param name="choices">The choices.</param>
        string SelectChoice(string prompt = null, params string[] choices);

        /// <summary>
        /// Returns once the user has confirmed the given prompt.
        /// </summary>
        /// <param name="prompt">The prompt.</param>
        void Confirm(string prompt = null);

        /// <summary>
        /// Selects the move the user will use.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="otherCharacters">The other characters.</param>
        Move SelectMove(Character user, IEnumerable<Character> otherCharacters);

        /// <summary>
        /// Selects the target for an action.
        /// </summary>
        /// <param name="targets">The targets.</param>
        Character SelectTarget(IEnumerable<Character> targets);
    }
}
