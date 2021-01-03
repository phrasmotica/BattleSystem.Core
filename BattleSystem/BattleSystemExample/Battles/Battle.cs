using System.Collections.Generic;
using System.Linq;
using BattleSystem.Actions;
using BattleSystem.Actions.Buff;
using BattleSystem.Actions.Damage;
using BattleSystem.Actions.Heal;
using BattleSystem.Actions.Protect;
using BattleSystem.Actions.ProtectLimitChange;
using BattleSystem.Characters;
using BattleSystem.Moves;
using BattleSystem.Moves.Success;
using BattleSystemExample.Actions;
using BattleSystemExample.Extensions;
using BattleSystemExample.Extensions.ActionResults;
using BattleSystemExample.Output;

namespace BattleSystemExample.Battles
{
    /// <summary>
    /// Class for processing a battle.
    /// </summary>
    public class Battle
    {
        /// <summary>
        /// The move processor.
        /// </summary>
        private readonly MoveProcessor _moveProcessor;

        /// <summary>
        /// The game output.
        /// </summary>
        private readonly IGameOutput _gameOutput;

        /// <summary>
        /// The characters in the battle.
        /// </summary>
        private readonly IEnumerable<Character> _characters;

        /// <summary>
        /// Gets the teams involved in the battle.
        /// </summary>
        private IEnumerable<IGrouping<string, Character>> Teams => _characters.GroupBy(c => c.Team);

        /// <summary>
        /// The action history for the battle.
        /// </summary>
        private readonly ActionHistory _actionHistory;

        /// <summary>
        /// Gets whether the battle is over, i.e. whether there is some team
        /// whose characters are all dead.
        /// </summary>
        private bool IsOver => Teams.Any(t => t.All(c => c.IsDead));

        /// <summary>
        /// Creates a new <see cref="Battle"/> instance.
        /// </summary>
        /// <param name="moveProcessor">The move processor.</param>
        /// <param name="actionHistory">The action history.</param>
        /// <param name="gameOutput">The game output.</param>
        /// <param name="characters">The characters in the battle.</param>
        public Battle(
            MoveProcessor moveProcessor,
            ActionHistory actionHistory,
            IGameOutput gameOutput,
            IEnumerable<Character> characters)
        {
            _moveProcessor = moveProcessor;
            _actionHistory = actionHistory;
            _gameOutput = gameOutput;
            _characters = characters;
        }

        /// <summary>
        /// Starts the battle and returns once it's over.
        /// </summary>
        public void Start()
        {
            while (!IsOver)
            {
                _actionHistory.StartTurn();
                _gameOutput.WriteLine();
                _gameOutput.WriteLine($"TURN {_actionHistory.TurnCounter}");

                foreach (var team in Teams)
                {
                    _gameOutput.WriteLine();

                    foreach (var c in team.Where(c => !c.IsDead))
                    {
                        _gameOutput.WriteLine(c.Summarise());
                    }
                }

                var characterOrder = _characters.Where(c => !c.IsDead)
                                                .OrderByDescending(c => c.CurrentSpeed)
                                                .ToArray();

                foreach (var character in characterOrder)
                {
                    var otherCharacters = characterOrder.Where(c => c.Id != character.Id);
                    var startTurnResult = character.OnStartTurn(otherCharacters);
                    ShowBattlePhaseResult(startTurnResult);
                }

                if (IsOver)
                {
                    break;
                }

                foreach (var character in characterOrder)
                {
                    var otherCharacters = characterOrder.Where(c => c.Id != character.Id);
                    var moveUse = character.ChooseMove(otherCharacters);
                    moveUse.SetTargets();
                    _moveProcessor.Push(moveUse);
                }

                while (!_moveProcessor.MoveUseQueueIsEmpty)
                {
                    var moveUse = _moveProcessor.ApplyNext();
                    if (moveUse.HasResult)
                    {
                        AddToActionHistory(moveUse);
                        ShowMoveUse(moveUse);
                    }
                }

                if (IsOver)
                {
                    break;
                }

                foreach (var character in characterOrder)
                {
                    var otherCharacters = characterOrder.Where(c => c.Id != character.Id);
                    var endTurnResult = character.OnEndTurn(otherCharacters);
                    ShowBattlePhaseResult(endTurnResult);
                }
            }

            ShowEndMessage();
        }

        /// <summary>
        /// Adds the given move use to the action history.
        /// </summary>
        /// <param name="moveUse"></param>
        private void AddToActionHistory(MoveUse moveUse)
        {
            var results = moveUse.ActionsResults.SelectMany(ars => ars.Results);
            foreach (var result in results)
            {
                _actionHistory.AddAction(result);
            }
        }

        /// <summary>
        /// Outputs a summary of the given move use.
        /// </summary>
        /// <param name="moveUse">The move use.</param>
        private void ShowMoveUse(MoveUse moveUse)
        {
            if (moveUse.HasResult && !moveUse.TargetsAllDead)
            {
                switch (moveUse.Result)
                {
                    case MoveUseResult.Success:
                        _gameOutput.WriteLine($"{moveUse.User.Name} used {moveUse.Move.Name}!");
                        break;

                    case MoveUseResult.Miss:
                        _gameOutput.WriteLine($"{moveUse.User.Name} used {moveUse.Move.Name} but missed!");
                        break;
                }

                foreach (var actionResult in moveUse.ActionsResults)
                {
                    if (actionResult.Success)
                    {
                        foreach (var result in actionResult.Results)
                        {
                            ShowResult(result);
                        }
                    }
                    else
                    {
                        _gameOutput.WriteLine("But it failed!");
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the given action result.
        /// </summary>
        /// <param name="result">The result.</param>
        private void ShowResult<TSource>(IActionResult<TSource> result)
        {
            if (result.TargetProtected)
            {
                _gameOutput.WriteLine(result.DescribeProtected());
            }
            else switch (result)
            {
                case DamageActionResult<TSource> dr:
                    var damageDescription = dr.Describe();
                    if (damageDescription is not null)
                    {
                        _gameOutput.WriteLine(damageDescription);
                    }
                    break;
                case BuffActionResult<TSource> br:
                    var buffDescription = br.Describe();
                    if (buffDescription is not null)
                    {
                        _gameOutput.WriteLine(buffDescription);
                    }
                    break;
                case HealActionResult<TSource> hr:
                    var healDescription = hr.Describe();
                    if (healDescription is not null)
                    {
                        _gameOutput.WriteLine(healDescription);
                    }
                    break;
                case ProtectLimitChangeActionResult<TSource> plcr:
                    var plcrDescription = plcr.Describe();
                    if (plcrDescription is not null)
                    {
                        _gameOutput.WriteLine(plcrDescription);
                    }
                    break;
                case ProtectActionResult<TSource> pr:
                    var protectDescription = pr.Describe();
                    if (protectDescription is not null)
                    {
                        _gameOutput.WriteLine(protectDescription);
                    }
                    break;
            }
        }

        /// <summary>
        /// Outputs the given battle phase result.
        /// </summary>
        /// <param name="battlePhaseResult">The battle phase result.</param>
        private void ShowBattlePhaseResult(BattlePhaseResult battlePhaseResult)
        {
            foreach (var actionUseResult in battlePhaseResult.ItemActionsResults)
            {
                foreach (var result in actionUseResult.Results)
                {
                    ShowResult(result);
                }
            }
        }

        /// <summary>
        /// Outputs the end message.
        /// </summary>
        private void ShowEndMessage()
        {
            var winningTeam = Teams.Single(t => t.Any(c => !c.IsDead));
            _gameOutput.WriteLine($"Team {winningTeam.Key} wins!");
        }
    }
}
