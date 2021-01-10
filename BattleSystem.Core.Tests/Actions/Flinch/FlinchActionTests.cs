using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Actions.Flinch;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Characters.Targets;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.Flinch
{
    /// <summary>
    /// Unit tests for <see cref="FlinchAction"/>.
    /// </summary>
    [TestFixture]
    public class FlinchActionTests
    {
        [Test]
        public void Use_CalculationSuccessfulWithTargets_CauseTargetsToFlinch()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(),
            };

            var flinch = CreateFlinchAction(new OthersActionTargetCalculator());

            flinch.SetTargets(user, otherCharacters);

            // Act
            _ = flinch.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].WillFlinch, Is.True);
        }

        [Test]
        public void Use_CalculationSuccessfulWithTargets_SucceedsAndAppliesActions()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(),
            };

            var flinch = CreateFlinchAction(new OthersActionTargetCalculator());

            flinch.SetTargets(user, otherCharacters);

            // Act
            var result = flinch.Use<string>(user, otherCharacters);

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
                TestHelpers.CreateBasicCharacter(maxHealth: 0),
            };

            var flinch = CreateFlinchAction(new OthersActionTargetCalculator());

            flinch.SetTargets(user, otherCharacters);

            // Act
            var result = flinch.Use<string>(user, otherCharacters);

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
                TestHelpers.CreateBasicCharacter(),
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

            var flinch = CreateFlinchAction(
                actionTargetCalculator: actionTargetCalculator.Object);

            flinch.SetTargets(user, otherCharacters);

            // Act
            var result = flinch.Use<string>(user, otherCharacters);

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

            var flinch = CreateFlinchAction();

            // Act
            var result = flinch.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.Results, Is.Empty);
            });
        }

        private static FlinchAction CreateFlinchAction(
            IActionTargetCalculator actionTargetCalculator = null)
        {
            var builder = new FlinchActionBuilder()
                            .WithActionTargetCalculator(actionTargetCalculator ?? new Mock<IActionTargetCalculator>().Object);

            return builder.Build();
        }
    }
}
