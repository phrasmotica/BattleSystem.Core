using System;
using BattleSystem.Actions;
using BattleSystem.Actions.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions
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

            var change = TestHelpers.CreateProtectLimitChange(new OthersActionTargetCalculator());

            change.SetTargets(user, otherCharacters);

            // Act
            _ = change.Use<string>(user, otherCharacters);

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

            var change = TestHelpers.CreateProtectLimitChange(new OthersActionTargetCalculator());

            change.SetTargets(user, otherCharacters);

            // Act
            var actionResults = change.Use<string>(user, otherCharacters);

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

            var change = TestHelpers.CreateProtectLimitChange(new OthersActionTargetCalculator());

            change.SetTargets(user, otherCharacters);

            // Act
            var actionResults = change.Use<string>(user, otherCharacters);

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

            change.SetTargets(user, otherCharacters);

            // Act
            var actionResults = change.Use<string>(user, otherCharacters);

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

            var change = TestHelpers.CreateProtect();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = change.Use<string>(user, otherCharacters));
        }
    }
}
