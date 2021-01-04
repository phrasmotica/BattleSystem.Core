using BattleSystem.Actions.Damage.Calculators;
using NUnit.Framework;
using static BattleSystem.Actions.Damage.Calculators.StatDifferenceDamageCalculator;

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
        public void Calculate_NoPowerTransforms_ReturnsDamage(int userAttack, int basePower, int targetDefence, int expectedAmount)
        {
            // Arrange
            var calculator = new StatDifferenceDamageCalculator(basePower);

            var user = TestHelpers.CreateBasicCharacter(attack: userAttack);
            var damage = TestHelpers.CreateDamageAction(calculator);
            var target = TestHelpers.CreateBasicCharacter(defence: targetDefence);

            // Act
            var amount = calculator.Calculate(user, damage, target);

            // Assert
            Assert.That(amount, Is.EqualTo(expectedAmount));
        }

        [Test]
        public void Calculate_WithPowerTransforms_ReturnsDamage()
        {
            // Arrange
            var calculator = new StatDifferenceDamageCalculator(10);

            var user = TestHelpers.CreateBasicCharacter(attack: 6);
            var damage = TestHelpers.CreateDamageAction(calculator);
            var target = TestHelpers.CreateBasicCharacter(defence: 5);

            var transforms = new PowerTransform[]
            {
                p => p + 10,
                p => p * 2,
            };
            var item = TestHelpers.CreateItem(damagePowerTransforms: transforms);
            _ = user.EquipItem(item);

            // Act
            var amount = calculator.Calculate(user, damage, target);

            // Assert
            Assert.That(amount, Is.EqualTo(40));
        }
    }
}
