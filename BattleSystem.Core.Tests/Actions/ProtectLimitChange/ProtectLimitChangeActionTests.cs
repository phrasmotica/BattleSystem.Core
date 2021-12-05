using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Actions.ProtectLimitChange;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Characters.Targets;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.ProtectLimitChange
{
    /// <summary>
    /// Unit tests for <see cref="ProtectLimitChangeAction"/>.
    /// </summary>
    [TestFixture]
    public class ProtectLimitChangeActionTests
    {
        [Test]
        public void Use_CalculationSuccessfulWithTargets_ChangesTargetsProtectLimit()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            var change = CreateProtectLimitChange(new OthersActionTargetCalculator());

            change.SetTargets(user, otherCharacters);

            // Act
            _ = change.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].ProtectLimit, Is.EqualTo(2));
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

            var change = CreateProtectLimitChange(new OthersActionTargetCalculator());

            change.SetTargets(user, otherCharacters);

            // Act
            var result = change.Use<string>(user, otherCharacters);

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

            var change = CreateProtectLimitChange(new OthersActionTargetCalculator());

            change.SetTargets(user, otherCharacters);

            // Act
            var result = change.Use<string>(user, otherCharacters);

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

            var change = CreateProtectLimitChange(
                actionTargetCalculator: actionTargetCalculator.Object);

            change.SetTargets(user, otherCharacters);

            // Act
            var result = change.Use<string>(user, otherCharacters);

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

            var change = CreateProtectLimitChange();

            // Act
            var result = change.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.Results, Is.Empty);
            });
        }

        private static ProtectLimitChangeAction CreateProtectLimitChange(
            IActionTargetCalculator actionTargetCalculator = null,
            int amount = 1)
        {
            return new ProtectLimitChangeActionBuilder()
                .WithActionTargetCalculator(actionTargetCalculator ?? Mock.Of<IActionTargetCalculator>())
                .WithAmount(amount)
                .Build();
        }
    }
}
