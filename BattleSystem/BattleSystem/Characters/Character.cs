﻿using System;
using System.Collections.Generic;
using BattleSystem.Extensions;
using BattleSystem.Moves;
using BattleSystem.Moves.Actions.Results;
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
        /// Gets or sets the list of characters who are protecting this character.
        /// </summary>
        protected List<string> ProtectQueue;

        /// <summary>
        /// Gets or sets the maximum allowed length of the protect queue.
        /// </summary>
        public int ProtectLimit { get; protected set; }

        /// <summary>
        /// Gets the length of the protect queue.
        /// </summary>
        public int ProtectCount => ProtectQueue?.Count ?? 0;

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

            ProtectQueue = new List<string>();
            ProtectLimit = 1;
        }

        /// <summary>
        /// Returns a move to use in battle.
        /// </summary>
        /// <param name="otherCharacters">The other characters in the battle.</param>
        public abstract MoveUse ChooseMove(IEnumerable<Character> otherCharacters);

        /// <summary>
        /// Takes the incoming damage and returns the result.
        /// </summary>
        /// <param name="damage">The incoming damage.</param>
        public virtual AttackResult ReceiveDamage(int damage)
        {
            if (ProtectQueue.Count > 0)
            {
                var userId = ConsumeProtect();

                return new AttackResult
                {
                    Applied = false,
                    TargetId = Id,
                    TargetProtected = true,
                    ProtectUserId = userId,
                };
            }

            var startingHealth = CurrentHealth;
            CurrentHealth -= damage;
            var endingHealth = CurrentHealth;

            return new AttackResult
            {
                Applied = true,
                TargetId = Id,
                StartingHealth = startingHealth,
                EndingHealth = endingHealth,
                TargetProtected = false,
            };
        }

        /// <summary>
        /// Receives effects from the given buff and returns the result.
        /// </summary>
        /// <param name="multipliers">The effects of incoming buff.</param>
        public virtual BuffResult ReceiveBuff(IDictionary<StatCategory, double> multipliers)
        {
            var startingMultipliers = Stats.MultipliersAsDictionary();

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

            var endingMultipliers = Stats.MultipliersAsDictionary();

            return new BuffResult
            {
                Applied = true,
                TargetId = Id,
                StartingStatMultipliers = startingMultipliers,
                EndingStatMultipliers = endingMultipliers,
            };
        }

        /// <summary>
        /// Restores the given amount of health, capped by the character's max health, and returns the result.
        /// </summary>
        /// <param name="amount">The healing amount.</param>
        public virtual HealResult Heal(int amount)
        {
            var startingHealth = CurrentHealth;
            CurrentHealth += Math.Min(MaxHealth - CurrentHealth, amount);
            var endingHealth = CurrentHealth;

            return new HealResult
            {
                Applied = true,
                TargetId = Id,
                StartingHealth = startingHealth,
                EndingHealth = endingHealth,
            };
        }

        /// <summary>
        /// Adds an item to the protect queue, which protects the character from the next attack.
        /// </summary>
        public virtual ProtectResult AddProtect(string userId)
        {
            if (ProtectCount >= ProtectLimit)
            {
                return new ProtectResult
                {
                    Applied = false,
                    TargetId = Id,
                };
            }

            ProtectQueue.Add(userId);

            return new ProtectResult
            {
                Applied = true,
                TargetId = Id,
            };
        }

        /// <summary>
        /// Changes the protect count limit by the given amount.
        /// </summary>
        public ProtectLimitChangeResult ChangeProtectCountLimit(int amount)
        {
            ProtectLimit += amount;

            return new ProtectLimitChangeResult
            {
                Applied = true,
                TargetId = Id,
                Amount = amount,
            };
        }

        /// <summary>
        /// Pops the next protect action from the queue and returns the ID of the protecting character.
        /// </summary>
        public string ConsumeProtect()
        {
            if (ProtectQueue.Count <= 0)
            {
                throw new InvalidOperationException($"Cannot consume a protect action because there are none in the queue!");
            }

            var protectorId = ProtectQueue[0];
            ProtectQueue.RemoveAt(0);
            return protectorId;
        }

        /// <summary>
        /// Clears the protect action queue.
        /// </summary>
        public void ClearProtectQueue()
        {
            ProtectQueue.Clear();
        }
    }
}
