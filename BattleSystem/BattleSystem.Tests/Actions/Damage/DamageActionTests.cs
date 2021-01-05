using System.Collections.Generic;
using System.Linq;
using BattleSystem.Actions.Damage;
using BattleSystem.Actions.Damage.Calculators;
using BattleSystem.Actions.Targets;
using BattleSystem.Characters;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Damage
{
    /// <summary>
    /// Unit tests for <see cref="DamageAction"/>.
    /// </summary>
    [TestFixture]
    public class DamageActionTests
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
                        It.IsAny<DamageAction>(),
                        It.IsAny<Character>()
                    )
                )
                .Returns((true, 6));

            var damage = TestHelpers.CreateDamageAction(
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

            var damage = TestHelpers.CreateDamageAction(
                damageCalculator: new AbsoluteDamageCalculator(5),
                actionTargetCalculator: new OthersActionTargetCalculator());

            damage.SetTargets(user, otherCharacters);

            // Act
            var result = damage.Use<string>(user, otherCharacters);

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
                TestHelpers.CreateBasicCharacter(maxHealth: 0)
            };

            var damage = TestHelpers.CreateDamageAction(
                actionTargetCalculator: new OthersActionTargetCalculator());

            damage.SetTargets(user, otherCharacters);

            // Act
            var result = damage.Use<string>(user, otherCharacters);

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

            var damage = TestHelpers.CreateDamageAction(
                actionTargetCalculator: actionTargetCalculator.Object);

            damage.SetTargets(user, otherCharacters);

            // Act
            var result = damage.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
                Assert.That(result.Results, Is.Empty);
            });
        }

        [Test]
        public void Use_CalculationUnsuccessful_FailsAndAppliesNoActions()
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
                .Returns((false, Enumerable.Empty<Character>()));

            var damage = TestHelpers.CreateDamageAction(
                actionTargetCalculator: actionTargetCalculator.Object);

            damage.SetTargets(user, otherCharacters);

            // Act
            var result = damage.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
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

            var damage = TestHelpers.CreateDamageAction();

            // Act and Assert
            var result = damage.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.Results, Is.Empty);
            });
        }
    }
}
