using System.Linq;
using BattleSystem.Moves.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Targets
{
    /// <summary>
    /// Unit tests for <see cref="TeamMoveTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class TeamMoveTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsTeam()
        {
            // Arrange
            var calculator = new TeamMoveTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "a"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "b"),
                TestHelpers.CreateBasicCharacter(name: "154", team: "a"),
            };

            // Act
            var targets = calculator.Calculate(user, otherCharacters).ToArray();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(targets.Length, Is.EqualTo(3));
                Assert.That(targets[0].Name, Is.EqualTo("wire"));
                Assert.That(targets[1].Name, Is.EqualTo("the"));
                Assert.That(targets[2].Name, Is.EqualTo("154"));
            });
        }
    }
}
