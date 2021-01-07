using System;
using System.Linq;
using BattleSystem.Core.Characters.Targets;
using BattleSystem.Core.Random;
using Moq;
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
        public void Ctor_NullRandom_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new RandomAllyActionTargetCalculator(null));
        }

        [Test]
        public void Calculate_WithAllies_ReturnsAlly()
        {
            // Arrange
            var random = new Mock<IRandom>();
            random
                .Setup(m => m.Next(2))
                .Returns(0);

            var calculator = new RandomAllyActionTargetCalculator(random.Object);

            var user = TestHelpers.CreateBasicCharacter(team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(team: "a"),
                TestHelpers.CreateBasicCharacter(team: "a"),
                TestHelpers.CreateBasicCharacter(team: "b"),
            };

            // Act
            var (success, targets) = calculator.Calculate(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(targets.Single(), Is.EqualTo(otherCharacters[0]));
            });
        }

        [Test]
        public void Calculate_NoAllies_ReturnsUnsuccessful()
        {
            // Arrange
            var calculator = new RandomAllyActionTargetCalculator(new Mock<IRandom>().Object);

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
