using System.Collections.Generic;
using System.Linq;
using BattleSystem.Actions.Results;
using BattleSystem.Items;
using BattleSystem.Moves;

namespace BattleSystem.Characters
{
    /// <summary>
    /// Records the results of actions that have been applied to some target.
    /// </summary>
    public class ActionHistory
    {
        /// <summary>
        /// Gets or sets the results of actions applied to this target by moves.
        /// </summary>
        public List<IActionResult<Move>> MoveActions { get; private set; }

        /// <summary>
        /// Gets the most recent move action.
        /// </summary>
        public DamageResult<Move> LastMoveDamageResult
        {
            get => MoveActions.Where(a => a is DamageResult<Move>)
                              .LastOrDefault() as DamageResult<Move>;
        }

        /// <summary>
        /// Gets or sets the results of actions applied to this target by items.
        /// </summary>
        public List<IActionResult<Item>> ItemActions { get; private set; }

        /// <summary>
        /// Creates a new <see cref="ActionHistory"/> instance.
        /// </summary>
        public ActionHistory()
        {
            MoveActions = new List<IActionResult<Move>>();
            ItemActions = new List<IActionResult<Item>>();
        }

        /// <summary>
        /// Adds the given action to the move action history.
        /// </summary>
        /// <typeparam name="TSource">The type of the object that caused the action.</typeparam>
        /// <param name="result">The result of the action.</param>
        public void AddAction<TSource>(IActionResult<TSource> result)
        {
            switch (result)
            {
                case IActionResult<Move> moveResult:
                    MoveActions.Add(moveResult);
                    break;
                case IActionResult<Item> itemResult:
                    ItemActions.Add(itemResult);
                    break;
            }
        }
    }
}
