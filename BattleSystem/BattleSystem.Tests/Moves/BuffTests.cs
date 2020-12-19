using System.Collections.Generic;
using BattleSystem.Moves;
using BattleSystem.Moves.Targets;
using BattleSystem.Stats;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves
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
                .Returns(otherCharacters[0]);

            var buff = TestHelpers.CreateBuff(
                moveTargetCalculator.Object,
                targetMultipliers: new Dictionary<StatCategory, double>
                {
                    [StatCategory.Attack] = 0.2
                });

            // Act
            buff.Use(user, otherCharacters);

            // Assert
            Assert.That(otherCharacters[0].Stats.Attack.CurrentValue, Is.EqualTo(12));
        }
    }
}
