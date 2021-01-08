using System;
using System.Linq;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Characters.Targets;
using BattleSystem.Core.Random;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Characters.Targets
{
    /// <summary>
    /// Unit tests for <see cref="RandomOtherActionTargetCalculator"/>.
    /// </summary>
    [TestFixture]
    public class RandomOtherActionTargetCalculatorTests
    {
        [Test]
        public void Ctor_NullRandom_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new RandomOtherActionTargetCalculator(null));
        }

        [Test]
        public void Calculate_WithOthers_ReturnsOtherCharacter()
        {
            // Arrange
            var random = new Mock<IRandom>();
            random
                .Setup(m => m.Next(2))
                .Returns(0);

            var calculator = new RandomOtherActionTargetCalculator(random.Object);

            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(),
                TestHelpers.CreateBasicCharacter(),
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
        public void Calculate_NoOthers_ReturnsUnsuccessful()
        {
            // Arrange
            var calculator = new RandomOtherActionTargetCalculator(new Mock<IRandom>().Object);

            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = Enumerable.Empty<Character>();

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
