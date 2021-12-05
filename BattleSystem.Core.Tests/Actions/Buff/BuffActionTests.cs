using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Actions.Buff;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Characters.Targets;
using BattleSystem.Core.Stats;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.Buff
{
    /// <summary>
    /// Unit tests for <see cref="BuffAction"/>.
    /// </summary>
    [TestFixture]
    public class BuffActionTests
    {
        [Test]
        public void Use_CalculationSuccessfulWithTargets_BuffActionsTargetStat()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(attack: 10)
            };

            var buff = CreateBuffAction(
                new OthersActionTargetCalculator(),
                new Dictionary<StatCategory, double>
                {
                    [StatCategory.Attack] = 0.2
                });

            buff.SetTargets(user, otherCharacters);

            // Act
            _ = buff.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].CurrentAttack, Is.EqualTo(12));
        }

        [Test]
        public void Use_CalculationSuccessfulWithTargets_SucceedsAndAppliesActions()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            var buff = CreateBuffAction(new OthersActionTargetCalculator());

            buff.SetTargets(user, otherCharacters);

            // Act
            var result = buff.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
                Assert.That(result.Results, Is.Not.Empty);
            });
        }

        [Test]
        public void Use_CalculationSuccessfulAllTargetsDead_SucceedsAndAppliesNoActions()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(maxHealth: 0)
            };

            var buff = CreateBuffAction(new OthersActionTargetCalculator());

            buff.SetTargets(user, otherCharacters);

            // Act
            var result = buff.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
                Assert.That(result.Results, Is.Empty);
            });
        }

        [Test]
        public void Use_CalculationSuccessfulNoTargets_SucceedsAndAppliesNoActions()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            var actionTargetCalculator = new Mock<IActionTargetCalculator>();
            actionTargetCalculator
                .Setup(
                    m => m.Calculate(
                        It.IsAny<Character>(),
                        It.IsAny<IEnumerable<Character>>()
                    )
                )
                .Returns((true, Enumerable.Empty<Character>()));

            var buff = CreateBuffAction(
                actionTargetCalculator: actionTargetCalculator.Object);

            buff.SetTargets(user, otherCharacters);

            // Act
            var result = buff.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
                Assert.That(result.Results, Is.Empty);
            });
        }

        [Test]
        public void Use_NoTargetsSet_FailsAndAppliesNoActions()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(),
            };

            var buff = CreateBuffAction();

            // Act
            var result = buff.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.Results, Is.Empty);
            });
        }

        private static BuffAction CreateBuffAction(
            IActionTargetCalculator actionTargetCalculator = null,
            IDictionary<StatCategory, double> targetMultipliers = null)
        {
            var builder = new BuffActionBuilder()
                            .WithActionTargetCalculator(actionTargetCalculator ?? Mock.Of<IActionTargetCalculator>());

            if (targetMultipliers is not null)
            {
                foreach (var multiplier in targetMultipliers)
                {
                    builder = builder.WithTargetMultiplier(multiplier.Key, multiplier.Value);
                }
            }

            return builder.Build();
        }
    }
}
