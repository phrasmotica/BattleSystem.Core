using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Actions.Results;
using BattleSystem.Moves.Success;

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
        /// Applies the move.
        /// </summary>
        public void Apply()
        {
            (Result, ActionsResults) = Move.Use(User, OtherCharacters);
        }
    }
}
