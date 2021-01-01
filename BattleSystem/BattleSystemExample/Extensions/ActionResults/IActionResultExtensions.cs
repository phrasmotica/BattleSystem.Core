using BattleSystem.Actions.Results;

namespace BattleSystemExample.Extensions.ActionResults
{
    /// <summary>
    /// Extension methods for <see cref="IActionResult{TSource}"./>
    /// </summary>
    public static class IActionResultExtensions
    {
        /// <summary>
        /// Returns a string describing this action result being protected.
        /// </summary>
        /// <param name="result">The action result.</param>
        public static string DescribeProtected<TSource>(this IActionResult<TSource> result)
        {
            var user = result.User;
            var target = result.Target;

            if (result.Target.Id == user.Id)
            {
                return $"{user.Name} protected itself!";
            }

            return $"{user.Name} protected {target.Name}!";
        }
    }
}
