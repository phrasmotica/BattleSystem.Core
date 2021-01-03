using BattleSystem.Actions.Heal.Calculators;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Heal.Calculators
{
    /// <summary>
    /// Unit tests for <see cref="PercentageHealingCalculator"/>.
    /// </summary>
    [TestFixture]
    public class PercentageHealingCalculatorTests
    {
        [TestCase(20, 1, 1)]
        [TestCase(20, 5, 1)]
        [TestCase(20, 10, 2)]
        [TestCase(20, 50, 10)]
        [TestCase(20, 100, 20)]
        public void Calculate_ReturnsDamage(int targetMaxHealth, int healAmount, int expectedAmount)
        {
            // Arrange
            var calculator = new PercentageHealingCalculator();

            var user = TestHelpers.CreateBasicCharacter();
            var heal = TestHelpers.CreateHeal(calculator, amount: healAmount);
            var target = TestHelpers.CreateBasicCharacter(maxHealth: targetMaxHealth);

            // Act
            var amount = calculator.Calculate(user, heal, target);

            // Assert
            Assert.That(amount, Is.EqualTo(expectedAmount));
        }
    }
}
