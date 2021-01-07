using System.Linq;
using BattleSystem.Battles.TurnBased;
using BattleSystem.Battles.TurnBased.Actions.Damage.Calculators;
using BattleSystem.Core.Characters.Targets;
using BattleSystem.Core.Moves;
using NUnit.Framework;

namespace BattleSystem.Battles.Tests.TurnBased.Actions.Damage.Calculators
{
    /// <summary>
    /// Unit tests for <see cref="BasePowerIncreasesLinearlyWithUsesDamageCalculator"/>.
    /// </summary>
    [TestFixture]
    public class BasePowerIncreasesLinearlyWithUsesDamageCalculatorTests
    {
        [Test]
        public void Calculate_NoPreviousUses_UsesStartingBasePower()
        {
            // Arrange
            var calculator = new BasePowerIncreasesLinearlyWithUsesDamageCalculator(10, 5, new ActionHistory());

            var user = TestHelpers.CreateBasicCharacter(attack: 6);
            var targets = new[]
            {
                TestHelpers.CreateBasicCharacter(defence: 5),
            };

            var damage = TestHelpers.CreateDamageAction(calculator);

            // Act
            var calculations = calculator.Calculate(user, damage, targets);

            // Assert
            Assert.That(calculations.Single().Amount, Is.InRange(8, 10));
        }

        [Test]
        public void Calculate_WithPreviousUses_UsesIncreasedBasePower()
        {
            // Arrange
            var actionHistory = new ActionHistory();
            var calculator = new BasePowerIncreasesLinearlyWithUsesDamageCalculator(10, 5, actionHistory);

            var user = TestHelpers.CreateBasicCharacter(attack: 6);
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(defence: 5),
            };

            var damage = TestHelpers.CreateDamageAction(
                damageCalculator: calculator,
                actionTargetCalculator: new OthersActionTargetCalculator());
            damage.SetTargets(user, otherCharacters);

            var move = TestHelpers.CreateMove(moveActions: damage);

            var moveUse = new MoveUse
            {
                Move = move,
                User = user,
                OtherCharacters = otherCharacters,
            };
            moveUse.Apply();

            actionHistory.AddMoveUse(moveUse);

            // Act
            var calculations = calculator.Calculate(user, damage, otherCharacters);

            // Assert
            Assert.That(calculations.First().Amount, Is.InRange(12, 15));
        }
    }
}
