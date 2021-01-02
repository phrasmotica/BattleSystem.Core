using System.Collections.Generic;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Targets
{
    /// <summary>
    /// Calculates the action targets as all of the other characters.
    /// </summary>
    public class OthersActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public IEnumerable<Character> Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            return otherCharacters;
        }
    }
}
