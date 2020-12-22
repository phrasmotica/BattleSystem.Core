using System.Collections.Generic;
using BattleSystem.Moves.Targets;
using BattleSystem.Stats;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Actions
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

            var moveTargetCalculator = new Mock<IMoveTargetCalculator>();
            moveTargetCalculator
                .Setup(m => m.Calculate(user, otherCharacters))
                .Returns(otherCharacters);

            var buff = TestHelpers.CreateBuff(
                moveTargetCalculator.Object,
                new Dictionary<StatCategory, double>
                {
                    [StatCategory.Attack] = 0.2
                });

            // Act
            buff.Use(user, otherCharacters);

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

            var buff = TestHelpers.CreateBuff(moveTargetCalculator: new OthersMoveTargetCalculator());

            // Act
            var appliedActions = buff.Use(user, otherCharacters);

            // Assert
            Assert.That(appliedActions, Is.True);
        }

        [Test]
        public void Use_WithDeadTargets_AppliesNoActions()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();

            var target = TestHelpers.CreateBasicCharacter(maxHealth: 1);
            var otherCharacters = new[]
            {
                target
            };

            target.ReceiveDamage(1);

            var buff = TestHelpers.CreateBuff();

            // Act
            var appliedActions = buff.Use(user, otherCharacters);

            // Assert
            Assert.That(appliedActions, Is.False);
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
            var appliedActions = buff.Use(user, otherCharacters);

            // Assert
            Assert.That(appliedActions, Is.False);
        }
    }
}
