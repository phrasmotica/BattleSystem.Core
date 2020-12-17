using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Extensions;
using BattleSystem.Stats;

namespace BattleSystem.Moves
{
    public class MoveUse
    {
        /// <summary>
        /// Gets or sets the move.
        /// </summary>
        public IMove Move { get; set; }

        /// <summary>
        /// Gets or sets the user of the move.
        /// </summary>
        public Character User { get; set; }

        /// <summary>
        /// Gets or sets the target of the move.
        /// </summary>
        public Character Target { get; set; }

        /// <summary>
        /// Gets or sets the damage taken by the characters with the given IDs from this move use.
        /// </summary>
        public IDictionary<string, int> DamageTaken { get; private set; }

        /// <summary>
        /// Gets or sets the changes in stat multipliers to the characters with the given IDs from this move use.
        /// </summary>
        public IDictionary<string, IDictionary<StatCategory, double>> StatMultiplierChanges { get; private set; }

        /// <summary>
        /// Applies the move.
        /// </summary>
        public void Apply()
        {
            var userStartingHealth = User.CurrentHealth;
            var targetStartingHealth = Target.CurrentHealth;

            var userStartingStatMultipliers = GetStatMultipliers(User);
            var targetStartingStatMultipliers = GetStatMultipliers(Target);

            Move.Use(User, Target);

            var userEndingHealth = User.CurrentHealth;
            var targetEndingHealth = Target.CurrentHealth;

            DamageTaken = new Dictionary<string, int>
            {
                [User.Id] = userStartingHealth - userEndingHealth,
                [Target.Id] = targetStartingHealth - targetEndingHealth,
            };

            var userEndingStatMultipliers = GetStatMultipliers(User);
            var targetEndingStatMultipliers = GetStatMultipliers(Target);

            StatMultiplierChanges = new Dictionary<string, IDictionary<StatCategory, double>>
            {
                [User.Id] = userEndingStatMultipliers.Subtract(userStartingStatMultipliers),
                [Target.Id] = targetEndingStatMultipliers.Subtract(targetStartingStatMultipliers),
            };
        }

        /// <summary>
        /// Returns the stat multipliers for the given character as a dictionary.
        /// </summary>
        /// <param name="character">The character.</param>
        private static IDictionary<StatCategory, double> GetStatMultipliers(Character character)
        {
            return new Dictionary<StatCategory, double>
            {
                [StatCategory.Attack] = character.Stats.Attack.Multiplier,
                [StatCategory.Defence] = character.Stats.Defence.Multiplier,
                [StatCategory.Speed] = character.Stats.Speed.Multiplier,
            };
        }
    }
}
