using BattleSystem.Characters;
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
            var user = TestHelpers.CreateBasicCharacter();
            var target = TestHelpers.CreateBasicCharacter();

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
