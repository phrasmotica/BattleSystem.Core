using System;
using BattleSystem.Core.Moves.Success;
using BattleSystem.Core.Random;
using Moq;
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
        public void Ctor_NullRandom_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new AccuracySuccessCalculator(0, null));
        }

        [Test]
        public void Calculate_InsideThreshold_ReturnsSuccess()
        {
            // Arrange
            var random = new Mock<IRandom>();
            random
                .Setup(m => m.Next(100))
                .Returns(85);

            var calculator = new AccuracySuccessCalculator(90, random.Object);

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
        public void Calculate_OutsideThreshold_ReturnsMiss()
        {
            // Arrange
            var random = new Mock<IRandom>();
            random
                .Setup(m => m.Next(100))
                .Returns(95);

            var calculator = new AccuracySuccessCalculator(90, random.Object);

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
