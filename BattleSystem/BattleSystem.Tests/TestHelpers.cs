using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Damage;
using BattleSystem.Healing;
using BattleSystem.Moves;
using BattleSystem.Stats;
using Moq;

namespace BattleSystem.Tests
{
    /// <summary>
    /// Helper methods for unit tests.
    /// </summary>
    public static class TestHelpers
    {
        /// <summary>
        /// Returns a basic character.
        /// </summary>
        public static BasicCharacter CreateBasicCharacter(string name = "yeti", int maxHealth = 5, int attack = 1, int defence = 1, int speed = 1)
        {
            var statSet = CreateStatSet();
            statSet.Attack.BaseValue = attack;
            statSet.Defence.BaseValue = defence;
            statSet.Speed.BaseValue = speed;

            return new BasicCharacter(name, maxHealth, statSet, CreateMoveSet());
        }

        /// <summary>
        /// Returns a stat set with default base values in each stat.
        /// </summary>
        public static StatSet CreateStatSet()
        {
            return new StatSet
            {
                Attack = CreateStat(),
                Defence = CreateStat(),
                Speed = CreateStat(),
            };
        }

        /// <summary>
        /// Returns a stat with the given base value.
        /// </summary>
        public static Stat CreateStat(int baseValue = 1)
        {
            return new Stat(baseValue);
        }

        /// <summary>
        /// Returns a move set with one move.
        /// </summary>
        public static MoveSet CreateMoveSet()
        {
            return new MoveSet
            {
                Move1 = CreateAttack(new Mock<IDamageCalculator>().Object),
            };
        }

        /// <summary>
        /// Returns a basic attack with the given max uses and power.
        /// </summary>
        public static Attack CreateAttack(IDamageCalculator damageCalculator, string name = "yeti", int maxUses = 5, int power = 2)
        {
            return new Attack(damageCalculator, name, maxUses, power);
        }

        /// <summary>
        /// Returns a basic buff with the given max uses.
        /// </summary>
        public static Buff CreateBuff(string name = "yeti", int maxUses = 5)
        {
            var userMultipliers = new Dictionary<StatCategory, double>
            {
                [StatCategory.Attack] = 0.2,
            };

            return new Buff(name, maxUses, userMultipliers, null);
        }

        /// <summary>
        /// Returns a basic heal with the given max uses, amount and mode.
        /// </summary>
        public static Heal CreateHeal(IHealingCalculator healingCalculator, string name = "yeti", int maxUses = 5, int amount = 5)
        {
            return new Heal(healingCalculator, name, maxUses, amount);
        }
    }
}
