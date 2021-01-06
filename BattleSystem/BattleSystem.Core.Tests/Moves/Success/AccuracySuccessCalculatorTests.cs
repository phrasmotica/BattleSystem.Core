using BattleSystem.Core.Moves.Success;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Moves.Success
{
    /// <summary>
    /// Unit tests for <see cref="AccuracySuccessCalculator"/>.
    /// </summary>
    [TestFixture]
    public class AccuracySuccessCalculatorTests
    {
        [Test]
        public void Calculate_MaxAccuracy_ReturnsSuccess()
        {
            // Arrange
            var calculator = new AccuracySuccessCalculator(100);

            var user = TestHelpers.CreateBasicCharacter();
            var move = TestHelpers.CreateMove();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            // Act
            var result = calculator.Calculate(user, move, otherCharacters);

            // Assert
            Assert.That(result, Is.EqualTo(MoveUseResult.Success));
        }

        [Test]
        public void Calculate_MinAccuracy_ReturnsMiss()
        {
            // Arrange
            var calculator = new AccuracySuccessCalculator(0);

            var user = TestHelpers.CreateBasicCharacter();
            var move = TestHelpers.CreateMove();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            // Act
            var result = calculator.Calculate(user, move, otherCharacters);

            // Assert
            Assert.That(result, Is.EqualTo(MoveUseResult.Miss));
        }
    }
}
