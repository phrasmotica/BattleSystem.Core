using BattleSystem.Actions.Damage.Calculators;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Damage.Calculators
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
        public void Calculate_ReturnsDamage(int amount)
        {
            // Arrange
            var calculator = new AbsoluteDamageCalculator(amount);

            var user = TestHelpers.CreateBasicCharacter();
            var damage = TestHelpers.CreateDamageAction(calculator);
            var target = TestHelpers.CreateBasicCharacter();

            // Act
            var actualAmount = calculator.Calculate(user, damage, target);

            // Assert
            Assert.That(actualAmount, Is.EqualTo(amount));
        }
    }
}
