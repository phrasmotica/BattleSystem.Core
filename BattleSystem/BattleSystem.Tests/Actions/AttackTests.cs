using BattleSystem.Characters;
using BattleSystem.Damage;
using BattleSystem.Actions;
using BattleSystem.Actions.Targets;
using Moq;
using NUnit.Framework;
using static BattleSystem.Actions.Attack;
using System;
using System.Collections.Generic;
using BattleSystem.Actions.Results;

namespace BattleSystem.Tests.Actions
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

            var attack = TestHelpers.CreateAttack(damageCalculator.Object, new OthersActionTargetCalculator());

            attack.SetTargets(user, otherCharacters);

            // Act
            _ = attack.Use<string>(user, otherCharacters);

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

            var attack = TestHelpers.CreateAttack(actionTargetCalculator: new OthersActionTargetCalculator());

            attack.SetTargets(user, otherCharacters);

            // Act
            var actionResults = attack.Use<string>(user, otherCharacters);

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

            var attack = TestHelpers.CreateAttack(actionTargetCalculator: new OthersActionTargetCalculator());

            attack.SetTargets(user, otherCharacters);

            // Act
            var actionResults = attack.Use<string>(user, otherCharacters);

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

            attack.SetTargets(user, otherCharacters);

            // Act
            var actionResults = attack.Use<string>(user, otherCharacters);

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

            var attack = TestHelpers.CreateAttack();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = attack.Use<string>(user, otherCharacters));
        }

        [Test]
        public void Power_Get_WithPowerTransforms_ReturnsTransformedPower()
        {
            // Arrange
            var attack = TestHelpers.CreateAttack(power: 10);

            var transforms = new PowerTransform[]
            {
                p => p + 10,
                p => p * 2,
            };
            var item = TestHelpers.CreateItem(attackPowerTransforms: transforms);

            attack.ReceiveTransforms(item);

            // Act and Assert
            Assert.That(attack.Power, Is.EqualTo(40));
        }
    }
}
