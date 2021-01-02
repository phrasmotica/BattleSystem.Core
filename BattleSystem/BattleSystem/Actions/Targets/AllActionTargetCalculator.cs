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
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            return (true, otherCharacters.Prepend(user));
        }
    }
}
