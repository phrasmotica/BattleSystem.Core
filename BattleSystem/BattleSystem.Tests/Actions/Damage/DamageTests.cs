using System;
using BattleSystem.Actions.Damage.Calulators;
using BattleSystem.Actions.Targets;
using BattleSystem.Characters;
using Moq;
using NUnit.Framework;
using static BattleSystem.Actions.Damage.Damage;

namespace BattleSystem.Tests.Actions.Damage
{
    /// <summary>
    /// Unit tests for <see cref="Damage"/>.
    /// </summary>
    [TestFixture]
    public class DamageTests
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
                        It.IsAny<BattleSystem.Actions.Damage.Damage>(),
                        It.IsAny<Character>()
                    )
                )
                .Returns(6);

            var damage = TestHelpers.CreateDamage(damageCalculator.Object, new OthersActionTargetCalculator());

            damage.SetTargets(user, otherCharacters);

            // Act
            _ = damage.Use<string>(user, otherCharacters);

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

            var damage = TestHelpers.CreateDamage(actionTargetCalculator: new OthersActionTargetCalculator());

            damage.SetTargets(user, otherCharacters);

            // Act
            var actionResults = damage.Use<string>(user, otherCharacters);

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

            var damage = TestHelpers.CreateDamage(actionTargetCalculator: new OthersActionTargetCalculator());

            damage.SetTargets(user, otherCharacters);

            // Act
            var actionResults = damage.Use<string>(user, otherCharacters);

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

            var damage = TestHelpers.CreateDamage();

            damage.SetTargets(user, otherCharacters);

            // Act
            var actionResults = damage.Use<string>(user, otherCharacters);

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

            var damage = TestHelpers.CreateDamage();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = damage.Use<string>(user, otherCharacters));
        }

        [Test]
        public void Power_Get_WithPowerTransforms_ReturnsTransformedPower()
        {
            // Arrange
            var damage = TestHelpers.CreateDamage(power: 10);

            var transforms = new PowerTransform[]
            {
                p => p + 10,
                p => p * 2,
            };
            var item = TestHelpers.CreateItem(damagePowerTransforms: transforms);

            damage.ReceiveTransforms(item);

            // Act and Assert
            Assert.That(damage.Power, Is.EqualTo(40));
        }
    }
}
