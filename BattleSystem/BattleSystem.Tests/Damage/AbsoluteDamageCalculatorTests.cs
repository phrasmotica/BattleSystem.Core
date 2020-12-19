using BattleSystem.Damage;
using BattleSystem.Tests;
using NUnit.Framework;

namespace BattleSystemTests.Damage
{
    /// <summary>
    /// Unit tests for <see cref="AbsoluteDamageCalculator"/>.
    /// </summary>
    [TestFixture]
    public class AbsoluteDamageCalculatorTests
    {
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100)]
        public void Calculate_ReturnsDamage(int attackPower)
        {
            // Arrange
            var calculator = new AbsoluteDamageCalculator();

            var user = TestHelpers.CreateBasicCharacter();
            var attack = TestHelpers.CreateAttack(calculator, power: attackPower);
            var target = TestHelpers.CreateBasicCharacter();

            // Act
            var damage = calculator.Calculate(user, attack, target);

            // Assert
            Assert.That(damage, Is.EqualTo(attackPower));
        }
    }
}
