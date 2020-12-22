using System;
using System.Linq;
using BattleSystem.Moves.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Targets
{
    /// <summary>
    /// Unit tests for <see cref="EnemiesMoveTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class EnemiesMoveTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsEnemies()
        {
            // Arrange
            var calculator = new EnemiesMoveTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "b"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "a"),
                TestHelpers.CreateBasicCharacter(name: "154", team: "b"),
            };

            // Act
            var targets = calculator.Calculate(user, otherCharacters).ToArray();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(targets.Length, Is.EqualTo(2));
                Assert.That(targets[0].Name, Is.EqualTo("the"));
                Assert.That(targets[1].Name, Is.EqualTo("154"));
            });
        }

        [Test]
        public void Calculate_NoEnemies_Throws()
        {
            // Arrange
            var calculator = new EnemiesMoveTargetCalculator();

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
