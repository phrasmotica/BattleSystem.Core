using System.Linq;
using BattleSystem.Core.Actions.Targets;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.Targets
{
    /// <summary>
    /// Unit tests for <see cref="TeamActionTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class TeamActionTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsTeam()
        {
            // Arrange
            var calculator = new TeamActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "a"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "b"),
                TestHelpers.CreateBasicCharacter(name: "154", team: "a"),
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
                Assert.That(targets[2].Name, Is.EqualTo("154"));
            });
        }
    }
}
