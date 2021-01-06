using System.Linq;
using BattleSystem.Core.Actions.Targets;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.Targets
{
    /// <summary>
    /// Unit tests for <see cref="AllActionTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class AllActionTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsAll()
        {
            // Arrange
            var calculator = new AllActionTargetCalculator();

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
                Assert.That(targets.Length, Is.EqualTo(3));
                Assert.That(targets[0].Name, Is.EqualTo("wire"));
                Assert.That(targets[1].Name, Is.EqualTo("the"));
                Assert.That(targets[2].Name, Is.EqualTo("15th"));
            });
        }
    }
}
