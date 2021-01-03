using System.Collections.Generic;
using System.Linq;
using BattleSystem.Actions.Damage.Calculators;
using BattleSystem.Actions.Targets;
using BattleSystem.Characters;
using Moq;
using NUnit.Framework;
using static BattleSystem.Actions.Damage.Damage;

namespace BattleSystem.Tests.Actions.Damage
{
    /// <summary>
    /// Unit tests for <see cref="BattleSystem.Actions.Damage.Damage"/>.
    /// </summary>
    [TestFixture]
    public class DamageTests
    {
        [Test]
        public void Use_CalculationSuccessfulWithTargets_DamagesTargets()
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

            var damage = TestHelpers.CreateDamage(
                damageCalculator.Object,
                new OthersActionTargetCalculator());

            damage.SetTargets(user, otherCharacters);

            // Act
            _ = damage.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].CurrentHealth, Is.EqualTo(2));
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

            var damage = TestHelpers.CreateDamage(
                actionTargetCalculator: new OthersActionTargetCalculator());

            damage.SetTargets(user, otherCharacters);

            // Act
            var (success, results) = damage.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(results, Is.Not.Empty);
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

            var damage = TestHelpers.CreateDamage(
                actionTargetCalculator: new OthersActionTargetCalculator());

            damage.SetTargets(user, otherCharacters);

            // Act
            var (success, results) = damage.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(results, Is.Empty);
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

            var damage = TestHelpers.CreateDamage(
                actionTargetCalculator: actionTargetCalculator.Object);

            damage.SetTargets(user, otherCharacters);

            // Act
            var (success, results) = damage.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(results, Is.Empty);
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

            var damage = TestHelpers.CreateDamage();

            // Act and Assert
            var (success, results) = damage.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.False);
                Assert.That(results, Is.Empty);
            });
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
