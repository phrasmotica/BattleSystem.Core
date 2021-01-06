using System.Linq;
using BattleSystem.Core.Actions.Targets;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.Targets
{
    /// <summary>
    /// Unit tests for <see cref="FirstAllyActionTargetCalculator"/>.
    /// </summary>
    [TestFixture]
    public class FirstAllyActionTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsFirstAlly()
        {
            // Arrange
            var calculator = new FirstAllyActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "b"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "a"),
            };

            // Act
            var (success, targets) = calculator.Calculate(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(targets.First().Name, Is.EqualTo("15th"));
            });
        }

        [Test]
        public void Calculate_NoAllies_ReturnsUnsuccessful()
        {
            // Arrange
            var calculator = new FirstAllyActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "b"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "b"),
            };

            // Act
            var (success, targets) = calculator.Calculate(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.False);
                Assert.That(targets, Is.Empty);
            });
        }
    }
}
