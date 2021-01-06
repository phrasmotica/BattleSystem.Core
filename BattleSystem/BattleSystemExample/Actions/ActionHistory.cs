using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Actions;
using BattleSystem.Core.Actions.Damage;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Items;
using BattleSystem.Core.Moves;

namespace BattleSystemExample.Actions
{
    /// <summary>
    /// Records the moves and actions that have been executed in a battle.
    /// </summary>
    public class ActionHistory
    {
        /// <summary>
        /// Gets or sets the turn counter.
        /// </summary>
        public int TurnCounter { get; private set; }

        /// <summary>
        /// Gets or sets the moves used.
        /// </summary>
        public List<(int turnNumber, MoveUse moveUse)> MoveUses { get; private set; }

        /// <summary>
        /// Gets or sets the actions used on the given turn from moves.
        /// </summary>
        public List<(int turnNumber, IEnumerable<IActionResult<Move>> results)> MoveActions
        {
            get
            {
                return MoveUses.Select(e =>
                {
                    var turnNumber = e.turnNumber;
                    var results = e.moveUse.ActionsResults
                                           .SelectMany(aur => aur.Results);

                    return (turnNumber, results);
                }).ToList();
            }
        }

        /// <summary>
        /// Gets or sets the results of actions executed by items.
        /// </summary>
        public List<(int turnNumber, IActionResult<Item> result)> ItemActions { get; private set; }

        /// <summary>
        /// Creates a new <see cref="ActionHistory"/> instance.
        /// </summary>
        public ActionHistory()
        {
            MoveUses = new List<(int, MoveUse)>();
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
        /// Adds the given move use to the move use history.
        /// </summary>
        /// <param name="moveUse">The move use.</param>
        public void AddMoveUse(MoveUse moveUse)
        {
            MoveUses.Add((TurnCounter, moveUse));
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
        public DamageActionResult<Move> LastMoveDamageResultAgainst(Character character)
        {
            return MoveActions.Single(a => a.turnNumber == TurnCounter).results
                              .Where(a => a.Target == character)
                              .Where(a => a.Applied)
                              .Where(a => a is DamageActionResult<Move>)
                              .LastOrDefault() as DamageActionResult<Move>;
        }

        /// <summary>
        /// Gets the number of times the given action has been used successfully
        /// by the given user since it last failed.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="user">The user.</param>
        public int GetMoveDamageConsecutiveSuccessCount(IAction action, Character user)
        {
            return MoveUses.Select(a => a.moveUse)
                           .SelectMany(mu => mu.ActionsResults)
                           .SelectMany(aur => aur.Results)
                           .Where(ar => ar.Action == action)
                           .Where(ar => ar.User == user)
                           .Reverse()
                           .TakeWhile(ar => ar.Applied)
                           .Count();
        }

        /// <summary>
        /// Gets the number of times the given move has been used successfully
        /// by the given user since it last failed.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <param name="user">The user.</param>
        public int GetMoveConsecutiveSuccessCount(Move move, Character user)
        {
            return MoveUses.Select(a => a.moveUse)
                           .Where(mu => mu.Move == move)
                           .Where(mu => mu.User == user)
                           .Reverse()
                           .TakeWhile(mu => mu.Success)
                           .Count();
        }
    }
}
