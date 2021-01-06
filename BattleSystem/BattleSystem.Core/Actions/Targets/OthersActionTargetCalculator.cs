using System.Collections.Generic;
using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Actions.Targets
{
    /// <summary>
    /// Calculates the action targets as all of the other characters.
    /// </summary>
    public class OthersActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public bool IsReactive => false;

        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            return (true, otherCharacters);
        }
    }
}
