using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Moves;
using BattleSystem.Moves.Actions.Results;
using BattleSystem.Moves.Success;
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
                _gameOutput.WriteLine();

                foreach (var team in teams)
                {
                    foreach (var c in team.Where(c => !c.IsDead))
                    {
                        _gameOutput.WriteLine($"{c.Name}: {c.CurrentHealth}/{c.MaxHealth} HP");
                    }

                    _gameOutput.WriteLine();
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
                        ShowMoveUse(moveUse);
                        ShowProtectedCharacters(characterOrder, moveUse);
                        ShowDamageTaken(characterOrder, moveUse);
                        ShowStatChanges(characterOrder, moveUse);
                    }
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
            switch (moveUse.Result)
            {
                case MoveUseResult.Success:
                    _gameOutput.WriteLine($"{moveUse.User.Name} used {moveUse.Move.Name}!");
                    break;

                case MoveUseResult.Miss:
                    _gameOutput.WriteLine($"{moveUse.User.Name} used {moveUse.Move.Name} but missed!");
                    break;
            }
        }

        /// <summary>
        /// Outputs info about any character that were protected from the given move use.
        /// </summary>
        /// <param name="characters">The characters.</param>
        /// <param name="moveUse">The move use.</param>
        private void ShowProtectedCharacters(IEnumerable<Character> characters, MoveUse moveUse)
        {
            var protectedAttacks = moveUse.ActionsResults
                                          .SelectMany(ar => ar)
                                          .Where(r => r is AttackResult pr && pr.TargetProtected)
                                          .Cast<AttackResult>();

            foreach (var result in protectedAttacks)
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
        }

        /// <summary>
        /// Outputs the damage taken by the given characters from the given move use.
        /// </summary>
        /// <param name="characters">The characters.</param>
        /// <param name="moveUse">The move use.</param>
        private void ShowDamageTaken(IEnumerable<Character> characters, MoveUse moveUse)
        {
            var damageTaken = moveUse.DamageTaken;

            foreach (var damage in damageTaken)
            {
                var character = characters.Single(c => c.Id == damage.Key);
                var amount = damage.Value;

                if (amount > 0)
                {
                    if (moveUse.CharacterDied(character.Id))
                    {
                        _gameOutput.WriteLine($"{character.Name} took {amount} damage and died!");
                    }
                    else
                    {
                        _gameOutput.WriteLine($"{character.Name} took {amount} damage!");
                    }
                }
                else if (amount < 0)
                {
                    _gameOutput.WriteLine($"{character.Name} recovered {-amount} health!");
                }
            }
        }

        /// <summary>
        /// Outputs the given stat changes that occurred to the given characters from the given move use.
        /// </summary>
        /// <param name="characters">The characters.</param>
        /// <param name="moveUse">The move use.</param>
        private void ShowStatChanges(IEnumerable<Character> characters, MoveUse moveUse)
        {
            var statMultiplierChanges = moveUse.StatMultiplierChanges;

            foreach (var statChanges in statMultiplierChanges)
            {
                var character = characters.Single(c => c.Id == statChanges.Key);
                if (!character.IsDead)
                {
                    var changeDict = statChanges.Value;

                    foreach (var change in changeDict)
                    {
                        var stat = change.Key;
                        var percentage = (int) (change.Value * 100);

                        if (percentage > 0)
                        {
                            // < 0 means the multiplier was lower before the move was used
                            _gameOutput.WriteLine($"{character.Name}'s {stat} rose by {percentage}%!");
                        }
                        else if (percentage < 0)
                        {
                            _gameOutput.WriteLine($"{character.Name}'s {stat} fell by {-percentage}%!");
                        }
                    }
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
