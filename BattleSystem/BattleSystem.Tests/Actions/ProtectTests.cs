using System;
using BattleSystem.Actions;
using BattleSystem.Actions.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions
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

            var protect = TestHelpers.CreateProtect(new OthersActionTargetCalculator());

            protect.SetTargets(user, otherCharacters);

            // Act
            _ = protect.Use<string>(user, otherCharacters);

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

            var protect = TestHelpers.CreateProtect(new OthersActionTargetCalculator());

            protect.SetTargets(user, otherCharacters);

            // Act
            var actionResults = protect.Use<string>(user, otherCharacters);

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

            var protect = TestHelpers.CreateProtect(new OthersActionTargetCalculator());

            protect.SetTargets(user, otherCharacters);

            // Act
            var actionResults = protect.Use<string>(user, otherCharacters);

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

            protect.SetTargets(user, otherCharacters);

            // Act
            var actionResults = protect.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(actionResults, Is.Empty);
        }

        [Test]
        public void Use_NoTargetsSet_Throws()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(),
            };

            var protect = TestHelpers.CreateProtect();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = protect.Use<string>(user, otherCharacters));
        }
    }
}
