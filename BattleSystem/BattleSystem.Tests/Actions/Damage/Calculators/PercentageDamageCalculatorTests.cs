using BattleSystem.Actions.Damage.Calculators;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Damage.Calculators
{
    /// <summary>
    /// Unit tests for <see cref="PercentageDamageCalculator"/>.
    /// </summary>
    [TestFixture]
    public class PercentageDamageCalculatorTests
    {
        [TestCase(20, 1, 1)]
        [TestCase(20, 5, 1)]
        [TestCase(20, 10, 2)]
        [TestCase(20, 50, 10)]
        [TestCase(20, 100, 20)]
        public void Calculate_ReturnsDamage(int targetMaxHealth, int percentage, int expectedAmount)
        {
            // Arrange
            var calculator = new PercentageDamageCalculator(percentage);

            var user = TestHelpers.CreateBasicCharacter();
            var damage = TestHelpers.CreateDamageAction(calculator);
            var target = TestHelpers.CreateBasicCharacter(maxHealth: targetMaxHealth);

            // Act
            var amount = calculator.Calculate(user, damage, target);

            // Assert
            Assert.That(amount, Is.EqualTo(expectedAmount));
        }
    }
}
