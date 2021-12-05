using System;
using System.Linq;
using BattleSystem.Core.Characters.Targets;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Characters.Targets
{
    /// <summary>
    /// Unit tests for <see cref="RandomCharacterActionTargetCalculator"/>.
    /// </summary>
    [TestFixture]
    public class RandomCharacterActionTargetCalculatorTests
    {
        [Test]
        public void Ctor_NullRandom_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new RandomCharacterActionTargetCalculator(null));
        }

        [Test]
        public void Calculate_ReturnsCharacter()
        {
            // Arrange
            var random = new Mock<Random>();
            random
                .Setup(m => m.Next(3))
                .Returns(1);

            var calculator = new RandomCharacterActionTargetCalculator(random.Object);

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
    }
}
