using System.Linq;
using BattleSystem.Core.Characters.Targets;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Characters.Targets
{
    /// <summary>
    /// Unit tests for <see cref="FirstEnemyActionTargetCalculator"/>.
    /// </summary>
    [TestFixture]
    public class FirstEnemyActionTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsFirstEnemy()
        {
            // Arrange
            var calculator = new FirstEnemyActionTargetCalculator();

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
                Assert.That(targets.First().Name, Is.EqualTo("15th"));
            });
        }

        [Test]
        public void Calculate_NoEnemies_ReturnsUnsuccessful()
        {
            // Arrange
            var calculator = new FirstEnemyActionTargetCalculator();

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
