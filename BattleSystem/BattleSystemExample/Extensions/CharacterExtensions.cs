using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Items;
using BattleSystemExample.Battles;
using BattleSystemExample.Constants;

namespace BattleSystemExample.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Character"/> and its sub-types.
    /// </summary>
    public static class CharacterExtensions
    {
        /// <summary>
        /// Returns a string containing this given character's name and health.
        /// </summary>
        /// <param name="character">The character.</param>
        public static string Summarise(this Character character)
        {
            return $"{character.Name}: {character.CurrentHealth}/{character.MaxHealth} HP";
        }

        /// <summary>
        /// Ends this character's turn.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="otherCharacters">The other characters.</param>
        public static BattlePhaseResult OnEndTurn(this Character character, IEnumerable<Character> otherCharacters)
        {
            var result = new BattlePhaseResult();

            if (character.HasItem)
            {
                var taggedActions = character.Item.GetCharacterTaggedActions(ActionTags.EndTurn);
                foreach (var a in taggedActions)
                {
                    a.Action.SetTargets(character, otherCharacters);

                    var actionResults = a.Action.Use<Item>(character, otherCharacters);
                    foreach (var r in actionResults)
                    {
                        r.Source = character.Item;
                    }

                    result.ItemActionsResults.Add(actionResults);
                }
            }

            character.ClearProtectQueue();

            return result;
        }
    }
}
