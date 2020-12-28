using BattleSystem.Moves.Actions;
using BattleSystem.Moves.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Actions
{
    /// <summary>
    /// Unit tests for <see cref="ProtectLimitChange"/>.
    /// </summary>
    [TestFixture]
    public class ProtectLimitChangeTests
    {
        [Test]
        public void Use_WithTargets_BumpsTargetProtectLimitChange()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            var change = TestHelpers.CreateProtectLimitChange(new OthersMoveTargetCalculator());

            // Act
            _ = change.Use(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].ProtectLimit, Is.EqualTo(2));
        }

        [Test]
        public void Use_WithTargets_AppliesActions()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            var change = TestHelpers.CreateProtectLimitChange(new OthersMoveTargetCalculator());

            // Act
            var actionResults = change.Use(user, otherCharacters);

            // Assert
            Assert.That(actionResults, Is.Not.Empty);
        }

        [Test]
        public void Use_WithDeadTargets_AppliesNoActions()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();

            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(maxHealth: 0)
            };

            var change = TestHelpers.CreateProtectLimitChange(new OthersMoveTargetCalculator());

            // Act
            var actionResults = change.Use(user, otherCharacters);

            // Assert
            Assert.That(actionResults, Is.Empty);
        }

        [Test]
        public void Use_WithoutTargets_AppliesNoActions()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            var change = TestHelpers.CreateProtectLimitChange();

            // Act
            var actionResults = change.Use(user, otherCharacters);

            // Assert
            Assert.That(actionResults, Is.Empty);
        }
    }
}
