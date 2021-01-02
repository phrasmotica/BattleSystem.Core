using System.Linq;
using BattleSystem.Actions.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Targets
{
    /// <summary>
    /// Unit tests for <see cref="OthersActionTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class OthersactionTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsOthers()
        {
            // Arrange
            var calculator = new OthersActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the"),
                TestHelpers.CreateBasicCharacter(name: "15th"),
            };

            // Act
            var result = calculator.Calculate(user, otherCharacters);
            var targets = result.targets.ToArray();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.success, Is.True);
                Assert.That(targets.Length, Is.EqualTo(2));
                Assert.That(targets[0].Name, Is.EqualTo("the"));
                Assert.That(targets[1].Name, Is.EqualTo("15th"));
            });
        }
    }
}
