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
        public void ReceiveAttack_TakesDamage()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5, defence: 1);

            // Act
            target.ReceiveAttack(TestHelpers.CreateAttack(), TestHelpers.CreateStat());

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(4));
        }

        [Test]
        public void ReceiveAttack_IsDeadIfNoHealthLeft()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5, defence: 1);

            // Act
            target.ReceiveAttack(TestHelpers.CreateAttack(power: 6), TestHelpers.CreateStat(2));

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

        [TestCase(2, HealingMode.Absolute, 5)]
        [TestCase(10, HealingMode.Absolute, 5)]
        [TestCase(20, HealingMode.Percentage, 4)]
        public void ReceiveHeal_AddsHealth(int amount, HealingMode healingMode, int expectedHealth)
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5, defence: 1);
            target.ReceiveAttack(TestHelpers.CreateAttack(power: 2), TestHelpers.CreateStat(2)); // now has 3 health

            // Act
            target.ReceiveHeal(TestHelpers.CreateHeal(amount: amount, mode: healingMode));

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(expectedHealth));
        }
    }
}
