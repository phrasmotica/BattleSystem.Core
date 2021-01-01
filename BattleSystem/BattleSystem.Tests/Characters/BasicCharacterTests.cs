using System;
using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Stats;
using NUnit.Framework;
using static BattleSystem.Items.Item;

namespace BattleSystem.Tests.Characters
{
    /// <summary>
    /// Unit tests for <see cref="BasicCharacter"/>.
    /// </summary>
    [TestFixture]
    public class BasicCharacterTests
    {
        [Test]
        public void ChooseMove_ReturnsMoveUse()
        {
            // Arrange
            var move = TestHelpers.CreateMove();
            var moveSet = TestHelpers.CreateMoveSet(move);

            var user = TestHelpers.CreateBasicCharacter(moveSet: moveSet);
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            // Act
            var moveUse = user.ChooseMove(otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(moveUse.Move, Is.EqualTo(move));
                Assert.That(moveUse.User, Is.EqualTo(user));
                Assert.That(moveUse.OtherCharacters, Is.EqualTo(otherCharacters));
            });
        }

        [Test]
        public void Stats_Get_NoItem_ReturnsStats()
        {
            // Arrange
            var character = TestHelpers.CreateBasicCharacter(attack: 1, defence: 2, speed: 3);

            // Act
            var stats = character.Stats;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(stats.Attack.BaseValue, Is.EqualTo(1));
                Assert.That(stats.Defence.BaseValue, Is.EqualTo(2));
                Assert.That(stats.Speed.BaseValue, Is.EqualTo(3));
            });
        }

        [Test]
        public void Stats_Get_WithItem_ReturnsTransformedStats()
        {
            // Arrange
            var character = TestHelpers.CreateBasicCharacter(attack: 10, defence: 20, speed: 30);

            var item = TestHelpers.CreateItem(
                attackBaseValueTransforms: new StatBaseValueTransform[]
                {
                    v => (int) (v * 1.1),
                },
                defenceBaseValueTransforms: new StatBaseValueTransform[]
                {
                    v => (int) (v * 1.2),
                },
                speedBaseValueTransforms: new StatBaseValueTransform[]
                {
                    v => (int) (v * 1.3),
                }
            );
            _ = character.EquipItem(item);

            // Act
            var stats = character.Stats;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(stats.Attack.CurrentValue, Is.EqualTo(11));
                Assert.That(stats.Defence.CurrentValue, Is.EqualTo(24));
                Assert.That(stats.Speed.CurrentValue, Is.EqualTo(39));
            });
        }

        [Test]
        public void EquipItem_FirstItem_SetsItem()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter();
            var item = TestHelpers.CreateItem();

            // Act
            var result = target.EquipItem(item);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(target.Item, Is.EqualTo(item));
                Assert.That(result.Success, Is.True);
                Assert.That(result.HadPreviousItem, Is.False);
                Assert.That(result.PreviousItem, Is.Null);
            });
        }

        [Test]
        public void EquipItem_SubsequentItem_SetsItem()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter();

            var item1 = TestHelpers.CreateItem();
            _ = target.EquipItem(item1);

            var item2 = TestHelpers.CreateItem();

            // Act
            var result = target.EquipItem(item2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(target.Item, Is.EqualTo(item2));
                Assert.That(result.Success, Is.True);
                Assert.That(result.HadPreviousItem, Is.True);
                Assert.That(result.PreviousItem, Is.EqualTo(item1));
            });
        }

        [Test]
        public void RemoveItem_HasItem_RemovesItem()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter();
            var item = TestHelpers.CreateItem();
            _ = target.EquipItem(item);

            // Act
            var result = target.RemoveItem();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(target.HasItem, Is.False);
                Assert.That(result.Success, Is.True);
                Assert.That(result.Item, Is.EqualTo(item));
            });
        }

        [Test]
        public void RemoveItem_HasNoItem_Fails()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter();

            // Act
            var result = target.RemoveItem();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.Item, Is.Null);
            });
        }

        [Test]
        public void ReceiveDamage_TakesDamage()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5);

            // Act
            _ = target.ReceiveDamage<string>(2, "omd");

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(3));
        }

        [Test]
        public void ReceiveDamage_WithProtectCounter_TakesNoDamage()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5);
            target.AddProtect<string>("userId");

            // Act
            _ = target.ReceiveDamage<string>(2, "omd");

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(5));
        }

        [Test]
        public void ReceiveDamage_IsDeadIfNoHealthLeft()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5);

            // Act
            _ = target.ReceiveDamage<string>(6, "omd");

            // Assert
            Assert.That(target.IsDead, Is.True);
        }

        [Test]
        public void ReceiveBuff_ChangesStatMultipliers()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter(attack: 10, defence: 10, speed: 10);

            // Act
            target.ReceiveBuff<string>(new Dictionary<StatCategory, double>
            {
                [StatCategory.Attack] = 0.2,
                [StatCategory.Defence] = -0.3,
                [StatCategory.Speed] = -0.1,
            }, "omd");

            // Assert
            Assert.That(target.Stats.Attack.CurrentValue, Is.EqualTo(12));
            Assert.That(target.Stats.Defence.CurrentValue, Is.EqualTo(7));
            Assert.That(target.CurrentSpeed, Is.EqualTo(9));
        }

        [Test]
        public void Heal_AddsHealth()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5);
            _ = target.ReceiveDamage<string>(2, "omd");

            // Act
            target.Heal<string>(2, "omd");

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(5));
        }

        [Test]
        public void AddProtect_AddsProtectActionToQueue()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter();

            // Act
            _ = target.AddProtect<string>("DJ rozwell");

            // Assert
            Assert.That(target.ProtectCount, Is.EqualTo(1));
        }

        [Test]
        public void AddProtect_LimitReached_ProtectActionNotAddedToQueue()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter();
            _ = target.AddProtect<string>("DJ rozwell");

            // Act
            _ = target.AddProtect<string>(target.Id);

            // Assert
            Assert.That(target.ProtectCount, Is.EqualTo(1));
        }

        [Test]
        public void ChangeProtectLimit_ChangesProtectLimit()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter();
            _ = target.AddProtect<string>("DJ rozwell");
            _ = target.ChangeProtectLimit<string>(1, target.Id); // ensures this isn't protected against

            // Act
            _ = target.AddProtect<string>(target.Id); // ensures this isn't protected against

            // Assert
            Assert.That(target.ProtectCount, Is.EqualTo(2));
        }

        [Test]
        public void ConsumeProtect_ReturnsIdOfProtectUser()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter();
            _ = target.AddProtect<string>("DJ rozwell");

            // Act
            var userId = target.ConsumeProtect();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(target.ProtectCount, Is.Zero);
                Assert.That(userId, Is.EqualTo("DJ rozwell"));
            });
        }

        [Test]
        public void ConsumeProtect_EmptyProtectQueue_Throws()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = target.ConsumeProtect());
        }

        [Test]
        public void ClearProtectQueue_EmptiesProtectQueue()
        {
            // Arrange
            var target = TestHelpers.CreateBasicCharacter();
            _ = target.AddProtect<string>("DJ rozwell");
            _ = target.AddProtect<string>("DJ rozwell");

            // Act
            target.ClearProtectQueue();

            // Act and Assert
            Assert.That(target.ProtectCount, Is.Zero);
        }
    }
}
