using BattleSystem.Moves;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace BattleSystem.Tests.Moves
{
    /// <summary>
    /// Unit tests for <see cref="Buff"/>.
    /// </summary>
    [TestFixture]
    public class BuffTests
    {
        [TestCase(5, true)]
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(-5, false)]
        public void CanUse_ReturnsCorrectly(int remainingUses, bool expectedCanUse)
        {
            // Arrange
            var buff = new Buff("yeti", remainingUses, null, null);
            Constraint constraint = expectedCanUse ? Is.True : Is.False;

            // Act and Assert
            Assert.That(buff.CanUse(), constraint);
        }

        [Test]
        public void Use_ReducesRemainingUses()
        {
            // Arrange
            var buff = new Buff("yeti", 2, null, null);

            // Act
            buff.Use(TestHelpers.CreateBasicCharacter(), TestHelpers.CreateBasicCharacter());

            // Assert
            Assert.That(buff.RemainingUses, Is.EqualTo(1));
        }
    }
}
