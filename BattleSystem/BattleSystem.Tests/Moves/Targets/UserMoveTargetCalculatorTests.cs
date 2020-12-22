using System.Linq;
using BattleSystem.Moves.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Targets
{
    /// <summary>
    /// Unit tests for <see cref="UserMoveTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class UserMoveTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsUser()
        {
            // Arrange
            var calculator = new UserMoveTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the"),
                TestHelpers.CreateBasicCharacter(name: "15th"),
            };

            // Act
            var targets = calculator.Calculate(user, otherCharacters);

            // Assert
            Assert.That(targets.First().Name, Is.EqualTo("wire"));
        }
    }
}
