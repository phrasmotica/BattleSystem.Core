using BattleSystem.Actions.Damage.Calculators;
using NUnit.Framework;
using static BattleSystem.Actions.Damage.Calculators.BasePowerDamageCalculator;

namespace BattleSystem.Tests.Actions.Damage.Calculators
{
    /// <summary>
    /// Unit tests for <see cref="BasePowerDamageCalculator"/>.
    /// </summary>
    [TestFixture]
    public class BasePowerDamageCalculatorTests
    {
        [TestCase(6, 10, 4, 16, 20)]
        [TestCase(6, 10, 5, 8, 10)]
        [TestCase(6, 10, 6, 1, 1)]
        [TestCase(6, 10, 7, 1, 1)]
        public void Calculate_NoPowerTransforms_ReturnsDamage(
            int userAttack,
            int basePower,
            int targetDefence,
            int expectedLowerBound,
            int expectedUpperBound)
        {
            // Arrange
            var calculator = new BasePowerDamageCalculator(basePower);

            var user = TestHelpers.CreateBasicCharacter(attack: userAttack);
            var damage = TestHelpers.CreateDamageAction(calculator);
            var target = TestHelpers.CreateBasicCharacter(defence: targetDefence);

            // Act
            var (success, amount) = calculator.Calculate(user, damage, target);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(amount, Is.InRange(expectedLowerBound, expectedUpperBound));
            });
        }

        [Test]
        public void Calculate_WithPowerTransforms_ReturnsDamage()
        {
            // Arrange
            var calculator = new BasePowerDamageCalculator(10);

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
            var (success, amount) = calculator.Calculate(user, damage, target);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(amount, Is.InRange(32, 40));
            });
        }
    }
}
