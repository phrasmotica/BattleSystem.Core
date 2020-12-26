using BattleSystem.Moves.Actions;
using BattleSystem.Moves.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Actions
{
    /// <summary>
    /// Unit tests for <see cref="Protect"/>.
    /// </summary>
    [TestFixture]
    public class ProtectTests
    {
        [Test]
        public void Use_WithTargets_BumpsTargetProtectCounter()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            var protect = TestHelpers.CreateProtect(new OthersMoveTargetCalculator());

            // Act
            _ = protect.Use(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].ProtectCount, Is.EqualTo(1));
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

            var protect = TestHelpers.CreateProtect(new OthersMoveTargetCalculator());

            // Act
            var actionResults = protect.Use(user, otherCharacters);

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

            var protect = TestHelpers.CreateProtect(new OthersMoveTargetCalculator());

            // Act
            var actionResults = protect.Use(user, otherCharacters);

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

            var protect = TestHelpers.CreateProtect();

            // Act
            var actionResults = protect.Use(user, otherCharacters);

            // Assert
            Assert.That(actionResults, Is.Empty);
        }
    }
}
