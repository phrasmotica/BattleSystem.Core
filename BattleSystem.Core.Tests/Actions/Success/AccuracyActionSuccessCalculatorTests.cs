using System;
using BattleSystem.Core.Actions.Success;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.Success
{
    /// <summary>
    /// Unit tests for <see cref="AccuracyActionSuccessCalculator"/>.
    /// </summary>
    [TestFixture]
    public class AccuracyActionSuccessCalculatorTests
    {
        [Test]
        public void Ctor_NullRandom_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new AccuracyActionSuccessCalculator(0, null));
        }

        [Test]
        public void Calculate_InsideThreshold_ReturnsSuccess()
        {
            // Arrange
            var random = new Mock<Random>();
            random
                .Setup(m => m.Next(100))
                .Returns(85);

            var calculator = new AccuracyActionSuccessCalculator(90, random.Object);

            var action = TestHelpers.CreateDamageAction();

            // Act
            var result = calculator.Calculate(action);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Calculate_OutsideThreshold_ReturnsMiss()
        {
            // Arrange
            var random = new Mock<Random>();
            random
                .Setup(m => m.Next(100))
                .Returns(95);

            var calculator = new AccuracyActionSuccessCalculator(90, random.Object);

            var action = TestHelpers.CreateDamageAction();

            // Act
            var result = calculator.Calculate(action);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
