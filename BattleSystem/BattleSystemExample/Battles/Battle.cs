using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Moves;
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
        /// The user.
        /// </summary>
        private readonly Character _user;

        /// <summary>
        /// The enemy.
        /// </summary>
        private readonly Character _enemy;

        /// <summary>
        /// Creates a new <see cref="Battle"/> instance.
        /// </summary>
        /// <param name="moveProcessor">The move processor.</param>
        /// <param name="gameOutput">The game output.</param>
        /// <param name="user">The user.</param>
        /// <param name="enemy">The enemy.</param>
        public Battle(MoveProcessor moveProcessor, IGameOutput gameOutput, Character user, Character enemy)
        {
            _moveProcessor = moveProcessor;
            _gameOutput = gameOutput;
            _user = user;
            _enemy = enemy;
        }

        /// <summary>
        /// Starts the battle and returns once it's over.
        /// </summary>
        public void Start()
        {
            while (!_user.IsDead && !_enemy.IsDead)
            {
                _gameOutput.WriteLine();
                _gameOutput.WriteLine($"{_enemy.Name}: {_enemy.CurrentHealth}/{_enemy.MaxHealth} HP");
                _gameOutput.WriteLine($"{_user.Name}: {_user.CurrentHealth}/{_user.MaxHealth} HP");

                var characterOrder = new[] { _user, _enemy }.OrderByDescending(c => c.CurrentSpeed);

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

                    ShowDamageTaken(characterOrder, moveUse);

                    ShowStatChanges(characterOrder, moveUse);
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
                    _gameOutput.WriteLine($"{character.Name} took {amount} damage!");
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

        /// <summary>
        /// Outputs the end message.
        /// </summary>
        private void ShowEndMessage()
        {
            if (_enemy.IsDead)
            {
                _gameOutput.WriteLine($"{_enemy.Name} is dead! {_user.Name} wins!");
            }
            else if (_user.IsDead)
            {
                _gameOutput.WriteLine($"{_user.Name} is dead! {_enemy.Name} wins!");
            }
        }
    }
}
