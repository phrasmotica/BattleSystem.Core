using System.Linq;
using BattleSystem.Actions.Targets;
using BattleSystem.Moves;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Targets
{
    /// <summary>
    /// Unit tests for <see cref="RetaliateActionTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class RetaliateActionTargetCalculatorTests
    {
        [Test]
        public void Calculate_UserJustTookDamage_ReturnsUserOfActionThatDamagedTheUser()
        {
            // Arrange
            var calculator = new RetaliateActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the"),
                TestHelpers.CreateBasicCharacter(name: "15th"),
            };

            user.ReceiveDamage<Move>(10, otherCharacters[0]);

            // Act
            var (success, targets) = calculator.Calculate(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(targets.Single().Name, Is.EqualTo("the"));
            });
        }

        [Test]
        public void Calculate_UserNotDamaged_ReturnsNoTarget()
        {
            // Arrange
            var calculator = new RetaliateActionTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the"),
                TestHelpers.CreateBasicCharacter(name: "15th"),
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
