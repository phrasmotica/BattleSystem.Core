using System;
using System.Linq;
using BattleSystem.Actions.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Targets
{
    /// <summary>
    /// Unit tests for <see cref="FirstAllyActionTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class FirstAllyactionTargetCalculatorTests
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
            var targets = calculator.Calculate(user, otherCharacters);

            // Assert
            Assert.That(targets.First().Name, Is.EqualTo("15th"));
        }

        [Test]
        public void Calculate_NoAllies_Throws()
        {
            // Arrange
            var calculator = new FirstAllyActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "b"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "b"),
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
