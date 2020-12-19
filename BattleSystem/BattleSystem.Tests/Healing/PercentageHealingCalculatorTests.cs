using BattleSystem.Healing;
using BattleSystem.Tests;
using NUnit.Framework;

namespace BattleSystemTests.Healing
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
        public void Calculate_ReturnsDamage(int targetMaxHealth, int healAmount, int expectedDamage)
        {
            // Arrange
            var calculator = new PercentageHealingCalculator();

            var user = TestHelpers.CreateBasicCharacter();
            var attack = TestHelpers.CreateHeal(calculator, amount: healAmount);
            var target = TestHelpers.CreateBasicCharacter(maxHealth: targetMaxHealth);

            // Act
            var damage = calculator.Calculate(user, attack, target);

            // Assert
            Assert.That(damage, Is.EqualTo(expectedDamage));
        }
    }
}
