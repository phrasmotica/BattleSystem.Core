using System.Linq;
using BattleSystem.Core.Actions.Targets;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.Targets
{
    /// <summary>
    /// Unit tests for <see cref="RandomEnemyActionTargetCalculator"/>.
    /// </summary>
    [TestFixture]
    public class RandomEnemyActionTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsEnemy()
        {
            // Arrange
            var calculator = new RandomEnemyActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "a"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "b"),
                TestHelpers.CreateBasicCharacter(name: "154", team: "b"),
            };

            // Act
            var (success, targets) = calculator.Calculate(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(targets.Single().Name, Is.AnyOf("15th", "154"));
            });
        }

        [Test]
        public void Calculate_NoEnemies_ReturnsUnsuccessful()
        {
            // Arrange
            var calculator = new RandomEnemyActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "a"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "a"),
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
