using BattleSystem.Actions.Results;
using BattleSystem.Items;

namespace BattleSystemExample.Extensions.ActionResults
{
    /// <summary>
    /// Extension methods for <see cref="DamageResult{TSource}"/>.
    /// </summary>
    public static class DamageResultExtensions
    {
        /// <summary>
        /// Returns a string describing this damage result.
        /// </summary>
        /// <param name="damage">The damage result.</param>
        public static string Describe<TSource>(this DamageResult<TSource> damage)
        {
            var target = damage.Target;
            var amount = damage.Amount;

            if (damage.IsSelfInflicted)
            {
                if (damage.TargetDied)
                {
                    return damage.Source switch
                    {
                        Item item => $"{target.Name} took {amount} damage from their {item.Name} and died!",
                        _ => $"{target.Name} took {amount} damage and died!",
                    };
                }

                return damage.Source switch
                {
                    Item item => $"{target.Name} took {amount} damage from their {item.Name}!",
                    _ => $"{target.Name} took {amount} damage!",
                };
            }

            var user = damage.User;

            if (damage.TargetDied)
            {
                if (damage.IsRetaliation)
                {
                    return damage.Source switch
                    {
                        Item item => $"{target.Name} took {amount} damage from {user.Name}'s {item.Name} and died!",
                        _ => $"{user.Name} retaliated to deal {amount} damage and kill {target.Name}!",
                    };
                }

                return damage.Source switch
                {
                    Item item => $"{target.Name} took {amount} damage from {user.Name}'s {item.Name} and died!",
                    _ => $"{target.Name} took {amount} damage and died!",
                };
            }

            if (damage.IsRetaliation)
            {
                return damage.Source switch
                {
                    Item item => $"{target.Name} took {amount} damage from {user.Name}'s {item.Name}!",
                    _ => $"{user.Name} retaliated to deal {amount} damage to {target.Name}!",
                };
            }

            return damage.Source switch
            {
                Item item => $"{target.Name} took {amount} damage from {user.Name}'s {item.Name}!",
                _ => $"{target.Name} took {amount} damage!",
            };
        }
    }
}
