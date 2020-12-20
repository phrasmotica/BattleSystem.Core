using BattleSystem.Moves;
using BattleSystem.Moves.Actions;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves
{
    /// <summary>
    /// Unit tests for <see cref="Move"/>.
    /// </summary>
    [TestFixture]
    public class MoveTests
    {
        [Test]
        public void SetMaxUses_SetsMaxAndRemainingUses()
        {
            // Arrange
            var move = new Move();

            // Act
            move.SetMaxUses(10);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(move.MaxUses, Is.EqualTo(10));
                Assert.That(move.RemainingUses, Is.EqualTo(10));
            });
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetMaxUses_HigherLimit_SetsMaxAndRemainingUses(bool ignoreRemainingUses)
        {
            // Arrange
            var move = new Move();
            move.SetMaxUses(10);

            // Act
            move.SetMaxUses(15, ignoreRemainingUses);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(move.MaxUses, Is.EqualTo(15));
                Assert.That(move.RemainingUses, Is.EqualTo(ignoreRemainingUses ? 10 : 15));
            });
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetMaxUses_LowerLimit_SetsMaxAndRemainingUses(bool ignoreRemainingUses)
        {
            // Arrange
            var move = new Move();
            move.SetMaxUses(10);

            // Act
            move.SetMaxUses(5, ignoreRemainingUses);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(move.MaxUses, Is.EqualTo(5));
                Assert.That(move.RemainingUses, Is.EqualTo(5));
            });
        }

        [TestCase(5, true)]
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(-1, false)]
        public void CanUse_ReturnsCorrectly(int remainingUses, bool expectedCanUse)
        {
            // Arrange
            var move = TestHelpers.CreateMove(maxUses: remainingUses);

            // Act and Assert
            Assert.That(move.CanUse(), Is.EqualTo(expectedCanUse));
        }

        [Test]
        public void Use_ReducesRemainingUses()
        {
            // Arrange
            var move = TestHelpers.CreateMove(
                maxUses: 5,
                moveActions: new Mock<IMoveAction>().Object);

            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            // Act
            _ = move.Use(user, otherCharacters);

            // Assert
            Assert.That(move.RemainingUses, Is.EqualTo(4));
        }
    }
}
