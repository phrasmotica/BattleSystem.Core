using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Targets
{
    /// <summary>
    /// Calculates the action targets as all of the characters, including the user.
    /// </summary>
    public class AllActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public IEnumerable<Character> Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            return otherCharacters.Prepend(user);
        }
    }
}
