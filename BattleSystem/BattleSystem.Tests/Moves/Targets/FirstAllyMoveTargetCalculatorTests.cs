using System;
using System.Linq;
using BattleSystem.Moves.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Targets
{
    /// <summary>
    /// Unit tests for <see cref="FirstEnemyMoveTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class FirstEnemyMoveTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsFirstEnemy()
        {
            // Arrange
            var calculator = new FirstEnemyMoveTargetCalculator();

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
            var calculator = new FirstEnemyMoveTargetCalculator();

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
