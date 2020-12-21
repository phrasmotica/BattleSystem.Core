using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Damage;
using BattleSystem.Healing;
using BattleSystem.Moves;
using BattleSystem.Moves.Actions;
using BattleSystem.Moves.Success;
using BattleSystem.Moves.Targets;
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
        public static BasicCharacter CreateBasicCharacter(
            string name = "yeti",
            int maxHealth = 5,
            int attack = 1,
            int defence = 1,
            int speed = 1,
            MoveSet moveSet = null)
        {
            var statSet = CreateStatSet();
            statSet.Attack.BaseValue = attack;
            statSet.Defence.BaseValue = defence;
            statSet.Speed.BaseValue = speed;

            return new BasicCharacter(
                name,
                maxHealth,
                statSet,
                moveSet ?? CreateMoveSet());
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
        /// Returns a move set with the given moves.
        /// </summary>
        public static MoveSet CreateMoveSet(params Move[] moves)
        {
            var moveSet = new MoveSet();

            foreach (var move in moves)
            {
                moveSet.AddMove(move);
            }

            return moveSet;
        }

        /// <summary>
        /// Returns a move with the given actions.
        /// </summary>
        public static Move CreateMove(
            string name = "yeti",
            string description = "amon",
            int maxUses = 5,
            ISuccessCalculator successCalculator = null,
            params IMoveAction[] moveActions)
        {
            var builder = new MoveBuilder()
                            .Name(name)
                            .Describe(description)
                            .WithMaxUses(maxUses)
                            .WithSuccessCalculator(successCalculator ?? new AlwaysSuccessCalculator());

            foreach (var action in moveActions)
            {
                builder = builder.WithAction(action);
            }

            return builder.Build();
        }

        /// <summary>
        /// Returns a basic attack with the given max uses and power.
        /// </summary>
        public static Attack CreateAttack(
            IDamageCalculator damageCalculator = null,
            IMoveTargetCalculator moveTargetCalculator = null,
            int power = 2)
        {
            return new AttackBuilder()
                .WithPower(power)
                .WithDamageCalculator(damageCalculator ?? new Mock<IDamageCalculator>().Object)
                .WithMoveTargetCalculator(moveTargetCalculator ?? new Mock<IMoveTargetCalculator>().Object)
                .Build();
        }

        /// <summary>
        /// Returns a basic buff with the given max uses.
        /// </summary>
        public static Buff CreateBuff(
            IMoveTargetCalculator moveTargetCalculator = null,
            IDictionary<StatCategory, double> targetMultipliers = null)
        {
            var builder = new BuffBuilder()
                            .WithMoveTargetCalculator(moveTargetCalculator ?? new Mock<IMoveTargetCalculator>().Object);

            if (targetMultipliers is not null)
            {
                foreach (var multiplier in targetMultipliers)
                {
                    builder = builder.WithTargetMultiplier(multiplier.Key, multiplier.Value);
                }
            }

            return builder.Build();
        }

        /// <summary>
        /// Returns a basic heal with the given max uses, amount and mode.
        /// </summary>
        public static Heal CreateHeal(
            IHealingCalculator healingCalculator = null,
            IMoveTargetCalculator moveTargetCalculator = null,
            int amount = 5)
        {
            return new Heal(
                healingCalculator ?? new Mock<IHealingCalculator>().Object,
                moveTargetCalculator ?? new Mock<IMoveTargetCalculator>().Object,
                amount);
        }
    }
}
