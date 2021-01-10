using BattleSystem.Core.Actions.Success;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Moves.Success
{
    /// <summary>
    /// Unit tests for <see cref="AlwaysActionSuccessCalculator"/>.
    /// </summary>
    [TestFixture]
    public class AlwaysActionSuccessCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsSuccess()
        {
            // Arrange
            var calculator = new AlwaysActionSuccessCalculator();

            var action = TestHelpers.CreateDamageAction();

            // Act
            var result = calculator.Calculate(action);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
