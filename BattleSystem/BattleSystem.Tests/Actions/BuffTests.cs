using System.Collections.Generic;
using BattleSystem.Actions.Targets;
using BattleSystem.Stats;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions
{
    /// <summary>
    /// Unit tests for <see cref="Buff"/>.
    /// </summary>
    [TestFixture]
    public class BuffTests
    {
        [Test]
        public void Use_BuffsTargetStat()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(attack: 10)
            };

            var buff = TestHelpers.CreateBuff(
                new OthersActionTargetCalculator(),
                new Dictionary<StatCategory, double>
                {
                    [StatCategory.Attack] = 0.2
                });

            // Act
            _ = buff.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].Stats.Attack.CurrentValue, Is.EqualTo(12));
        }

        [Test]
        public void Use_WithTargets_AppliesActions()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            var buff = TestHelpers.CreateBuff(new OthersActionTargetCalculator());

            // Act
            var actionResults = buff.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(actionResults, Is.Not.Empty);
        }

        [Test]
        public void Use_WithDeadTargets_AppliesNoActions()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(maxHealth: 0)
            };

            var buff = TestHelpers.CreateBuff(new OthersActionTargetCalculator());

            // Act
            var actionResults = buff.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(actionResults, Is.Empty);
        }

        [Test]
        public void Use_WithoutTargets_AppliesNoActions()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            var buff = TestHelpers.CreateBuff();

            // Act
            var actionResults = buff.Use<string>(user, otherCharacters);

            // Assert
            Assert.That(actionResults, Is.Empty);
        }
    }
}
