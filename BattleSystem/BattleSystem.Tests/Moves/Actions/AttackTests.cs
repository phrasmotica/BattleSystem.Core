using BattleSystem.Characters;
using BattleSystem.Damage;
using BattleSystem.Moves.Actions;
using BattleSystem.Moves.Targets;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Actions
{
    /// <summary>
    /// Unit tests for <see cref="Attack"/>.
    /// </summary>
    [TestFixture]
    public class AttackTests
    {
        [Test]
        public void Use_DamagesTarget()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(maxHealth: 8)
            };

            var moveTargetCalculator = new Mock<IMoveTargetCalculator>();
            moveTargetCalculator
                .Setup(m => m.Calculate(user, otherCharacters))
                .Returns(otherCharacters[0]);

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

            var attack = TestHelpers.CreateAttack(damageCalculator.Object, moveTargetCalculator.Object);

            // Act
            attack.Use(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].CurrentHealth, Is.EqualTo(2));
        }
    }
}
