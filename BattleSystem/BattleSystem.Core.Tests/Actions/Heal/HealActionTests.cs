using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Actions.Heal;
using BattleSystem.Core.Actions.Heal.Calculators;
using BattleSystem.Core.Actions.Targets;
using BattleSystem.Core.Characters;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.Heal
{
    /// <summary>
    /// Unit tests for <see cref="HealAction"/>.
    /// </summary>
    [TestFixture]
    public class HealActionTests
    {
        [Test]
        public void Use_CalculationSuccessfulWithTargets_HealsTargets()
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
                        It.IsAny<HealAction>(),
                        It.IsAny<Character>()
                    )
                )
                .Returns(2);

            var heal = TestHelpers.CreateHeal(healingCalculator.Object, new OthersActionTargetCalculator());

            _ = otherCharacters[0].ReceiveDamage<string>(2, user);

            heal.SetTargets(user, otherCharacters);

            // Act
            heal.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].CurrentHealth, Is.EqualTo(5));
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

            var heal = TestHelpers.CreateHeal(actionTargetCalculator: new OthersActionTargetCalculator());

            heal.SetTargets(user, otherCharacters);

            // Act
            var result = heal.Use<string>(user, otherCharacters);

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

            var heal = TestHelpers.CreateHeal(
                actionTargetCalculator: new OthersActionTargetCalculator());

            heal.SetTargets(user, otherCharacters);

            // Act
            var result = heal.Use<string>(user, otherCharacters);

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

            var heal = TestHelpers.CreateHeal(
                actionTargetCalculator: actionTargetCalculator.Object);

            heal.SetTargets(user, otherCharacters);

            // Act
            var result = heal.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
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

            var heal = TestHelpers.CreateHeal();

            // Act
            var result = heal.Use<string>(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.Results, Is.Empty);
            });
        }
    }
}
