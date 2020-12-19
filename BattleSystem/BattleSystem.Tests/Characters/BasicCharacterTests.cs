using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Moves;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Tests.Characters
{
    /// <summary>
    /// Unit tests for <see cref="BasicCharacter"/>.
    /// </summary>
    [TestFixture]
    public class BasicCharacterTests
    {
        [Test]
        public void ChooseMove_ReturnsMoveUse()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter();

            var move = new Mock<IMove>();
            move.Setup(
                m => m.CalculateTarget(
                    It.IsAny<Character>(),
                    It.IsAny<IEnumerable<Character>>()
                )
            )
            .Returns(target);

            var moveSet = TestHelpers.CreateMoveSet(move.Object);

            var user = TestHelpers.CreateBasicCharacter(moveSet: moveSet);

            // Act
            var moveUse = user.ChooseMove(new[] { target });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(moveUse.Move, Is.EqualTo(user.Moves.Move1));
                Assert.That(moveUse.User, Is.EqualTo(user));
                Assert.That(moveUse.Target, Is.EqualTo(target));
            });
        }
    }
}
