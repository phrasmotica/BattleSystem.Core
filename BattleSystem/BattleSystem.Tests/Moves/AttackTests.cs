using BattleSystem.Characters;
using BattleSystem.Damage;
using BattleSystem.Moves;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace BattleSystem.Tests.Moves
{
    /// <summary>
    /// Unit tests for <see cref="Attack"/>.
    /// </summary>
    [TestFixture]
    public class AttackTests
    {
        [TestCase(5, true)]
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(-5, false)]
        public void CanUse_ReturnsCorrectly(int remainingUses, bool expectedCanUse)
        {
            // Arrange
            var attack = TestHelpers.CreateAttack(new Mock<IDamageCalculator>().Object, "yeti", remainingUses, 1);
            Constraint constraint = expectedCanUse ? Is.True : Is.False;

            // Act and Assert
            Assert.That(attack.CanUse(), constraint);
        }

        [Test]
        public void Use_DamagesTarget()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 8);

            var damageCalculator = new Mock<IDamageCalculator>();
            damageCalculator
                .Setup(
                    m => m.Calculate(
                        It.IsAny<Character>(),
                        It.IsAny<Attack>(),
                        It.IsAny<Character>()
                    )
                )
                .Returns(6);

            var attack = TestHelpers.CreateAttack(damageCalculator.Object);

            // Act
            attack.Use(TestHelpers.CreateBasicCharacter(), target);

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(2));
        }

        [Test]
        public void Use_ReducesRemainingUses()
        {
            // Arrange
            var attack = TestHelpers.CreateAttack(new Mock<IDamageCalculator>().Object, maxUses: 2);

            // Act
            attack.Use(TestHelpers.CreateBasicCharacter(), TestHelpers.CreateBasicCharacter());

            // Assert
            Assert.That(attack.RemainingUses, Is.EqualTo(1));
        }
    }
}
