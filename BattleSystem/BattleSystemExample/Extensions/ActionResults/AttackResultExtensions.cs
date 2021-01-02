using BattleSystem.Actions.Results;
using BattleSystem.Items;

namespace BattleSystemExample.Extensions.ActionResults
{
    /// <summary>
    /// Extension methods for <see cref="AttackResult{TSource}"/>.
    /// </summary>
    public static class AttackResultExtensions
    {
        /// <summary>
        /// Returns a string describing this attack result.
        /// </summary>
        /// <param name="attack">The attack result.</param>
        public static string Describe<TSource>(this AttackResult<TSource> attack)
        {
            var target = attack.Target;
            var amount = attack.Damage;

            if (attack.IsSelfInflicted)
            {
                if (attack.TargetDied)
                {
                    return attack.Source switch
                    {
                        Item item => $"{target.Name} took {amount} damage from their {item.Name} and died!",
                        _ => $"{target.Name} took {amount} damage and died!",
                    };
                }

                return attack.Source switch
                {
                    Item item => $"{target.Name} took {amount} damage from their {item.Name}!",
                    _ => $"{target.Name} took {amount} damage!",
                };
            }

            var user = attack.User;

            if (attack.TargetDied)
            {
                return attack.Source switch
                {
                    Item item => $"{target.Name} took {amount} damage from {user.Name}'s {item.Name} and died!",
                    _ => $"{target.Name} took {amount} damage and died!",
                };
            }

            return attack.Source switch
            {
                Item item => $"{target.Name} took {amount} damage from {user.Name}'s {item.Name}!",
                _ => $"{target.Name} took {amount} damage!",
            };
        }
    }
}
