using System;
using BattleSystem.Damage;
using BattleSystem.Healing;
using BattleSystem.Moves;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace BattleSystem.Tests.Moves
{
    /// <summary>
    /// Unit tests for <see cref="MoveSet"/>.
    /// </summary>
    [TestFixture]
    public class MoveSetTests
    {
        [Test]
        public void ChooseRandom_DoesNotReturnNull()
        {
            // Arrange
            var moveSet = new MoveSet
            {
                Move1 = TestHelpers.CreateAttack(new Mock<IDamageCalculator>().Object),
                Move3 = TestHelpers.CreateBuff(),
            };

            // Act
            var move = moveSet.ChooseRandom();

            // Assert
            Assert.That(move, Is.Not.Null);
        }

        [Test]
        public void GetChoices_ContainsCorrectMoveNames()
        {
            // Arrange
            var moveSet = new MoveSet
            {
                Move1 = TestHelpers.CreateAttack(new Mock<IDamageCalculator>().Object, "move1"),
                Move3 = TestHelpers.CreateBuff("move3"),
            };

            // Act
            var choices = moveSet.GetChoices();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(choices, Contains.Substring("move1"));
                Assert.That(choices, Contains.Substring("move3"));
            });
        }

        [Test]
        public void GetIndexes_ReturnsCorrectIndexes()
        {
            // Arrange
            var moveSet = new MoveSet
            {
                Move1 = TestHelpers.CreateAttack(new Mock<IDamageCalculator>().Object, "move1"),
                Move2 = TestHelpers.CreateHeal(new Mock<IHealingCalculator>().Object, "move2"),
                Move4 = TestHelpers.CreateAttack(new Mock<IDamageCalculator>().Object, "move4"),
            };

            // Act
            var indexes = moveSet.GetIndexes();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(indexes, Contains.Item(1));
                Assert.That(indexes, Contains.Item(2));
                Assert.That(indexes, new NotConstraint(Contains.Item(3)));
                Assert.That(indexes, Contains.Item(4));
            });
        }

        [TestCase(0, "move1")]
        [TestCase(1, "move2")]
        [TestCase(2, "move3")]
        [TestCase(3, "move4")]
        public void GetMove_ValidIndex_ReturnsCorrectMove(int index, string expectedName)
        {
            // Arrange
            var moveSet = new MoveSet
            {
                Move1 = TestHelpers.CreateAttack(new Mock<IDamageCalculator>().Object, "move1"),
                Move2 = TestHelpers.CreateHeal(new Mock<IHealingCalculator>().Object, "move2"),
                Move3 = TestHelpers.CreateBuff("move3"),
                Move4 = TestHelpers.CreateAttack(new Mock<IDamageCalculator>().Object, "move4"),
            };

            // Act
            var move = moveSet.GetMove(index);

            // Assert
            Assert.That(move.Name, Is.EqualTo(expectedName));
        }

        [TestCase(-1)]
        [TestCase(5)]
        public void GetMove_InvalidIndex_Throws(int index)
        {
            // Arrange, Act and Assert
            Assert.Throws<ArgumentException>(() => _ = new MoveSet().GetMove(index));
        }
    }
}
