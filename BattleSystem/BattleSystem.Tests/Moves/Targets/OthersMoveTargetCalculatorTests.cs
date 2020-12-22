using System.Linq;
using BattleSystem.Moves.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Targets
{
    /// <summary>
    /// Unit tests for <see cref="OthersMoveTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class OthersMoveTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsOthers()
        {
            // Arrange
            var calculator = new OthersMoveTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire");
            var characters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the"),
                TestHelpers.CreateBasicCharacter(name: "15th"),
            };

            // Act
            var targets = calculator.Calculate(user, characters).ToArray();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(targets.Length, Is.EqualTo(2));
                Assert.That(targets[0].Name, Is.EqualTo("the"));
                Assert.That(targets[1].Name, Is.EqualTo("15th"));
            });
        }
    }
}
