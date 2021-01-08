using System.Collections.Generic;
using System.Linq;

namespace BattleSystem.Core.Characters.Targets
{
    /// <summary>
    /// Calculates the action targets as all of the characters, including the user.
    /// </summary>
    public class AllActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public bool IsReactive => false;

        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            return (true, otherCharacters.Prepend(user));
        }
    }
}
