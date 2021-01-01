﻿using BattleSystem.Characters;
using BattleSystem.Healing;
using BattleSystem.Actions;
using BattleSystem.Actions.Targets;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions
{
    /// <summary>
    /// Unit tests for <see cref="Heal"/>.
    /// </summary>
    [TestFixture]
    public class HealTests
    {
        [Test]
        public void Use_HealsTarget()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            var healingCalculator = new Mock<IHealingCalculator>();
            healingCalculator
                .Setup(
                    m => m.Calculate(
                        It.IsAny<Character>(),
                        It.IsAny<Heal>(),
                        It.IsAny<Character>()
                    )
                )
                .Returns(2);

            var heal = TestHelpers.CreateHeal(healingCalculator.Object, new OthersActionTargetCalculator());

            _ = otherCharacters[0].ReceiveDamage<string>(2, user);

            // Act
            heal.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].CurrentHealth, Is.EqualTo(5));
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

            var heal = TestHelpers.CreateHeal(actionTargetCalculator: new OthersActionTargetCalculator());

            // Act
            var actionResults = heal.Use<string>(user, otherCharacters);

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

            var heal = TestHelpers.CreateHeal(actionTargetCalculator: new OthersActionTargetCalculator());

            // Act
            var actionResults = heal.Use<string>(user, otherCharacters);

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

            var heal = TestHelpers.CreateHeal();

            // Act
            var actionResults = heal.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(actionResults, Is.Empty);
        }
    }
}
