﻿using System.Collections.Generic;
using BattleSystem.Moves;
using BattleSystem.Stats;

namespace BattleSystem.Characters
{
    /// <summary>
    /// Class representing a character who fights randomly.
    /// </summary>
    public class BasicCharacter : Character
    {
        /// <summary>
        /// Creates a new basic character with the given name, max health, stats and moves.
        /// </summary>
        public BasicCharacter(string name, int maxHealth, StatSet stats, MoveSet moves) : base(name, maxHealth, stats, moves) { }

        /// <inheritdoc />
        public override MoveUse ChooseMove(IEnumerable<Character> otherCharacters)
        {
            return new MoveUse
            {
                Move = Moves.ChooseRandom(),
                User = this,
                OtherCharacters = otherCharacters,
            };
        }
    }
}
