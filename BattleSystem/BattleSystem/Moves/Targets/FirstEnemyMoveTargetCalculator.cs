﻿using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;

namespace BattleSystem.Moves.Targets
{
    /// <summary>
    /// Calculates the move target as the first of the user's enemies.
    /// </summary>
    public class FirstEnemyMoveTargetCalculator : IMoveTargetCalculator
    {
        /// <inheritdoc />
        public IEnumerable<Character> Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            var enemies = otherCharacters.Where(c => c.Team != user.Team);
            if (!enemies.Any())
            {
                throw new ArgumentException("No enemies to choose as a target!", nameof(otherCharacters));
            }

            return enemies.Take(1);
        }
    }
}
