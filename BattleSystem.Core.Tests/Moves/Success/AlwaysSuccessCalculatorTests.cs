using BattleSystem.Core.Moves;
using BattleSystem.Core.Moves.Success;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Moves.Success
{
    /// <summary>
    /// Unit tests for <see cref="AlwaysMoveSuccessCalculator"/>.
    /// </summary>
    [TestFixture]
    public class AlwaysMoveSuccessCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsSuccess()
        {
            // Arrange
            var calculator = new AlwaysMoveSuccessCalculator();

            var move = TestHelpers.CreateMove();

            // Act
            var result = calculator.Calculate(move);

            // Assert
            Assert.That(result, Is.EqualTo(MoveUseResult.Success));
        }
    }
}
