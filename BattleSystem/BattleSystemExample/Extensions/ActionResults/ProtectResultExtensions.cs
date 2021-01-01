using BattleSystem.Actions.Results;
using BattleSystem.Items;

namespace BattleSystemExample.Extensions.ActionResults
{
    /// <summary>
    /// Extension methods for <see cref="ProtectResult{TSource}"/>.
    /// </summary>
    public static class ProtectResultExtensions
    {
        /// <summary>
        /// Returns a string describing this protect result.
        /// </summary>
        /// <param name="protect">The protect result.</param>
        public static string Describe<TSource>(this ProtectResult<TSource> protect)
        {
            var target = protect.Target;
            if (target.IsDead)
            {
                return null;
            }

            if (protect.IsSelfInflicted)
            {
                return protect.Source switch
                {
                    Item item => $"{target.Name}'s {item.Name} protected them!",
                    _ => $"{target.Name} became protected!",
                };
            }

            var user = protect.User;

            return protect.Source switch
            {
                Item item => $"{user.Name}'s {item.Name} protected {target.Name}!",
                _ => $"{user.Name} protected {target.Name}!",
            };
        }
    }
}
