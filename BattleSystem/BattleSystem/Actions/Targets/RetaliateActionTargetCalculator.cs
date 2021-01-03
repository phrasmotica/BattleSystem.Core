using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Targets
{
    /// <summary>
    /// Calculates the action target as the user of the move containing the
    /// action that last affected the user of this action.
    /// </summary>
    public class RetaliateActionTargetCalculator : IActionTargetCalculator
    {
        /// <inheritdoc />
        public bool IsReactive => true;

        /// <inheritdoc />
        public (bool success, IEnumerable<Character> targets) Calculate(Character user, IEnumerable<Character> otherCharacters)
        {
            var result = user.ActionHistory.LastMoveDamageResult;
            if (result is null)
            {
                return (false, Enumerable.Empty<Character>());
            }

            if (result.User == user)
            {
                // cannot retaliate against oneself
                return (false, Enumerable.Empty<Character>());
            }

            return (true, new[] { result.User });
        }
    }
}
