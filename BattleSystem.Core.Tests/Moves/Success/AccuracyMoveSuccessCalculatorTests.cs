using System;
using BattleSystem.Core.Moves;
using Moq;
using NUnit.Framework;
using BattleSystem.Core.Moves.Success;

namespace BattleSystem.Core.Tests.Moves.Success
{
    /// <summary>
    /// Unit tests for <see cref="AccuracyMoveSuccessCalculator"/>.
    /// </summary>
    [TestFixture]
    public class AccuracyMoveSuccessCalculatorTests
    {
        [Test]
        public void Ctor_NullRandom_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new AccuracyMoveSuccessCalculator(0, null));
        }

        [Test]
        public void Calculate_InsideThreshold_ReturnsSuccess()
        {
            // Arrange
            var random = new Mock<Random>();
            random
                .Setup(m => m.Next(100))
                .Returns(85);

            var calculator = new AccuracyMoveSuccessCalculator(90, random.Object);

            var move = TestHelpers.CreateMove();

            // Act
            var result = calculator.Calculate(move);

            // Assert
            Assert.That(result, Is.EqualTo(MoveUseResult.Success));
        }

        [Test]
        public void Calculate_OutsideThreshold_ReturnsMiss()
        {
            // Arrange
            var random = new Mock<Random>();
            random
                .Setup(m => m.Next(100))
                .Returns(95);

            var calculator = new AccuracyMoveSuccessCalculator(90, random.Object);

            var move = TestHelpers.CreateMove();

            // Act
            var result = calculator.Calculate(move);

            // Assert
            Assert.That(result, Is.EqualTo(MoveUseResult.Miss));
        }
    }
}
