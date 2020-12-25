﻿using BattleSystem.Characters;
using BattleSystem.Damage;
using BattleSystem.Moves.Actions;
using BattleSystem.Moves.Targets;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Actions
{
    /// <summary>
    /// Unit tests for <see cref="Attack"/>.
    /// </summary>
    [TestFixture]
    public class AttackTests
    {
        [Test]
        public void Use_WithTargets_DamagesTargets()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(maxHealth: 8)
            };

            var damageCalculator = new Mock<IDamageCalculator>();
            damageCalculator
                .Setup(
                    m => m.Calculate(
                        It.IsAny<Character>(),
                        It.IsAny<Attack>(),
                        It.IsAny<Character>()
                    )
                )
                .Returns(6);

            var attack = TestHelpers.CreateAttack(damageCalculator.Object, new OthersMoveTargetCalculator());

            // Act
            _ = attack.Use(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].CurrentHealth, Is.EqualTo(2));
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

            var attack = TestHelpers.CreateAttack(moveTargetCalculator: new OthersMoveTargetCalculator());

            // Act
            var actionResults = attack.Use(user, otherCharacters);

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

            var attack = TestHelpers.CreateAttack(moveTargetCalculator: new OthersMoveTargetCalculator());

            // Act
            var actionResults = attack.Use(user, otherCharacters);

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

            var attack = TestHelpers.CreateAttack();

            // Act
            var actionResults = attack.Use(user, otherCharacters);

            // Assert
            Assert.That(actionResults, Is.Empty);
        }
    }
}
