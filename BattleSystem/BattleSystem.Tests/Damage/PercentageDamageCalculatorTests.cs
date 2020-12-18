using BattleSystem.Damage;
using BattleSystem.Tests;
using NUnit.Framework;

namespace BattleSystemTests.Damage
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
        public void Calculate_ReturnsDamage(int targetMaxHealth, int attackPower, int expectedDamage)
        {
            // Arrange
            var calculator = new PercentageDamageCalculator();

            var user = TestHelpers.CreateBasicCharacter();
            var attack = TestHelpers.CreateAttack(calculator, power: attackPower);
            var target = TestHelpers.CreateBasicCharacter(maxHealth: targetMaxHealth);

            // Act
            var damage = calculator.Calculate(user, attack, target);

            // Assert
            Assert.That(damage, Is.EqualTo(expectedDamage));
        }
    }
}
