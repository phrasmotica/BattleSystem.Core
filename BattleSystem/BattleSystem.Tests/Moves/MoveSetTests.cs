using System;
using System.Linq;
using BattleSystem.Moves;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves
{
    /// <summary>
    /// Unit tests for <see cref="MoveSet"/>.
    /// </summary>
    [TestFixture]
    public class MoveSetTests
    {
        [Test]
        public void AddMove_AddsMove()
        {
            // Arrange
            var moveSet = TestHelpers.CreateMoveSet();

            // Act
            moveSet.AddMove(TestHelpers.CreateMove());

            // Assert
            Assert.That(moveSet.Moves.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddMove_NullMove_Throws()
        {
            // Arrange
            var moveSet = TestHelpers.CreateMoveSet();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => moveSet.AddMove(null));
        }

        [Test]
        public void ChooseRandom_DoesNotReturnNull()
        {
            // Arrange
            var moveSet = TestHelpers.CreateMoveSet(
                TestHelpers.CreateMove(),
                TestHelpers.CreateMove()
            );

            // Act
            var move = moveSet.ChooseRandom();

            // Assert
            Assert.That(move, Is.Not.Null);
        }

        [Test]
        public void Summarise_ContainsCorrectSummaries()
        {
            // Arrange
            var moveSet = TestHelpers.CreateMoveSet(
                TestHelpers.CreateMove(name: "move1"),
                TestHelpers.CreateMove(name: "move3")
            );

            // Act
            var choices = moveSet.Summarise();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(choices, Contains.Substring("move1"));
                Assert.That(choices, Contains.Substring("move3"));
            });
        }

        [Test]
        public void Summarise_WithIndexes_ContainsCorrectSummaries()
        {
            // Arrange
            var moveSet = TestHelpers.CreateMoveSet(
                TestHelpers.CreateMove(name: "move1"),
                TestHelpers.CreateMove(name: "move3")
            );

            // Act
            var choices = moveSet.Summarise(true);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(choices, Contains.Substring("1: move1"));
                Assert.That(choices, Contains.Substring("2: move3"));
            });
        }

        [Test]
        public void GetIndexes_ReturnsCorrectIndexes()
        {
            // Arrange
            var moveSet = TestHelpers.CreateMoveSet(
                TestHelpers.CreateMove(name: "move1"),
                TestHelpers.CreateMove(name: "move2"),
                TestHelpers.CreateMove(name: "move4")
            );

            // Act
            var indexes = moveSet.GetIndexes().ToList();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(indexes.Count, Is.EqualTo(3));
                Assert.That(indexes, Contains.Item(1));
                Assert.That(indexes, Contains.Item(2));
                Assert.That(indexes, Contains.Item(3));
            });
        }

        [TestCase(0, "move1")]
        [TestCase(1, "move2")]
        [TestCase(2, "move3")]
        [TestCase(3, "move4")]
        public void GetMove_ValidIndex_ReturnsCorrectMove(int index, string expectedName)
        {
            // Arrange
            var moveSet = TestHelpers.CreateMoveSet(
                TestHelpers.CreateMove(name: "move1"),
                TestHelpers.CreateMove(name: "move2"),
                TestHelpers.CreateMove(name: "move3"),
                TestHelpers.CreateMove(name: "move4")
            );

            // Act
            var move = moveSet.GetMove(index);

            // Assert
            Assert.That(move.Name, Is.EqualTo(expectedName));
        }

        [TestCase(-1)]
        [TestCase(5)]
        public void GetMove_InvalidIndex_Throws(int index)
        {
            // Arrange
            var moveSet = TestHelpers.CreateMoveSet(
                TestHelpers.CreateMove()
            );

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _ = moveSet.GetMove(index));
        }
    }
}
