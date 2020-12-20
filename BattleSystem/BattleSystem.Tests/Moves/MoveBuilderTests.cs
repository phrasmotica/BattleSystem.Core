using BattleSystem.Moves;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves
{
    /// <summary>
    /// Unit tests for <see cref="MoveBuilder"/>.
    /// </summary>
    [TestFixture]
    public class MoveBuilderTests
    {
        [Test]
        public void Name_SetsMoveName()
        {
            // Arrange
            var moveBuilder = new MoveBuilder();
            
            // Act
            var move = moveBuilder.Name("boost").Build();

            // Assert
            Assert.That(move.Name, Is.EqualTo("boost"));
        }

        [Test]
        public void Describe_SetsMoveDescription()
        {
            // Arrange
            var moveBuilder = new MoveBuilder();

            // Act
            var move = moveBuilder.Describe("boost").Build();

            // Assert
            Assert.That(move.Description, Is.EqualTo("boost"));
        }

        [TestCase]
        public void WithMaxUses_SetsMaxAndRemainingUses()
        {
            // Arrange
            var moveBuilder = new MoveBuilder();

            // Act
            var move = moveBuilder.WithMaxUses(10).Build();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(move.MaxUses, Is.EqualTo(10));
                Assert.That(move.RemainingUses, Is.EqualTo(10));
            });
        }
    }
}
