using BattleSystem.Characters;
using BattleSystem.Healing;
using BattleSystem.Moves.Actions;
using BattleSystem.Moves.Targets;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Actions
{
    /// <summary>
    /// Unit tests for <see cref="Heal"/>.
    /// </summary>
    [TestFixture]
    public class HealTests
    {
        [Test]
        public void Use_HealsTarget()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            var moveTargetCalculator = new Mock<IMoveTargetCalculator>();
            moveTargetCalculator
                .Setup(m => m.Calculate(user, otherCharacters))
                .Returns(otherCharacters);

            var healingCalculator = new Mock<IHealingCalculator>();
            healingCalculator
                .Setup(
                    m => m.Calculate(
                        It.IsAny<Character>(),
                        It.IsAny<Heal>(),
                        It.IsAny<Character>()
                    )
                )
                .Returns(2);

            var heal = TestHelpers.CreateHeal(healingCalculator.Object, moveTargetCalculator.Object);

            otherCharacters[0].ReceiveDamage(2);

            // Act
            heal.Use(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].CurrentHealth, Is.EqualTo(5));
        }
    }
}
