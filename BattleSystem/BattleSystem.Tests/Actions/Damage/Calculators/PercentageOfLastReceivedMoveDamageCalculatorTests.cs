using BattleSystem.Actions.Damage.Calculators;
using BattleSystem.Actions.Results;
using BattleSystem.Moves;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Damage.Calculators
{
    /// <summary>
    /// Unit tests for <see cref="PercentageOfLastReceivedMoveDamageCalculator"/>.
    /// </summary>
    [TestFixture]
    public class PercentageOfLastReceivedMoveDamageCalculatorTests
    {
        [TestCase(1, 1)]
        [TestCase(5, 1)]
        [TestCase(10, 2)]
        [TestCase(50, 10)]
        [TestCase(100, 20)]
        public void Calculate_ReturnsCalculatedAmount(int power, int expectedAmount)
        {
            // Arrange
            var calculator = new PercentageOfLastReceivedMoveDamageCalculator(10);

            var user = TestHelpers.CreateBasicCharacter();
            var damage = TestHelpers.CreateDamage(calculator, power: power);
            var target = TestHelpers.CreateBasicCharacter();

            var result = new DamageResult<Move>
            {
                StartingHealth = 30,
                EndingHealth = 10,
            };
            user.ActionHistory.AddAction(result);

            // Act
            var amount = calculator.Calculate(user, damage, target);

            // Assert
            Assert.That(amount, Is.EqualTo(expectedAmount));
        }

        [Test]
        public void Calculate_NoPreviousDamageResult_ReturnsDefaultAmount()
        {
            // Arrange
            var calculator = new PercentageOfLastReceivedMoveDamageCalculator(10);

            var user = TestHelpers.CreateBasicCharacter();
            var damage = TestHelpers.CreateDamage();
            var target = TestHelpers.CreateBasicCharacter();

            // Act
            var amount = calculator.Calculate(user, damage, target);

            // Assert
            Assert.That(amount, Is.EqualTo(10));
        }
    }
}
