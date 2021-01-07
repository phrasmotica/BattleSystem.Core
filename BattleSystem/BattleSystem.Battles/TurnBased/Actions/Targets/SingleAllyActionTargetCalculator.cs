﻿using System.Collections.Generic;
using System.Linq;
using BattleSystem.Abstractions.Control;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Characters.Targets;

namespace BattleSystem.Battles.TurnBased.Actions.Targets
{
    /// <summary>
    /// Lets the user choose a single ally as the action target.
    /// </summary>
    public class SingleAllyActionTargetCalculator : IActionTargetCalculator
    {
        /// <summary>
        /// The user input.
        /// </summary>
        private readonly IUserInput _userInput;

        /// <summary>
        /// Creates a new <see cref="SingleAllyActionTargetCalculator"/> instance.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        public SingleAllyActionTargetCalculator(IUserInput userInput)
        {
            _userInput = userInput;
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

            var targets = otherCharacters.Where(c => c.Team == user.Team).ToArray();
            if (targets.Length == 1)
            {
                return (true, new[] { targets[0] });
            }

            return (true, new[] { _userInput.SelectTarget(targets) });
        }
    }
}
