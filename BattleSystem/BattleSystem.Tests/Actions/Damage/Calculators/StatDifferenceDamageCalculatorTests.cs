using BattleSystem.Actions.Damage.Calculators;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Damage.Calculators
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
        public void Calculate_ReturnsDamage(int userAttack, int power, int targetDefence, int expectedAmount)
        {
            // Arrange
            var calculator = new StatDifferenceDamageCalculator();

            var user = TestHelpers.CreateBasicCharacter(attack: userAttack);
            var damage = TestHelpers.CreateDamageAction(calculator, power: power);
            var target = TestHelpers.CreateBasicCharacter(defence: targetDefence);

            // Act
            var amount = calculator.Calculate(user, damage, target);

            // Assert
            Assert.That(amount, Is.EqualTo(expectedAmount));
        }
    }
}
