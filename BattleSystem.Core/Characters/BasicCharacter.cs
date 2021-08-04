using System;
using System.Collections.Generic;
using BattleSystem.Core.Abilities;
using BattleSystem.Core.Moves;
using BattleSystem.Core.Random;
using BattleSystem.Core.Stats;

namespace BattleSystem.Core.Characters
{
    /// <summary>
    /// Class representing a character who fights randomly.
    /// </summary>
    public class BasicCharacter : Character
    {
        /// <summary>
        /// The random number generator.
        /// </summary>
        private readonly IRandom _random;

        /// <summary>
        /// Creates a new basic character with the given name, max health, stats and moves.
        /// </summary>
        public BasicCharacter(
            string name,
            string team,
            int maxHealth,
            StatSet stats,
            MoveSet moves,
            Ability ability,
            IRandom random) : base(name, team, maxHealth, stats, moves, ability)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        /// <inheritdoc />
        public override MoveUse ChooseMove(IEnumerable<Character> otherCharacters)
        {
            return new MoveUse
            {
                Move = Moves.ChooseRandom(_random),
                User = this,
                OtherCharacters = otherCharacters,
            };
        }
    }
}
