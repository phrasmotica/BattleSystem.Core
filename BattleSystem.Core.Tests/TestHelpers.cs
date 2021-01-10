using BattleSystem.Core.Actions;
using BattleSystem.Core.Actions.Damage;
using BattleSystem.Core.Actions.Damage.Calculators;
using BattleSystem.Core.Actions.Heal;
using BattleSystem.Core.Actions.Heal.Calculators;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Characters.Targets;
using BattleSystem.Core.Items;
using BattleSystem.Core.Moves;
using BattleSystem.Core.Moves.Success;
using BattleSystem.Core.Random;
using BattleSystem.Core.Stats;
using Moq;
using static BattleSystem.Core.Actions.ActionContainer;
using static BattleSystem.Core.Actions.Damage.Calculators.BasePowerDamageCalculator;
using static BattleSystem.Core.Items.Item;
using static BattleSystem.Core.Moves.Move;

namespace BattleSystem.Core.Tests
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
            MoveSet moveSet = null,
            IRandom random = null)
        {
            return new BasicCharacter(
                name,
                team,
                maxHealth,
                CreateStatSet(attack, defence, speed),
                moveSet ?? CreateMoveSet(),
                random ?? new Mock<IRandom>().Object);
        }

        /// <summary>
        /// Returns a stat set the given base values in each stat.
        /// </summary>
        private static StatSet CreateStatSet(int attack, int defence, int speed)
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
        private static Stat CreateStat(int baseValue = 1)
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
        /// Returns an item.
        /// </summary>
        public static Item CreateItem(
            string name = "jim",
            string description = "eureka",
            StatValueTransform[] attackValueTransforms = null,
            StatValueTransform[] defenceValueTransforms = null,
            StatValueTransform[] speedValueTransforms = null,
            PowerTransform[] damagePowerTransforms = null)
        {
            var builder = new ItemBuilder()
                            .Name(name)
                            .Describe(description);

            if (attackValueTransforms is not null)
            {
                foreach (var t in attackValueTransforms)
                {
                    builder = builder.WithAttackValueTransform(t);
                }
            }

            if (defenceValueTransforms is not null)
            {
                foreach (var t in defenceValueTransforms)
                {
                    builder = builder.WithDefenceValueTransform(t);
                }
            }

            if (speedValueTransforms is not null)
            {
                foreach (var t in speedValueTransforms)
                {
                    builder = builder.WithSpeedValueTransform(t);
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
            MoveSuccessCalculatorFactory successCalculatorFactory = null,
            params IAction[] moveActions)
        {
            if (successCalculatorFactory is null)
            {
                successCalculatorFactory = () => new AlwaysMoveSuccessCalculator();
            }

            var builder = new MoveBuilder()
                            .Name(name)
                            .Describe(description)
                            .WithMaxUses(maxUses)
                            .WithSuccessCalculatorFactory(successCalculatorFactory);

            foreach (var action in moveActions)
            {
                builder = builder.WithAction(action);
            }

            return builder.Build();
        }

        /// <summary>
        /// Returns a damage action.
        /// </summary>
        public static DamageAction CreateDamageAction(
            IDamageCalculator damageCalculator = null,
            IActionTargetCalculator actionTargetCalculator = null)
        {
            return new DamageActionBuilder()
                .WithDamageCalculator(damageCalculator ?? new Mock<IDamageCalculator>().Object)
                .WithActionTargetCalculator(actionTargetCalculator ?? new Mock<IActionTargetCalculator>().Object)
                .Build();
        }

        /// <summary>
        /// Returns a heal action.
        /// </summary>
        public static HealAction CreateHeal(
            IHealingCalculator healingCalculator = null,
            IActionTargetCalculator actionTargetCalculator = null,
            int amount = 5)
        {
            return new HealActionBuilder()
                .WithAmount(amount)
                .WithHealingCalculator(healingCalculator ?? new Mock<IHealingCalculator>().Object)
                .WithActionTargetCalculator(actionTargetCalculator ?? new Mock<IActionTargetCalculator>().Object)
                .Build();
        }
    }
}
