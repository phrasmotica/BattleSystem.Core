﻿using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Extensions;
using BattleSystem.Moves.Actions.Results;
using BattleSystem.Moves.Success;
using BattleSystem.Stats;

namespace BattleSystem.Moves
{
    public class MoveUse
    {
        /// <summary>
        /// Gets or sets the move.
        /// </summary>
        public Move Move { get; set; }

        /// <summary>
        /// Gets or sets the user of the move.
        /// </summary>
        public Character User { get; set; }

        /// <summary>
        /// Gets or sets the characters that did not use the move.
        /// </summary>
        public IEnumerable<Character> OtherCharacters { get; set; }

        /// <summary>
        /// Gets or sets the result of the move use.
        /// </summary>
        public MoveUseResult Result { get; private set; }

        /// <summary>
        /// Gets or sets the results of each action in the move use.
        /// </summary>
        public IEnumerable<IEnumerable<IMoveActionResult>> ActionsResults { get; private set; }

        /// <summary>
        /// The starting health of the characters, keyed by their IDs.
        /// </summary>
        private IDictionary<string, int> CharactersStartingHealth;

        /// <summary>
        /// The ending health of the characters, keyed by their IDs.
        /// </summary>
        private IDictionary<string, int> CharactersEndingHealth;

        /// <summary>
        /// Gets or sets the damage taken by the characters with the given IDs from this move use.
        /// </summary>
        public IDictionary<string, int> DamageTaken => CharactersStartingHealth.Subtract(CharactersEndingHealth, includeZeroes: true);

        /// <summary>
        /// Gets or sets the changes in stat multipliers to the characters with the given IDs from this move use.
        /// </summary>
        public IDictionary<string, IDictionary<StatCategory, double>> StatMultiplierChanges { get; private set; }

        /// <summary>
        /// Applies the move.
        /// </summary>
        public void Apply()
        {
            CharactersStartingHealth = ComputeCharactersHealth();
            var charactersStartingStatMultipliers = ComputeCharactersStatMultipliers();

            ActionsResults = Move.Use(User, OtherCharacters);

            CharactersEndingHealth = ComputeCharactersHealth();
            var charactersEndingStatMultipliers = ComputeCharactersStatMultipliers();

            StatMultiplierChanges = new Dictionary<string, IDictionary<StatCategory, double>>();

            foreach (var entry in charactersStartingStatMultipliers)
            {
                var characterId = entry.Key;
                var startingStatMultipliers = entry.Value;
                var endingStatMultipliers = charactersEndingStatMultipliers[characterId];

                StatMultiplierChanges[characterId] = endingStatMultipliers.Subtract(startingStatMultipliers);
            }
        }

        /// <summary>
        /// Returns whether the character with the given ID died from this move use.
        /// </summary>
        /// <param name="id">The ID of the character.</param>
        public bool CharacterDied(string id)
        {
            if (CharactersEndingHealth.TryGetValue(id, out var health))
            {
                return health <= 0;
            }

            return false;
        }

        /// <summary>
        /// Returns a dictionary of the health of each character keyed by the character's ID.
        /// </summary>
        private IDictionary<string, int> ComputeCharactersHealth()
        {
            var dict = new Dictionary<string, int>
            {
                [User.Id] = User.CurrentHealth
            };

            foreach (var character in OtherCharacters)
            {
                dict[character.Id] = character.CurrentHealth;
            }

            return dict;
        }

        /// <summary>
        /// Returns a dictionary of the stat multipliers of each character keyed by the character's ID.
        /// </summary>
        private IDictionary<string, IDictionary<StatCategory, double>> ComputeCharactersStatMultipliers()
        {
            var dict = new Dictionary<string, IDictionary<StatCategory, double>>
            {
                [User.Id] = GetStatMultipliers(User)
            };

            foreach (var character in OtherCharacters)
            {
                dict[character.Id] = GetStatMultipliers(character);
            }

            return dict;
        }

        /// <summary>
        /// Returns the stat multipliers for the given character as a dictionary.
        /// </summary>
        /// <param name="character">The character.</param>
        private static IDictionary<StatCategory, double> GetStatMultipliers(Character character)
        {
            return new Dictionary<StatCategory, double>
            {
                [StatCategory.Attack] = character.Stats.Attack.Multiplier,
                [StatCategory.Defence] = character.Stats.Defence.Multiplier,
                [StatCategory.Speed] = character.Stats.Speed.Multiplier,
            };
        }
    }
}
