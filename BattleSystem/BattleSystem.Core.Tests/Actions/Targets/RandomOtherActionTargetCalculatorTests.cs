using System.Linq;
using BattleSystem.Core.Actions.Targets;
using BattleSystem.Core.Characters;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.Targets
{
    /// <summary>
    /// Unit tests for <see cref="RandomOtherActionTargetCalculator"/>.
    /// </summary>
    [TestFixture]
    public class RandomOtherActionTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsOtherCharacter()
        {
            // Arrange
            var calculator = new RandomOtherActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "a"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "b"),
            };

            // Act
            var (success, targets) = calculator.Calculate(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(targets.Single().Name, Is.AnyOf("the", "15th"));
            });
        }

        [Test]
        public void Calculate_NoOtherCharacters_ReturnsUnsuccessful()
        {
            // Arrange
            var calculator = new RandomOtherActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = Enumerable.Empty<Character>();

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
