using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Moves;
using BattleSystem.Stats;
using NUnit.Framework;

namespace BattleSystem.Tests.Characters
{
    /// <summary>
    /// Unit tests for <see cref="Character"/>.
    /// </summary>
    [TestFixture]
    public class CharacterTests
    {
        [Test]
        public void ReceiveDamage_TakesDamage()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5);

            // Act
            target.ReceiveDamage(2);

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(3));
        }

        [Test]
        public void ReceiveDamage_IsDeadIfNoHealthLeft()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5);

            // Act
            target.ReceiveDamage(6);

            // Assert
            Assert.That(target.IsDead, Is.True);
        }

        [Test]
        public void ReceiveBuff_ChangesStatMultipliers()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter(attack: 10, defence: 10, speed: 10);

            // Act
            target.ReceiveBuff(new Dictionary<StatCategory, double>
            {
                [StatCategory.Attack] = 0.2,
                [StatCategory.Defence] = -0.3,
                [StatCategory.Speed] = -0.1,
            });

            // Assert
            Assert.That(target.Stats.Attack.CurrentValue, Is.EqualTo(12));
            Assert.That(target.Stats.Defence.CurrentValue, Is.EqualTo(7));
            Assert.That(target.CurrentSpeed, Is.EqualTo(9));
        }

        [Test]
        public void ReceiveHeal_AddsHealth()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5);
            target.ReceiveDamage(2);

            // Act
            target.Heal(2);

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(5));
        }
    }
}
