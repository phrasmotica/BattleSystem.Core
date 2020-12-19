using BattleSystem.Healing;
using BattleSystem.Moves;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace BattleSystem.Tests.Moves
{
    /// <summary>
    /// Unit tests for <see cref="Heal"/>.
    /// </summary>
    [TestFixture]
    public class HealTests
    {
        [TestCase(5, true)]
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(-5, false)]
        public void CanUse_ReturnsCorrectly(int remainingUses, bool expectedCanUse)
        {
            // Arrange
            var heal = new Heal(new Mock<IHealingCalculator>().Object, "yeti", remainingUses, 1);
            Constraint constraint = expectedCanUse ? Is.True : Is.False;

            // Act and Assert
            Assert.That(heal.CanUse(), constraint);
        }

        [Test]
        public void Use_ReducesRemainingUses()
        {
            // Arrange
            var heal = new Heal(new Mock<IHealingCalculator>().Object, "yeti", 2, 1);

            // Act
            heal.Use(TestHelpers.CreateBasicCharacter(), TestHelpers.CreateBasicCharacter());

            // Assert
            Assert.That(heal.RemainingUses, Is.EqualTo(1));
        }
    }
}
