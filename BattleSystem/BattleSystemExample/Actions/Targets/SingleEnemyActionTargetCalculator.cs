﻿using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Actions.Targets;
using BattleSystemExample.Input;
using BattleSystemExample.Output;

namespace BattleSystemExample.Actions.Targets
{
    /// <summary>
    /// Lets the user choose a single enemy as the action target.
    /// </summary>
    public class SingleEnemyActionTargetCalculator : IActionTargetCalculator
    {
        /// <summary>
        /// The user input.
        /// </summary>
        private readonly IUserInput _userInput;

        /// <summary>
        /// The game output.
        /// </summary>
        private readonly IGameOutput _gameOutput;

        /// <summary>
        /// Creates a new <see cref="SingleEnemyActionTargetCalculator"/> instance.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public SingleEnemyActionTargetCalculator(IUserInput userInput, IGameOutput gameOutput)
        {
            _userInput = userInput;
            _gameOutput = gameOutput;
        }

        /// <inheritdoc />
        public bool IsReactive => false;

        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            if (otherCharacters is null || !otherCharacters.Any())
            {
                return (false, Enumerable.Empty<Character>());
            }

            var targets = otherCharacters.Where(c => c.Team != user.Team).ToArray();
            if (targets.Length == 1)
            {
                return (true, new[] { targets[0] });
            }

            _gameOutput.WriteLine($"Select a target for the move:");
            _gameOutput.WriteLine(GetChoices(targets));

            return (true, new[] { SelectTarget(targets) });
        }

        /// <summary>
        /// Returns a string describing the given characters as potential action targets.
        /// </summary>
        /// <param name="targets">The potential action targets.</param>
        private static string GetChoices(IEnumerable<Character> targets)
        {
            var choices = targets.Select((c, i) => $"{i + 1}: {c.Name}");
            return string.Join("\n", choices);
        }

        /// <summary>
        /// Lets the player select a action target and returns it.
        /// </summary>
        /// <param name="user">The user of the move.</param>
        /// <param name="targets">The potential action targets.</param>
        private Character SelectTarget(IEnumerable<Character> targets)
        {
            Character character = null;

            var validIndexes = targets.Select((_, i) => i + 1);
            int chosenIndex = -1;

            while (!validIndexes.Contains(chosenIndex))
            {
                chosenIndex = _userInput.SelectIndex();

                if (!validIndexes.Contains(chosenIndex))
                {
                    _gameOutput.WriteLine($"Invalid choice! Please enter one of: {string.Join(", ", validIndexes)}");
                    continue;
                }

                // subtract 1 to avoid off-by-one errors
                character = targets.ToArray()[chosenIndex - 1];
            }

            return character;
        }
    }
}