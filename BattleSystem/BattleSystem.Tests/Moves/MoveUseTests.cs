using BattleSystem.Moves;
using BattleSystem.Moves.Success;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves
{
    /// <summary>
    /// Unit tests for <see cref="MoveUse"/>.
    /// </summary>
    [TestFixture]
    public class MoveUseTests
    {
        [Test]
        public void Apply_ReturnsResult()
        {
            // Arrange
            var move = TestHelpers.CreateMove();

            var moveUse = new MoveUse
            {
                Move = move,
                User = TestHelpers.CreateBasicCharacter(),
                OtherCharacters = new[]
                {
                    TestHelpers.CreateBasicCharacter()
                }
            };

            // Act
            moveUse.Apply();

            // Assert
            Assert.That(moveUse.Result, Is.EqualTo(MoveUseResult.Success));
        }
    }
}
