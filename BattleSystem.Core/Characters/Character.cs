using System;
using System.Collections.Generic;
using BattleSystem.Core.Actions.Buff;
using BattleSystem.Core.Actions.Damage;
using BattleSystem.Core.Actions.Flinch;
using BattleSystem.Core.Actions.Heal;
using BattleSystem.Core.Actions.Protect;
using BattleSystem.Core.Actions.ProtectLimitChange;
using BattleSystem.Core.Items;
using BattleSystem.Core.Items.Results;
using BattleSystem.Core.Moves;
using BattleSystem.Core.Stats;

namespace BattleSystem.Core.Characters
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
        /// The character's stats.
        /// </summary>
        protected StatSet Stats;

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
        /// Gets or sets whether the character will flinch on their next move.
        /// </summary>
        public bool WillFlinch { get; set; }

        /// <summary>
        /// Gets the character's current attack stat value.
        /// </summary>
        public int CurrentAttack
        {
            get
            {
                var transformedValue = Stats.Attack.CurrentValue;

                if (HasItem)
                {
                    foreach (var t in Item.ActionContainer.StatValueTransforms[StatCategory.Attack])
                    {
                        transformedValue = t(transformedValue);
                    }
                }

                return transformedValue;
            }
        }

        /// <summary>
        /// Gets the character's current defence stat value.
        /// </summary>
        public int CurrentDefence
        {
            get
            {
                var transformedValue = Stats.Defence.CurrentValue;

                if (HasItem)
                {
                    foreach (var t in Item.ActionContainer.StatValueTransforms[StatCategory.Defence])
                    {
                        transformedValue = t(transformedValue);
                    }
                }

                return transformedValue;
            }
        }

        /// <summary>
        /// Gets the character's current speed stat value.
        /// </summary>
        public int CurrentSpeed
        {
            get
            {
                var transformedValue = Stats.Speed.CurrentValue;

                if (HasItem)
                {
                    foreach (var t in Item.ActionContainer.StatValueTransforms[StatCategory.Speed])
                    {
                        transformedValue = t(transformedValue);
                    }
                }

                return transformedValue;
            }
        }

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
        public virtual DamageActionResult<TSource> ReceiveDamage<TSource>(
            int damage,
            Character user)
        {
            DamageActionResult<TSource> result;

            if (CanProtectFrom(user))
            {
                var protectUser = ConsumeProtect();

                result =  new DamageActionResult<TSource>
                {
                    Applied = false,
                    User = user,
                    Target = this,
                    TargetProtected = true,
                    ProtectUser = protectUser,
                    Tags = new HashSet<string>(),
                };
            }
            else
            {
                var startingHealth = CurrentHealth;
                CurrentHealth -= damage;
                var endingHealth = CurrentHealth;

                result = new DamageActionResult<TSource>
                {
                    Applied = true,
                    User = user,
                    Target = this,
                    TargetProtected = false,
                    StartingHealth = startingHealth,
                    EndingHealth = endingHealth,
                    Tags = new HashSet<string>(),
                };
            }

            return result;
        }

        /// <summary>
        /// Receives effects from the given buff and returns the result.
        /// </summary>
        /// <param name="multipliers">The effects of incoming buff.</param>
        /// <param name="user">The character who used the incoming buff.</param>
        /// <typeparam name="TSource">The type of the source of the incoming buff.</typeparam>
        public virtual BuffActionResult<TSource> ReceiveBuff<TSource>(
            IDictionary<StatCategory, double> multipliers,
            Character user)
        {
            BuffActionResult<TSource> result;

            if (CanProtectFrom(user))
            {
                var protectUser = ConsumeProtect();

                result = new BuffActionResult<TSource>
                {
                    Applied = false,
                    User = user,
                    Target = this,
                    TargetProtected = true,
                    ProtectUser = protectUser,
                    Tags = new HashSet<string>(),
                };
            }
            else
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

                result = new BuffActionResult<TSource>
                {
                    Applied = true,
                    User = user,
                    Target = this,
                    TargetProtected = false,
                    StartingStatMultipliers = startingMultipliers,
                    EndingStatMultipliers = endingMultipliers,
                    Tags = new HashSet<string>(),
                };
            }

            return result;
        }

        /// <summary>
        /// Sets the character to flinch and returns the result.
        /// </summary>
        /// <param name="user">The character who used the incoming flinch action.</param>
        /// <typeparam name="TSource">The type of the source of the incoming flinch action.</typeparam>
        public virtual FlinchActionResult<TSource> Flinch<TSource>(Character user)
        {
            FlinchActionResult<TSource> result;

            if (CanProtectFrom(user))
            {
                var protectUser = ConsumeProtect();

                result = new FlinchActionResult<TSource>
                {
                    Applied = false,
                    User = user,
                    Target = this,
                    TargetProtected = true,
                    ProtectUser = protectUser,
                    Tags = new HashSet<string>(),
                };
            }
            else
            {
                WillFlinch = true;

                result = new FlinchActionResult<TSource>
                {
                    Applied = true,
                    User = user,
                    Target = this,
                    TargetProtected = false,
                    Tags = new HashSet<string>(),
                };
            }

            return result;
        }

        /// <summary>
        /// Restores the given amount of health, capped by the character's max health, and returns the result.
        /// </summary>
        /// <param name="amount">The healing amount.</param>
        /// <param name="user">The character who used the incoming heal.</param>
        /// <typeparam name="TSource">The type of the source of the incoming heal.</typeparam>
        public virtual HealActionResult<TSource> Heal<TSource>(
            int amount,
            Character user)
        {
            HealActionResult<TSource> result;

            if (CanProtectFrom(user))
            {
                var protectUser = ConsumeProtect();

                result = new HealActionResult<TSource>
                {
                    Applied = false,
                    User = user,
                    Target = this,
                    TargetProtected = true,
                    ProtectUser = protectUser,
                    Tags = new HashSet<string>(),
                };
            }
            else
            {
                var startingHealth = CurrentHealth;
                CurrentHealth += Math.Min(MaxHealth - CurrentHealth, amount);
                var endingHealth = CurrentHealth;

                result = new HealActionResult<TSource>
                {
                    Applied = true,
                    User = user,
                    Target = this,
                    TargetProtected = false,
                    StartingHealth = startingHealth,
                    EndingHealth = endingHealth,
                    Tags = new HashSet<string>(),
                };
            }

            return result;
        }

        /// <summary>
        /// Adds an item to the protect queue, which protects the character from the next action.
        /// </summary>
        /// <param name="user">The character who protected this character.</param>
        /// <typeparam name="TSource">The type of the source of the incoming protect action.</typeparam>
        public virtual ProtectActionResult<TSource> AddProtect<TSource>(Character user)
        {
            ProtectActionResult<TSource> result;

            if (CanProtectFrom(user))
            {
                var protectUser = ConsumeProtect();

                result = new ProtectActionResult<TSource>
                {
                    Applied = false,
                    User = user,
                    Target = this,
                    TargetProtected = true,
                    ProtectUser = protectUser,
                    Tags = new HashSet<string>(),
                };
            }
            else if (ProtectCount >= ProtectLimit)
            {
                result = new ProtectActionResult<TSource>
                {
                    Applied = false,
                    User = user,
                    Target = this,
                    Tags = new HashSet<string>(),
                };
            }
            else
            {
                ProtectQueue.Add(user);

                result = new ProtectActionResult<TSource>
                {
                    Applied = true,
                    User = user,
                    Target = this,
                    TargetProtected = false,
                    Tags = new HashSet<string>(),
                };
            }

            return result;
        }

        /// <summary>
        /// Changes the protect limit by the given amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="user">The character who caused this protect limit change.</param>
        /// <typeparam name="TSource">The type of the source of the incoming protect limit change.</typeparam>
        public ProtectLimitChangeActionResult<TSource> ChangeProtectLimit<TSource>(
            int amount,
            Character user)
        {
            ProtectLimitChangeActionResult<TSource> result;

            if (CanProtectFrom(user))
            {
                var protectUser = ConsumeProtect();

                result = new ProtectLimitChangeActionResult<TSource>
                {
                    Applied = false,
                    User = user,
                    Target = this,
                    TargetProtected = true,
                    ProtectUser = protectUser,
                    Tags = new HashSet<string>(),
                };
            }
            else
            {
                ProtectLimit += amount;

                result = new ProtectLimitChangeActionResult<TSource>
                {
                    Applied = true,
                    User = user,
                    Target = this,
                    TargetProtected = false,
                    Amount = amount,
                    Tags = new HashSet<string>(),
                };
            }

            return result;
        }

        /// <summary>
        /// Returns whether the character can block an action from the given user.
        /// </summary>
        /// <param name="user">The character using the incoming action.</param>
        private bool CanProtectFrom(Character user)
        {
            return user.Id != Id && ProtectCount > 0;
        }

        /// <summary>
        /// Pops the next protect action from the queue and returns the protecting character.
        /// </summary>
        public Character ConsumeProtect()
        {
            if (ProtectCount <= 0)
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
