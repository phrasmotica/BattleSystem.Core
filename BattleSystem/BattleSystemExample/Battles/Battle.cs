using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Moves;
using BattleSystem.Actions.Results;
using BattleSystem.Moves.Success;
using BattleSystemExample.Output;
using BattleSystemExample.Extensions;
using BattleSystemExample.Extensions.ActionResults;

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
        /// Gets whether the battle is over, i.e. whether there is some team
        /// whose characters are all dead.
        /// </summary>
        private bool IsOver => Teams.Any(t => t.All(c => c.IsDead));

        /// <summary>
        /// Creates a new <see cref="Battle"/> instance.
        /// </summary>
        /// <param name="moveProcessor">The move processor.</param>
        /// <param name="gameOutput">The game output.</param>
        /// <param name="characters">The characters in the battle.</param>
        public Battle(
            MoveProcessor moveProcessor,
            IGameOutput gameOutput,
            IEnumerable<Character> characters)
        {
            _moveProcessor = moveProcessor;
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
                    var moveUse = character.ChooseMove(otherCharacters);
                    _moveProcessor.Push(moveUse);
                }

                var moveUses = _moveProcessor.Apply();

                foreach (var moveUse in moveUses)
                {
                    ShowMoveUse(moveUse);
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
        /// Outputs a summary of the given move use.
        /// </summary>
        /// <param name="moveUse">The move use.</param>
        private void ShowMoveUse(MoveUse moveUse)
        {
            // don't show use if move was successful but all targets were dead
            var moveCancelled = moveUse.Result == MoveUseResult.Success
                             && moveUse.ActionsResults.All(ars => !ars.Any());

            if (!moveCancelled)
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

                foreach (var actionResults in moveUse.ActionsResults)
                {
                    foreach (var result in actionResults)
                    {
                        ShowResult(result);
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
                case AttackResult<TSource> ar:
                    var attackDescription = ar.Describe();
                    if (attackDescription is not null)
                    {
                        _gameOutput.WriteLine(attackDescription);
                    }
                    break;
                case BuffResult<TSource> br:
                    var buffDescription = br.Describe();
                    if (buffDescription is not null)
                    {
                        _gameOutput.WriteLine(buffDescription);
                    }
                    break;
                case HealResult<TSource> hr:
                    var healDescription = hr.Describe();
                    if (healDescription is not null)
                    {
                        _gameOutput.WriteLine(healDescription);
                    }
                    break;
                case ProtectLimitChangeResult<TSource> plcr:
                    var plcrDescription = plcr.Describe();
                    if (plcrDescription is not null)
                    {
                        _gameOutput.WriteLine(plcrDescription);
                    }
                    break;
                case ProtectResult<TSource> pr:
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
            foreach (var actionResults in battlePhaseResult.ItemActionsResults)
            {
                foreach (var result in actionResults)
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
