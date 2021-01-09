using System.Collections.Generic;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Moves.Success;
using System.Linq;
using BattleSystem.Core.Actions;

namespace BattleSystem.Core.Moves
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
        public MoveUseResult? Result { get; set; }

        /// <summary>
        /// Gets whether the move use has a result.
        /// </summary>
        public bool HasResult => Result.HasValue;

        /// <summary>
        /// Gets whether the move use was successful.
        /// </summary>
        public bool Success => Result == MoveUseResult.Success;

        /// <summary>
        /// Gets whether the user of the move flinched.
        /// </summary>
        public bool UserFlinched => Result == MoveUseResult.Flinched;

        /// <summary>
        /// Gets or sets the result of each action use in the move use.
        /// </summary>
        public IEnumerable<ActionUseResult<Move>> ActionsResults { get; set; }

        /// <summary>
        /// Gets whether all the targets of the actions in this move use were
        /// dead before the move was used.
        /// </summary>
        public bool TargetsAllDead
        {
            get
            {
                return Result == MoveUseResult.Success
                    && ActionsResults is not null
                    && ActionsResults.All(ars => ars.Success && !ars.Results.Any());
            }
        }

        /// <summary>
        /// Sets the targets.
        /// </summary>
        public void SetTargets()
        {
            Move.SetTargets(User, OtherCharacters);
        }

        /// <summary>
        /// Applies the move.
        /// </summary>
        public void Apply()
        {
            (Result, ActionsResults) = Move.Use(User, OtherCharacters);
        }
    }
}
