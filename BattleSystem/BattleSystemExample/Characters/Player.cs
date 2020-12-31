using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Moves;
using BattleSystem.Stats;
using BattleSystemExample.Extensions;
using BattleSystemExample.Input;
using BattleSystemExample.Output;

namespace BattleSystemExample.Characters
{
    /// <summary>
    /// Class representing a user-controller player.
    /// </summary>
    public class Player : Character
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
        /// Creates a new <see cref="Player"/> instance.
        /// </summary>
        public Player(
            IUserInput userInput,
            IGameOutput gameOutput,
            string name,
            string team,
            int maxHealth,
            StatSet stats,
            MoveSet moves) : base(name, team, maxHealth, stats, moves)
        {
            _userInput = userInput;
            _gameOutput = gameOutput;
        }

        /// <inheritdoc/>
        public override MoveUse ChooseMove(IEnumerable<Character> otherCharacters)
        {
            var move = SelectMove(otherCharacters);

            return new MoveUse
            {
                Move = move,
                User = this,
                OtherCharacters = otherCharacters,
            };
        }

        /// <summary>
        /// Lets the player select a move and returns it.
        /// </summary>
        /// <param name="otherCharacters">The other characters in the battle.</param>
        private Move SelectMove(IEnumerable<Character> otherCharacters)
        {
            Move move = null;

            var validIndexes = Moves.GetIndexes();
            int chosenIndex = -1;

            var allCharacters = otherCharacters.Prepend(this).ToArray();

            while (!validIndexes.Contains(chosenIndex) || !(move?.CanUse() ?? false))
            {
                _gameOutput.WriteLine();
                _gameOutput.WriteLine($"What will {Name} do?");
                _gameOutput.WriteLine(Moves.Summarise(true));
                _gameOutput.WriteLine();

                var inspectChoices = allCharacters.Select((_, i) => $"inspect {i + 1}").ToArray();

                var input = _userInput.ReadLine();
                if (input == "view")
                {
                    ViewCharacters(otherCharacters);
                    continue;
                }

                var index = Array.IndexOf(inspectChoices, input);
                if (index > -1)
                {
                    InspectPlayer(allCharacters[index]);
                    continue;
                }

                var inputIsValid = int.TryParse(input, out chosenIndex);

                if (!validIndexes.Contains(chosenIndex))
                {
                    _gameOutput.WriteLine($"Invalid choice! Please enter one of: {string.Join(", ", validIndexes)}");
                    continue;
                }

                // chosenIndex starts from 1, so subtract 1 to avoid off-by-one errors
                move = Moves.GetMove(chosenIndex - 1);

                if (!move.CanUse())
                {
                    _gameOutput.WriteLine($"{move.Name} has no uses left! Choose another move");
                }
            }

            return move;
        }

        /// <summary>
        /// Views a summary of all the characters.
        /// </summary>
        private void ViewCharacters(IEnumerable<Character> otherCharacters)
        {
            var allCharacters = otherCharacters.Prepend(this).ToArray();
            var teams = allCharacters.GroupBy(c => c.Team).ToArray();

            foreach (var team in teams)
            {
                _gameOutput.WriteLine();

                foreach (var c in team.Where(c => !c.IsDead))
                {
                    _gameOutput.WriteLine(c.Summarise());
                }
            }
        }

        /// <summary>
        /// Inspects the given character.
        /// </summary>
        /// <param name="character">The character.</param>
        private void InspectPlayer(Character character)
        {
            _gameOutput.WriteLine();
            _gameOutput.WriteLine(character.Summarise());

            if (character.Team == Team)
            {
                _gameOutput.WriteLine();
                _gameOutput.WriteLine("Moves:");
                _gameOutput.WriteLine(character.Moves.Summarise());

                if (character.HasItem)
                {
                    _gameOutput.WriteLine();
                    _gameOutput.WriteLine("Item:");
                    _gameOutput.WriteLine(character.Item.Summarise());
                }
            }
        }
    }
}
