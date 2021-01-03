using System.Linq;
using BattleSystem.Actions.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Targets
{
    /// <summary>
    /// Unit tests for <see cref="UserActionTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class UserActionTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsUser()
        {
            // Arrange
            var calculator = new UserActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the"),
                TestHelpers.CreateBasicCharacter(name: "15th"),
            };

            // Act
            var (success, targets) = calculator.Calculate(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(targets.First().Name, Is.EqualTo("wire"));
            });
        }
    }
}
