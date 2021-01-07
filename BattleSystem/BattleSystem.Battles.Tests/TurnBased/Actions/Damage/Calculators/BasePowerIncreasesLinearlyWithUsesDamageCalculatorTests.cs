using System.Linq;
using BattleSystem.Battles.TurnBased;
using BattleSystem.Battles.TurnBased.Actions.Damage.Calculators;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Battles.Tests.TurnBased.Actions.Damage.Calculators
{
    /// <summary>
    /// Unit tests for <see cref="BasePowerIncreasesLinearlyWithUsesDamageCalculator"/>.
    /// </summary>
    [TestFixture]
    public class BasePowerIncreasesLinearlyWithUsesDamageCalculatorTests
    {
        [Test]
        public void Calculate_NoPreviousUses_UsesStartingBasePower()
        {
            // Arrange
            var damage = TestHelpers.CreateDamageAction();

            var user = TestHelpers.CreateBasicCharacter(attack: 6);
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(defence: 5),
            };

            var calculator = new BasePowerIncreasesLinearlyWithUsesDamageCalculator(10, 5, new Mock<IActionHistory>().Object);

            // Act
            var calculation = calculator.Calculate(user, damage, otherCharacters).Single();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(calculation.Success, Is.True);
                Assert.That(calculation.Amount, Is.InRange(8, 10));
            });
        }

        [Test]
        public void Calculate_WithPreviousUses_UsesIncreasedBasePower()
        {
            // Arrange
            var damage = TestHelpers.CreateDamageAction();

            var user = TestHelpers.CreateBasicCharacter(attack: 6);
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(defence: 5),
            };

            var actionHistory = new Mock<IActionHistory>();
            actionHistory
                .Setup(m => m.GetMoveDamageConsecutiveSuccessCount(damage, user))
                .Returns(1);

            var calculator = new BasePowerIncreasesLinearlyWithUsesDamageCalculator(10, 5, actionHistory.Object);

            // Act
            var calculation = calculator.Calculate(user, damage, otherCharacters).Single();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(calculation.Success, Is.True);
                Assert.That(calculation.Amount, Is.InRange(12, 15));
            });
        }
    }
}
