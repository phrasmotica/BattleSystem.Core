using BattleSystem.Damage;
using BattleSystem.Tests;
using NUnit.Framework;

namespace BattleSystemTests.Damage
{
    /// <summary>
    /// Unit tests for <see cref="StatDifferenceDamageCalculator"/>.
    /// </summary>
    [TestFixture]
    public class StatDifferenceDamageCalculatorTests
    {
        [TestCase(6, 10, 4, 20)]
        [TestCase(6, 10, 5, 10)]
        [TestCase(6, 10, 6, 1)]
        [TestCase(6, 10, 7, 1)]
        public void Calculate_ReturnsDamage(int userAttack, int attackPower, int targetDefence, int expectedDamage)
        {
            // Arrange
            var calculator = new StatDifferenceDamageCalculator();

            var user = TestHelpers.CreateBasicCharacter(attack: userAttack);
            var attack = TestHelpers.CreateAttack(calculator, power: attackPower);
            var target = TestHelpers.CreateBasicCharacter(defence: targetDefence);

            // Act
            var damage = calculator.Calculate(user, attack, target);

            // Assert
            Assert.That(damage, Is.EqualTo(expectedDamage));
        }
    }
}
