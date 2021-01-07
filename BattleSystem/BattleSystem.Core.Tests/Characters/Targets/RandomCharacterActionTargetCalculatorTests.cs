using System.Linq;
using BattleSystem.Core.Characters.Targets;
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
        public void Calculate_ReturnsCharacter()
        {
            // Arrange
            var calculator = new RandomCharacterActionTargetCalculator();

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
                Assert.That(targets.Single().Name, Is.AnyOf("wire", "the", "15th"));
            });
        }
    }
}
