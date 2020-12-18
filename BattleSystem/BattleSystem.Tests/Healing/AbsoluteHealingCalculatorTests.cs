using BattleSystem.Healing;
using BattleSystem.Tests;
using NUnit.Framework;

namespace BattleSystemTests.Healing
{
    /// <summary>
    /// Unit tests for <see cref="AbsoluteHealingCalculator"/>.
    /// </summary>
    [TestFixture]
    public class AbsoluteHealingCalculatorTests
    {
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100)]
        public void Calculate_ReturnsHealing(int healAmount)
        {
            // Arrange
            var calculator = new AbsoluteHealingCalculator();

            var user = TestHelpers.CreateBasicCharacter();
            var attack = TestHelpers.CreateHeal(calculator, amount: healAmount);
            var target = TestHelpers.CreateBasicCharacter();

            // Act
            var amount = calculator.Calculate(user, attack, target);

            // Assert
            Assert.That(amount, Is.EqualTo(healAmount));
        }
    }
}
