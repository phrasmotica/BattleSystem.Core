using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Actions.Protect;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Characters.Targets;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.Protect
{
    /// <summary>
    /// Unit tests for <see cref="ProtectAction"/>.
    /// </summary>
    [TestFixture]
    public class ProtectActionTests
    {
        [Test]
        public void Use_CalculationSuccessfulWithTargets_BumpsTargetsProtectCounter()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            var protect = CreateProtectAction(new OthersActionTargetCalculator());

            protect.SetTargets(user, otherCharacters);

            // Act
            _ = protect.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].ProtectCount, Is.EqualTo(1));
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

            var protect = CreateProtectAction(new OthersActionTargetCalculator());

            protect.SetTargets(user, otherCharacters);

            // Act
            var result = protect.Use<string>(user, otherCharacters);

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

            var protect = CreateProtectAction(new OthersActionTargetCalculator());

            protect.SetTargets(user, otherCharacters);

            // Act
            var result = protect.Use<string>(user, otherCharacters);

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

            var protect = CreateProtectAction(
                actionTargetCalculator: actionTargetCalculator.Object);

            protect.SetTargets(user, otherCharacters);

            // Act
            var result = protect.Use<string>(user, otherCharacters);

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

            var protect = CreateProtectAction();

            // Act
            var result = protect.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.Results, Is.Empty);
            });
        }

        private static ProtectAction CreateProtectAction(
            IActionTargetCalculator actionTargetCalculator = null)
        {
            return new ProtectActionBuilder()
                .WithActionTargetCalculator(actionTargetCalculator ?? Mock.Of<IActionTargetCalculator>())
                .Build();
        }
    }
}
