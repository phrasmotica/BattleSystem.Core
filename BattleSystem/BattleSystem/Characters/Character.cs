using System;
using System.Collections.Generic;
using BattleSystem.Items;
using BattleSystem.Items.Results;
using BattleSystem.Moves;
using BattleSystem.Actions.Results;
using BattleSystem.Stats;
using System.Linq;

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
        /// Gets or sets the character's item slot.
        /// </summary>
        public ItemSlot ItemSlot { get; protected set; }

        /// <summary>
        /// Gets whether the character is holding an item.
        /// </summary>
        public bool HasItem => ItemSlot.HasItem;

        /// <summary>
        /// Gets the character's item.
        /// </summary>
        public Item Item => ItemSlot.Current;

        /// <summary>
        /// Gets or sets the list of characters who are protecting this character.
        /// </summary>
        protected List<Character> ProtectQueue;

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
            Id = Guid.NewGuid().ToString();
            Name = name;
            Team = team;

            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;

            Stats = stats;
            Moves = moves;

            ItemSlot = new ItemSlot();

            ProtectQueue = new List<Character>();
            ProtectLimit = 1;
        }

        /// <summary>
        /// Returns a move to use in battle.
        /// </summary>
        /// <param name="otherCharacters">The other characters in the battle.</param>
        public abstract MoveUse ChooseMove(IEnumerable<Character> otherCharacters);

        /// <summary>
        /// Equips the given item and returns the result.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual EquipItemResult EquipItem(Item item)
        {
            var hadPreviousItem = HasItem;

            Item previousItem = null;
            if (hadPreviousItem)
            {
                previousItem = Item;
            }

            ItemSlot.Set(item);

            Stats.ClearTransforms();
            if (item is not null)
            {
                Stats.ReceiveTransforms(item);
            }

            return new EquipItemResult
            {
                Success = true,
                HadPreviousItem = hadPreviousItem,
                PreviousItem = previousItem,
            };
        }

        /// <summary>
        /// Removes the character's item and returns the result.
        /// </summary>
        public virtual RemoveItemResult RemoveItem()
        {
            if (!HasItem)
            {
                return new RemoveItemResult
                {
                    Success = false,
                };
            }

            var item = Item;
            ItemSlot.Remove();

            return new RemoveItemResult
            {
                Success = true,
                Item = item,
            };
        }

        /// <summary>
        /// Takes the incoming damage and returns the result.
        /// </summary>
        /// <param name="damage">The incoming damage.</param>
        /// <param name="user">The character who inflicted the incoming damage.</param>
        /// <typeparam name="TSource">The type of the source of the incoming damage.</typeparam>
        public virtual AttackResult<TSource> ReceiveDamage<TSource>(
            int damage,
            Character user)
        {
            if (user.Id != Id && ProtectQueue.Any())
            {
                var protectUser = ConsumeProtect();

                return new AttackResult<TSource>
                {
                    Applied = false,
                    User = user,
                    Target = this,
                    TargetProtected = true,
                    ProtectUser = protectUser,
                };
            }

            var startingHealth = CurrentHealth;
            CurrentHealth -= damage;
            var endingHealth = CurrentHealth;

            return new AttackResult<TSource>
            {
                Applied = true,
                User = user,
                Target = this,
                TargetProtected = false,
                StartingHealth = startingHealth,
                EndingHealth = endingHealth,
            };
        }

        /// <summary>
        /// Receives effects from the given buff and returns the result.
        /// </summary>
        /// <param name="multipliers">The effects of incoming buff.</param>
        /// <param name="user">The character who used the incoming buff.</param>
        /// <typeparam name="TSource">The type of the source of the incoming buff.</typeparam>
        public virtual BuffResult<TSource> ReceiveBuff<TSource>(
            IDictionary<StatCategory, double> multipliers,
            Character user)
        {
            if (user.Id != Id && ProtectQueue.Any())
            {
                var protectUser = ConsumeProtect();

                return new BuffResult<TSource>
                {
                    Applied = false,
                    User = user,
                    Target = this,
                    TargetProtected = true,
                    ProtectUser = protectUser,
                };
            }

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

            return new BuffResult<TSource>
            {
                Applied = true,
                User = user,
                Target = this,
                TargetProtected = false,
                StartingStatMultipliers = startingMultipliers,
                EndingStatMultipliers = endingMultipliers,
            };
        }

        /// <summary>
        /// Restores the given amount of health, capped by the character's max health, and returns the result.
        /// </summary>
        /// <param name="amount">The healing amount.</param>
        /// <param name="user">The character who used the incoming heal.</param>
        /// <typeparam name="TSource">The type of the source of the incoming heal.</typeparam>
        public virtual HealResult<TSource> Heal<TSource>(
            int amount,
            Character user)
        {
            if (user.Id != Id && ProtectQueue.Any())
            {
                var protectUser = ConsumeProtect();

                return new HealResult<TSource>
                {
                    Applied = false,
                    User = user,
                    Target = this,
                    TargetProtected = true,
                    ProtectUser = protectUser,
                };
            }

            var startingHealth = CurrentHealth;
            CurrentHealth += Math.Min(MaxHealth - CurrentHealth, amount);
            var endingHealth = CurrentHealth;

            return new HealResult<TSource>
            {
                Applied = true,
                User = user,
                Target = this,
                TargetProtected = false,
                StartingHealth = startingHealth,
                EndingHealth = endingHealth,
            };
        }

        /// <summary>
        /// Adds an item to the protect queue, which protects the character from the next attack.
        /// </summary>
        /// <param name="user">The character who protected this character.</param>
        /// <typeparam name="TSource">The type of the source of the incoming protect action.</typeparam>
        public virtual ProtectResult<TSource> AddProtect<TSource>(Character user)
        {
            if (user.Id != Id && ProtectQueue.Any())
            {
                var protectUser = ConsumeProtect();

                return new ProtectResult<TSource>
                {
                    Applied = false,
                    User = user,
                    Target = this,
                    TargetProtected = true,
                    ProtectUser = protectUser,
                };
            }

            if (ProtectCount >= ProtectLimit)
            {
                return new ProtectResult<TSource>
                {
                    Applied = false,
                    User = user,
                    Target = this,
                };
            }

            ProtectQueue.Add(user);

            return new ProtectResult<TSource>
            {
                Applied = true,
                User = user,
                Target = this,
                TargetProtected = false,
            };
        }

        /// <summary>
        /// Changes the protect limit by the given amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="user">The character who caused this protect limit change.</param>
        /// <typeparam name="TSource">The type of the source of the incoming protect limit change.</typeparam>
        public ProtectLimitChangeResult<TSource> ChangeProtectLimit<TSource>(
            int amount,
            Character user)
        {
            if (user.Id != Id && ProtectQueue.Any())
            {
                var protectUser = ConsumeProtect();

                return new ProtectLimitChangeResult<TSource>
                {
                    Applied = false,
                    User = user,
                    Target = this,
                    TargetProtected = true,
                    ProtectUser = protectUser,
                };
            }

            ProtectLimit += amount;

            return new ProtectLimitChangeResult<TSource>
            {
                Applied = true,
                User = user,
                Target = this,
                TargetProtected = false,
                Amount = amount,
            };
        }

        /// <summary>
        /// Pops the next protect action from the queue and returns the protecting character.
        /// </summary>
        public Character ConsumeProtect()
        {
            if (ProtectQueue.Count <= 0)
            {
                throw new InvalidOperationException($"Cannot consume a protect action because there are none in the queue!");
            }

            var protectingCharacter = ProtectQueue[0];
            ProtectQueue.RemoveAt(0);
            return protectingCharacter;
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
