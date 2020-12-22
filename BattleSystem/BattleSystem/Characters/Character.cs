using System;
using System.Collections.Generic;
using BattleSystem.Moves;
using BattleSystem.Stats;

namespace BattleSystem.Characters
{
    /// <summary>
    /// Abstract class representing a character.
    /// </summary>
    public abstract class Character
    {
        /// <summary>
        /// Gets or sets the character's ID.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets or sets the character's name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the character's team.
        /// </summary>
        public string Team { get; protected set; }

        /// <summary>
        /// Gets or sets the character's max health.
        /// </summary>
        public int MaxHealth { get; protected set; }

        /// <summary>
        /// Gets or sets the character's current health.
        /// </summary>
        public int CurrentHealth { get; protected set; }

        /// <summary>
        /// Gets or sets the character's stats.
        /// </summary>
        public StatSet Stats { get; protected set; }

        /// <summary>
        /// Gets or sets the character's moves.
        /// </summary>
        public MoveSet Moves { get; protected set; }

        /// <summary>
        /// Gets or sets the character's protect counter.
        /// </summary>
        public int ProtectCounter { get; set; }

        /// <summary>
        /// Gets the character's current speed.
        /// </summary>
        public int CurrentSpeed => Stats.Speed.CurrentValue;

        /// <summary>
        /// Gets whether the character is dead.
        /// </summary>
        public bool IsDead => CurrentHealth <= 0;

        /// <summary>
        /// Creates a new character with the given name, max health, stats and moves.
        /// </summary>
        public Character(
            string name,
            string team,
            int maxHealth,
            StatSet stats,
            MoveSet moves)
        {
            Name = name;
            Team = team;

            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;

            Stats = stats;
            Moves = moves;

            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Returns a move to use in battle.
        /// </summary>
        /// <param name="otherCharacters">The other characters in the battle.</param>
        public abstract MoveUse ChooseMove(IEnumerable<Character> otherCharacters);

        /// <summary>
        /// Takes the incoming damage, pending the protection counter.
        /// </summary>
        /// <param name="damage">The incoming damage.</param>
        public virtual void ReceiveDamage(int damage)
        {
            if (ProtectCounter > 0)
            {
                ProtectCounter--;
            }
            else
            {
                CurrentHealth -= damage;
            }
        }

        /// <summary>
        /// Receives effects from the given buff.
        /// </summary>
        /// <param name="multipliers">The effects of incoming buff.</param>
        public virtual void ReceiveBuff(IDictionary<StatCategory, double> multipliers)
        {
            foreach (var mult in multipliers)
            {
                var statCategory = mult.Key;

                switch (statCategory)
                {
                    case StatCategory.Attack:
                        Stats.Attack.Multiplier += mult.Value;
                        break;

                    case StatCategory.Defence:
                        Stats.Defence.Multiplier += mult.Value;
                        break;

                    case StatCategory.Speed:
                        Stats.Speed.Multiplier += mult.Value;
                        break;

                    default:
                        throw new ArgumentException($"Unrecognised stat category {statCategory}!");
                }
            }
        }

        /// <summary>
        /// Restores the given amount of health, capped by the character's max health.
        /// </summary>
        /// <param name="amount">The healing amount.</param>
        public virtual void Heal(int amount)
        {
            CurrentHealth += Math.Min(MaxHealth - CurrentHealth, amount);
        }
    }
}
