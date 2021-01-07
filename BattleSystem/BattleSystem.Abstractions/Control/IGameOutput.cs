using System.Collections.Generic;
using BattleSystem.Core.Actions;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Items;
using BattleSystem.Core.Moves;

namespace BattleSystem.Abstractions.Control
{
    /// <summary>
    /// Interface for ways the game can output details of a battle.
    /// </summary>
    public interface IGameOutput
    {
        /// <summary>
        /// Shows the start of the given turn.
        /// </summary>
        /// <param name="turnCounter">The turn counter.</param>
        void ShowStartTurn(int turnCounter);

        /// <summary>
        /// Shows a summary of the team containing the given characters.
        /// </summary>
        /// <param name="characters">The character on the team.</param>
        void ShowTeamSummary(IEnumerable<Character> characters);

        /// <summary>
        /// Shows a summary of the given character.
        /// </summary>
        /// <param name="character">The character.</param>
        void ShowCharacterSummary(Character character);

        /// <summary>
        /// Shows a summary of the given move set.
        /// </summary>
        /// <param name="moveSet">The move set.</param>
        void ShowMoveSetSummary(MoveSet moveSet);

        /// <summary>
        /// Shows a summary of the given item.
        /// </summary>
        /// <param name="item">The item.</param>
        void ShowItemSummary(Item item);

        /// <summary>
        /// Shows a blank message.
        /// </summary>
        void ShowMessage();

        /// <summary>
        /// Shows the given message.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowMessage(string message);

        /// <summary>
        /// Shows the given move use.
        /// </summary>
        /// <param name="moveUse">The move use.</param>
        void ShowMoveUse(MoveUse moveUse);

        /// <summary>
        /// Shows the given action result.
        /// </summary>
        /// <typeparam name="TSource">The type of the source of the action result.</typeparam>
        /// <param name="result">The action result.</param>
        void ShowResult<TSource>(IActionResult<TSource> result);

        /// <summary>
        /// Shows the end of the battle.
        /// </summary>
        /// <param name="winningTeam">The name of the winning team.</param>
        void ShowBattleEnd(string winningTeam);
    }
}
