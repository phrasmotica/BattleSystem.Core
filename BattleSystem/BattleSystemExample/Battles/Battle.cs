using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Moves;
using BattleSystem.Actions.Results;
using BattleSystem.Moves.Success;
using BattleSystemExample.Output;
using BattleSystemExample.Extensions;

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
            var teams = Teams.ToArray();

            while (teams.All(t => t.Any(c => !c.IsDead)))
            {
                foreach (var team in teams)
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
                    // move might be cancelled due to the user dying or all targets dying
                    var moveCancelled = !moveUse.ActionsResults.Any();
                    if (!moveCancelled)
                    {
                        ShowMoveUse(characterOrder, moveUse);
                    }
                }

                foreach (var character in characterOrder)
                {
                    character.ClearProtectQueue();
                }
            }

            ShowEndMessage();
        }

        /// <summary>
        /// Outputs a summary of the given move use.
        /// </summary>
        /// <param name="characters">The characters.</param>
        /// <param name="moveUse">The move use.</param>
        private void ShowMoveUse(IEnumerable<Character> characters, MoveUse moveUse)
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
                    ShowResult(characters, result);
                }
            }
        }

        /// <summary>
        /// Outputs the given action result.
        /// </summary>
        /// <param name="characters">The characters.</param>
        /// <param name="result">The result.</param>
        private void ShowResult(IEnumerable<Character> characters, IActionResult result)
        {
            if (result.TargetProtected)
            {
                ShowProtectedResult(characters, result);
            }
            else switch (result)
            {
                case AttackResult ar:
                    ShowAttack(characters, ar);
                    break;
                case BuffResult br:
                    ShowBuff(characters, br);
                    break;
                case HealResult hr:
                    ShowHeal(characters, hr);
                    break;
                case ProtectLimitChangeResult plcr:
                    ShowProtectLimitChangeResult(characters, plcr);
                    break;
                case ProtectResult pr:
                    ShowProtectResult(characters, pr);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Outputs info about the given protected action result.
        /// </summary>
        /// <param name="characters">The characters.</param>
        /// <param name="result">The action result.</param>
        private void ShowProtectedResult(
            IEnumerable<Character> characters,
            IActionResult result)
        {
            var user = characters.Single(c => c.Id == result.ProtectUserId);

            if (result.TargetId == user.Id)
            {
                _gameOutput.WriteLine($"{user.Name} protected itself!");
            }
            else
            {
                var target = characters.Single(c => c.Id == result.TargetId);
                _gameOutput.WriteLine($"{user.Name} protected {target.Name}!");
            }
        }

        /// <summary>
        /// Outputs info about the given attack result.
        /// </summary>
        /// <param name="characters">The characters.</param>
        /// <param name="attack">The attack result.</param>
        private void ShowAttack(
            IEnumerable<Character> characters,
            AttackResult attack)
        {
            var target = characters.Single(c => c.Id == attack.TargetId);
            var amount = attack.Damage;

            if (attack.TargetDied)
            {
                _gameOutput.WriteLine($"{target.Name} took {amount} damage and died!");
            }
            else
            {
                _gameOutput.WriteLine($"{target.Name} took {amount} damage!");
            }
        }

        /// <summary>
        /// Outputs the given stat changes that occurred to the given characters from the given move use.
        /// </summary>
        /// <param name="characters">The characters.</param>
        /// <param name="buff">The buff result.</param>
        private void ShowBuff(
            IEnumerable<Character> characters,
            BuffResult buff)
        {
            var target = characters.Single(c => c.Id == buff.TargetId);
            if (!target.IsDead)
            {
                var statMultiplierChanges = buff.StatMultiplierChanges;

                foreach (var change in statMultiplierChanges)
                {
                    var stat = change.Key;
                    var percentage = (int)(change.Value * 100);

                    if (percentage > 0)
                    {
                        // < 0 means the multiplier was lower before the move was used
                        _gameOutput.WriteLine($"{target.Name}'s {stat} rose by {percentage}%!");
                    }
                    else if (percentage < 0)
                    {
                        _gameOutput.WriteLine($"{target.Name}'s {stat} fell by {-percentage}%!");
                    }
                }
            }
        }

        /// <summary>
        /// Outputs info about the given heal result.
        /// </summary>
        /// <param name="characters">The characters.</param>
        /// <param name="heal">The heal result.</param>
        private void ShowHeal(
            IEnumerable<Character> characters,
            HealResult heal)
        {
            var target = characters.Single(c => c.Id == heal.TargetId);

            _gameOutput.WriteLine($"{target.Name} recovered {heal.Amount} health!");
        }

        /// <summary>
        /// Outputs info about a protect limit change result.
        /// </summary>
        /// <param name="characters">The characters.</param>
        /// <param name="result">The protect limit change result.</param>
        private void ShowProtectLimitChangeResult(
            IEnumerable<Character> characters,
            ProtectLimitChangeResult result)
        {
            var target = characters.Single(c => c.Id == result.TargetId);

            if (!target.IsDead)
            {
                if (result.Amount > 0)
                {
                    _gameOutput.WriteLine($"{target.Name} had its protection limit increased by {result.Amount}!");
                }
                else
                {
                    _gameOutput.WriteLine($"{target.Name} had its protection limit decreased by {-result.Amount}!");
                }
            }
        }

        /// <summary>
        /// Outputs info about a protect limit change result.
        /// </summary>
        /// <param name="characters">The characters.</param>
        /// <param name="protect">The protect limit change result.</param>
        private void ShowProtectResult(
            IEnumerable<Character> characters,
            ProtectResult protect)
        {
            var target = characters.Single(c => c.Id == protect.TargetId);
            if (!target.IsDead)
            {
                _gameOutput.WriteLine($"{target.Name} became protected!");
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
