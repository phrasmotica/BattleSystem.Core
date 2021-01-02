using System;
using System.Linq;
using BattleSystem.Actions.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Targets
{
    /// <summary>
    /// Unit tests for <see cref="FirstEnemyActionTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class FirstEnemyactionTargetCalculatorTests
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
            var targets = calculator.Calculate(user, otherCharacters);

            // Assert
            Assert.That(targets.First().Name, Is.EqualTo("15th"));
        }

        [Test]
        public void Calculate_NoEnemies_Throws()
        {
            // Arrange
            var calculator = new FirstEnemyActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "a"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "a"),
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(
                () => _ = calculator.Calculate(
                    user,
                    otherCharacters
                )
            );
        }
    }
}
