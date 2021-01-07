using System.Linq;
using BattleSystem.Core.Characters.Targets;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Characters.Targets
{
    /// <summary>
    /// Unit tests for <see cref="RandomAllyActionTargetCalculator"/>.
    /// </summary>
    [TestFixture]
    public class RandomAllyActionTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsAlly()
        {
            // Arrange
            var calculator = new RandomAllyActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "a"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "a"),
                TestHelpers.CreateBasicCharacter(name: "154", team: "b"),
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
        public void Calculate_NoAllies_ReturnsUnsuccessful()
        {
            // Arrange
            var calculator = new RandomAllyActionTargetCalculator();

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
