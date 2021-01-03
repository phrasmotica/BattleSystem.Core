using System.Collections.Generic;
using System.Linq;
using BattleSystem.Actions;
using BattleSystem.Actions.Targets;
using BattleSystem.Characters;
using BattleSystem.Stats;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions
{
    /// <summary>
    /// Unit tests for <see cref="Buff"/>.
    /// </summary>
    [TestFixture]
    public class BuffTests
    {
        [Test]
        public void Use_CalculationSuccessfulWithTargets_BuffsTargetStat()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(attack: 10)
            };

            var buff = TestHelpers.CreateBuff(
                new OthersActionTargetCalculator(),
                new Dictionary<StatCategory, double>
                {
                    [StatCategory.Attack] = 0.2
                });

            buff.SetTargets(user, otherCharacters);

            // Act
            _ = buff.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].Stats.Attack.CurrentValue, Is.EqualTo(12));
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

            var buff = TestHelpers.CreateBuff(new OthersActionTargetCalculator());

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

            var buff = TestHelpers.CreateBuff(new OthersActionTargetCalculator());

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

            var buff = TestHelpers.CreateBuff(
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

            var buff = TestHelpers.CreateBuff();

            // Act
            var result = buff.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.Results, Is.Empty);
            });
        }
    }
}
