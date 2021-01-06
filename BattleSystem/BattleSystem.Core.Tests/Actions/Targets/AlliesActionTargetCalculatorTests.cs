using System.Linq;
using BattleSystem.Core.Actions.Targets;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.Targets
{
    /// <summary>
    /// Unit tests for <see cref="AlliesActionTargetCalculator"/>.
    /// </summary>
    [TestFixture]
    public class AlliesActionTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsAllies()
        {
            // Arrange
            var calculator = new AlliesActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "a"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "b"),
                TestHelpers.CreateBasicCharacter(name: "154", team: "a"),
            };

            // Act
            var result = calculator.Calculate(user, otherCharacters);
            var targets = result.targets.ToArray();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.success, Is.True);
                Assert.That(targets.Length, Is.EqualTo(2));
                Assert.That(targets[0].Name, Is.EqualTo("the"));
                Assert.That(targets[1].Name, Is.EqualTo("154"));
            });
        }

        [Test]
        public void Calculate_NoAllies_ReturnsUnsuccessful()
        {
            // Arrange
            var calculator = new AlliesActionTargetCalculator();

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
