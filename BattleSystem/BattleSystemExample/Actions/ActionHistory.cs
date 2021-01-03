using System.Collections.Generic;
using System.Linq;
using BattleSystem.Actions.Results;
using BattleSystem.Characters;
using BattleSystem.Items;
using BattleSystem.Moves;

namespace BattleSystemExample.Actions
{
    /// <summary>
    /// Records the results of actions that have been applied to some target.
    /// </summary>
    public class ActionHistory
    {
        /// <summary>
        /// Gets or sets the turn counter.
        /// </summary>
        public int TurnCounter { get; private set; }

        /// <summary>
        /// Gets or sets the results of actions applied to this target by moves.
        /// </summary>
        public List<(int turnNumber, IActionResult<Move> result)> MoveActions { get; private set; }

        /// <summary>
        /// Gets or sets the results of actions applied to this target by items.
        /// </summary>
        public List<(int turnNumber, IActionResult<Item> result)> ItemActions { get; private set; }

        /// <summary>
        /// Creates a new <see cref="ActionHistory"/> instance.
        /// </summary>
        public ActionHistory()
        {
            MoveActions = new List<(int, IActionResult<Move>)>();
            ItemActions = new List<(int, IActionResult<Item>)>();
        }

        /// <summary>
        /// Starts the turn.
        /// </summary>
        public void StartTurn()
        {
            TurnCounter++;
        }

        /// <summary>
        /// Adds the given action to the move action history.
        /// </summary>
        /// <typeparam name="TSource">The type of the object that caused the action.</typeparam>
        /// <param name="result">The result of the action.</param>
        /// <param name="turnNumber">The turn number of the action being used.</param>
        public void AddAction<TSource>(IActionResult<TSource> result)
        {
            switch (result)
            {
                case IActionResult<Move> moveResult:
                    MoveActions.Add((TurnCounter, moveResult));
                    break;

                case IActionResult<Item> itemResult:
                    ItemActions.Add((TurnCounter, itemResult));
                    break;
            }
        }

        /// <summary>
        /// Gets the most recent damage action from a move that affected the
        /// given character on the current turn.
        /// </summary>
        /// <param name="character">The affected character.</param>
        /// <param name="turnNumber">The turn number.</param>
        public DamageResult<Move> LastMoveDamageResultAgainst(Character character)
        {
            return MoveActions.Where(a => a.turnNumber == TurnCounter)
                              .Select(a => a.result)
                              .Where(a => a.Target == character)
                              .Where(a => a.Applied)
                              .Where(a => a is DamageResult<Move>)
                              .LastOrDefault() as DamageResult<Move>;
        }
    }
}
