using BattleSystem.Moves;
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
        public void Apply_UsesMove()
        {
            // Arrange
            var move = TestHelpers.CreateAttack(maxUses: 5);

            var moveUse = new MoveUse
            {
                Move = move,
                User = TestHelpers.CreateBasicCharacter(),
                Target = TestHelpers.CreateBasicCharacter(),
            };

            // Act
            moveUse.Apply();

            // Assert
            Assert.That(move.RemainingUses, Is.EqualTo(4));
        }
    }
}
