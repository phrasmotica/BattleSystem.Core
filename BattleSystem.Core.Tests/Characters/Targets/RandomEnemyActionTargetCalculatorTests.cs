using System;
using System.Linq;
using BattleSystem.Core.Characters.Targets;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Characters.Targets
{
    /// <summary>
    /// Unit tests for <see cref="RandomEnemyActionTargetCalculator"/>.
    /// </summary>
    [TestFixture]
    public class RandomEnemyActionTargetCalculatorTests
    {
        [Test]
        public void Ctor_NullRandom_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new RandomEnemyActionTargetCalculator(null));
        }

        [Test]
        public void Calculate_WithEnemies_ReturnsEnemy()
        {
            // Arrange
            var random = new Mock<Random>();
            random
                .Setup(m => m.Next(2))
                .Returns(0);

            var calculator = new RandomEnemyActionTargetCalculator(random.Object);

            var user = TestHelpers.CreateBasicCharacter(team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(team: "a"),
                TestHelpers.CreateBasicCharacter(team: "b"),
                TestHelpers.CreateBasicCharacter(team: "b"),
            };

            // Act
            var (success, targets) = calculator.Calculate(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(targets.Single(), Is.EqualTo(otherCharacters[1]));
            });
        }

        [Test]
        public void Calculate_NoEnemies_ReturnsUnsuccessful()
        {
            // Arrange
            var calculator = new RandomEnemyActionTargetCalculator(Mock.Of<Random>());

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
