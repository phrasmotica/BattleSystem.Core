using System;
using System.Linq;
using BattleSystem.Core.Actions.Damage.Calculators;
using BattleSystem.Core.Random;
using Moq;
using NUnit.Framework;
using static BattleSystem.Core.Actions.Damage.Calculators.BasePowerDamageCalculator;

namespace BattleSystem.Core.Tests.Actions.Damage.Calculators
{
    /// <summary>
    /// Unit tests for <see cref="BasePowerDamageCalculator"/>.
    /// </summary>
    [TestFixture]
    public class BasePowerDamageCalculatorTests
    {
        [Test]
        public void Ctor_NullRandom_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new BasePowerDamageCalculator(0, null));
        }

        [TestCase(6, 10, 4, 20)]
        [TestCase(6, 10, 5, 10)]
        [TestCase(6, 10, 6, 1)]
        [TestCase(6, 10, 7, 1)]
        public void Calculate_NoPowerTransforms_ReturnsDamage(
            int userAttack,
            int basePower,
            int targetDefence,
            int expectedAmount)
        {
            // Arrange
            var random = new Mock<IRandom>();
            random
                .Setup(m => m.Next(80, 101))
                .Returns(100);

            var calculator = new BasePowerDamageCalculator(basePower, random.Object);

            var user = TestHelpers.CreateBasicCharacter(attack: userAttack);
            var damage = TestHelpers.CreateDamageAction(calculator);
            var target = TestHelpers.CreateBasicCharacter(defence: targetDefence);

            // Act
            var calculation = calculator.Calculate(user, damage, new[] { target }).Single();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(calculation.Success, Is.True);
                Assert.That(calculation.Amount, Is.EqualTo(expectedAmount));
            });
        }

        [Test]
        public void Calculate_NoPowerTransforms_MultipleTargets_ReturnsSpreadDamage()
        {
            // Arrange
            var random = new Mock<IRandom>();
            random
                .Setup(m => m.Next(80, 101))
                .Returns(100);

            var calculator = new BasePowerDamageCalculator(10, random.Object);

            var user = TestHelpers.CreateBasicCharacter(attack: 6);
            var damage = TestHelpers.CreateDamageAction(calculator);
            var target1 = TestHelpers.CreateBasicCharacter(defence: 5);
            var target2 = TestHelpers.CreateBasicCharacter();

            // Act
            var calculation = calculator.Calculate(user, damage, new[] { target1, target2 }).First();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(calculation.Success, Is.True);
                Assert.That(calculation.Amount, Is.EqualTo(7));
            });
        }

        [Test]
        public void Calculate_WithPowerTransforms_ReturnsDamage()
        {
            // Arrange
            var random = new Mock<IRandom>();
            random
                .Setup(m => m.Next(80, 101))
                .Returns(100);

            var calculator = new BasePowerDamageCalculator(10, random.Object);

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
            var calculation = calculator.Calculate(user, damage, new[] { target }).Single();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(calculation.Success, Is.True);
                Assert.That(calculation.Amount, Is.EqualTo(40));
            });
        }
    }
}
