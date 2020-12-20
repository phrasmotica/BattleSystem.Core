using System;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Moves.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Targets
{
    /// <summary>
    /// Unit tests for <see cref="FirstMoveTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class FirstMoveTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsFirst()
        {
            // Arrange
            var calculator = new FirstMoveTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire");
            var characters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the"),
                TestHelpers.CreateBasicCharacter(name: "15th"),
            };

            // Act
            var targets = calculator.Calculate(user, characters);

            // Assert
            Assert.That(targets.First().Name, Is.EqualTo("the"));
        }

        [Test]
        public void Calculate_NoCharacters_Throws()
        {
            // Arrange
            var calculator = new FirstMoveTargetCalculator();

            // Act and Assert
            Assert.Throws<ArgumentException>(
                () => _ = calculator.Calculate(
                    TestHelpers.CreateBasicCharacter(),
                    Enumerable.Empty<Character>()
                )
            );
        }
    }
}
