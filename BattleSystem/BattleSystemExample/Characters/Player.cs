using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Moves;
using BattleSystem.Stats;
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
            _gameOutput.WriteLine($"What will {Name} do?");
            _gameOutput.WriteLine(Moves.Summarise(true));

            var move = SelectMove();

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
        private Move SelectMove()
        {
            Move move = null;

            var validIndexes = Moves.GetIndexes();
            int chosenIndex = -1;

            while (!validIndexes.Contains(chosenIndex) || !(move?.CanUse() ?? false))
            {
                chosenIndex = _userInput.SelectIndex();

                if (!validIndexes.Contains(chosenIndex))
                {
                    _gameOutput.WriteLine($"Invalid choice! Please enter one of: {string.Join(", ", validIndexes)}");
                    continue;
                }

                // chosenIndex is between 1 and 4, so subtract 1 to avoid off-by-one errors
                move = Moves.GetMove(chosenIndex - 1);

                if (!move.CanUse())
                {
                    _gameOutput.WriteLine($"{move.Name} has no uses left! Choose another move");
                }
            }

            return move;
        }
    }
}
