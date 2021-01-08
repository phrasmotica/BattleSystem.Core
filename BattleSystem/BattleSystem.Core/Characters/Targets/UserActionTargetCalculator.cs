using System.Collections.Generic;

namespace BattleSystem.Core.Characters.Targets
{
    /// <summary>
    /// Calculates the action target as the user.
    /// </summary>
    public class UserActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public bool IsReactive => false;

        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            return (true, new[] { user });
        }
    }
}
