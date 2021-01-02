using System.Collections.Generic;
using BattleSystem.Actions;
using BattleSystem.Characters;
using BattleSystem.Healing;
using BattleSystem.Items;
using BattleSystem.Moves;
using BattleSystem.Moves.Success;
using BattleSystem.Actions.Damage;
using BattleSystem.Actions.Damage.Calulators;
using BattleSystem.Actions.Targets;
using BattleSystem.Stats;
using Moq;
using static BattleSystem.Actions.Damage.Damage;
using static BattleSystem.Items.Item;

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
            string team = "a",
            int maxHealth = 5,
            int attack = 1,
            int defence = 1,
            int speed = 1,
            MoveSet moveSet = null)
        {
            return new BasicCharacter(
                name,
                team,
                maxHealth,
                CreateStatSet(attack, defence, speed),
                moveSet ?? CreateMoveSet());
        }

        /// <summary>
        /// Returns a stat set with default base values in each stat.
        /// </summary>
        public static StatSet CreateStatSet(int attack, int defence, int speed)
        {
            return new StatSet
            {
                Attack = CreateStat(attack),
                Defence = CreateStat(defence),
                Speed = CreateStat(speed),
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
        /// Returns an item slot.
        /// </summary>
        public static ItemSlot CreateItemSlot()
        {
            return new ItemSlot();
        }

        /// <summary>
        /// Returns an item.
        /// </summary>
        public static Item CreateItem(
            string name = "jim",
            string description = "eureka",
            StatBaseValueTransform[] attackBaseValueTransforms = null,
            StatBaseValueTransform[] defenceBaseValueTransforms = null,
            StatBaseValueTransform[] speedBaseValueTransforms = null,
            PowerTransform[] damagePowerTransforms = null)
        {
            var builder = new ItemBuilder()
                            .Name(name)
                            .Describe(description);

            if (attackBaseValueTransforms is not null)
            {
                foreach (var t in attackBaseValueTransforms)
                {
                    builder = builder.WithAttackBaseValueTransform(t);
                }
            }

            if (defenceBaseValueTransforms is not null)
            {
                foreach (var t in defenceBaseValueTransforms)
                {
                    builder = builder.WithDefenceBaseValueTransform(t);
                }
            }

            if (speedBaseValueTransforms is not null)
            {
                foreach (var t in speedBaseValueTransforms)
                {
                    builder = builder.WithSpeedBaseValueTransform(t);
                }
            }

            if (damagePowerTransforms is not null)
            {
                foreach (var t in damagePowerTransforms)
                {
                    builder = builder.WithDamagePowerTransform(t);
                }
            }

            return builder.Build();
        }

        /// <summary>
        /// Returns a move with the given actions.
        /// </summary>
        public static Move CreateMove(
            string name = "yeti",
            string description = "amon",
            int maxUses = 5,
            ISuccessCalculator successCalculator = null,
            params IAction[] moveActions)
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
        /// Returns an attack action.
        /// </summary>
        public static Damage CreateDamage(
            IDamageCalculator damageCalculator = null,
            IActionTargetCalculator actionTargetCalculator = null,
            int power = 2)
        {
            return new DamageBuilder()
                .WithPower(power)
                .WithDamageCalculator(damageCalculator ?? new Mock<IDamageCalculator>().Object)
                .WithActionTargetCalculator(actionTargetCalculator ?? new Mock<IActionTargetCalculator>().Object)
                .Build();
        }

        /// <summary>
        /// Returns a buff action.
        /// </summary>
        public static Buff CreateBuff(
            IActionTargetCalculator actionTargetCalculator = null,
            IDictionary<StatCategory, double> targetMultipliers = null)
        {
            var builder = new BuffBuilder()
                            .WithActionTargetCalculator(actionTargetCalculator ?? new Mock<IActionTargetCalculator>().Object);

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
        /// Returns a heal action.
        /// </summary>
        public static Heal CreateHeal(
            IHealingCalculator healingCalculator = null,
            IActionTargetCalculator actionTargetCalculator = null,
            int amount = 5)
        {
            return new HealBuilder()
                .WithAmount(amount)
                .WithHealingCalculator(healingCalculator ?? new Mock<IHealingCalculator>().Object)
                .WithActionTargetCalculator(actionTargetCalculator ?? new Mock<IActionTargetCalculator>().Object)
                .Build();
        }

        /// <summary>
        /// Returns a protect action.
        /// </summary>
        public static Protect CreateProtect(
            IActionTargetCalculator actionTargetCalculator = null)
        {
            return new ProtectBuilder()
                .WithActionTargetCalculator(actionTargetCalculator ?? new Mock<IActionTargetCalculator>().Object)
                .Build();
        }

        /// <summary>
        /// Returns a protect limit change action.
        /// </summary>
        public static ProtectLimitChange CreateProtectLimitChange(
            IActionTargetCalculator actionTargetCalculator = null,
            int amount = 1)
        {
            return new ProtectLimitChangeBuilder()
                .WithActionTargetCalculator(actionTargetCalculator ?? new Mock<IActionTargetCalculator>().Object)
                .WithAmount(amount)
                .Build();
        }
    }
}
