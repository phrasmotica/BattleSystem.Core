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
            var move = TestHelpers.CreateMove();
            var moveSet = TestHelpers.CreateMoveSet(move);

            var user = TestHelpers.CreateBasicCharacter(moveSet: moveSet);
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            // Act
            var moveUse = user.ChooseMove(otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(moveUse.Move, Is.EqualTo(move));
                Assert.That(moveUse.User, Is.EqualTo(user));
                Assert.That(moveUse.OtherCharacters, Is.EqualTo(otherCharacters));
            });
        }
    }
}
