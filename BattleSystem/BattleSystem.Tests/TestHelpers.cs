﻿using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Damage;
using BattleSystem.Healing;
using BattleSystem.Moves;
using BattleSystem.Moves.Actions;
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
        /// Returns a move set with one move.
        /// </summary>
        public static MoveSet CreateMoveSet(Move move1 = null)
        {
            return new MoveSet
            {
                Move1 = move1 ?? CreateMove(
                    moveActions: CreateAttack(new Mock<IDamageCalculator>().Object)
                ),
            };
        }

        /// <summary>
        /// Returns a move with the given actions.
        /// </summary>
        public static Move CreateMove(
            string name = "yeti",
            string description = "amon",
            int maxUses = 5,
            params IMoveAction[] moveActions)
        {
            var builder = new MoveBuilder()
                            .Name(name)
                            .Describe(description)
                            .WithMaxUses(maxUses);

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
            return new Attack(
                damageCalculator ?? new Mock<IDamageCalculator>().Object,
                moveTargetCalculator ?? new Mock<IMoveTargetCalculator>().Object,
                power);
        }

        /// <summary>
        /// Returns a basic buff with the given max uses.
        /// </summary>
        public static Buff CreateBuff(
            IMoveTargetCalculator moveTargetCalculator = null,
            IDictionary<StatCategory, double> userMultipliers = null,
            IDictionary<StatCategory, double> targetMultipliers = null)
        {
            return new Buff(
                moveTargetCalculator ?? new Mock<IMoveTargetCalculator>().Object,
                userMultipliers ?? new Dictionary<StatCategory, double>
                {
                    [StatCategory.Attack] = 0.2,
                },
                targetMultipliers);
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
