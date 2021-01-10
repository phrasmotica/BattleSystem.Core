using System;
using System.Collections.Generic;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Moves;
using BattleSystem.Core.Stats;
using NUnit.Framework;
using static BattleSystem.Core.Actions.ActionContainer;
using static BattleSystem.Core.Items.Item;

namespace BattleSystem.Core.Tests.Characters
{
    /// <summary>
    /// Unit tests for <see cref="BasicCharacter"/>.
    /// </summary>
    [TestFixture]
    public class BasicCharacterTests
    {
        [Test]
        public void Ctor_NullRandom_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new BasicCharacter(
                    "velocity",
                    "design",
                    12,
                    new StatSet(),
                    new MoveSet(),
                    null);
            });
        }

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
        public void CurrentStats_NoItem_ReturnsStats()
        {
            // Arrange
            var character = TestHelpers.CreateBasicCharacter(attack: 1, defence: 2, speed: 3);

            // Act and Assert
            Assert.Multiple(() =>
            {
                Assert.That(character.CurrentAttack, Is.EqualTo(1));
                Assert.That(character.CurrentDefence, Is.EqualTo(2));
                Assert.That(character.CurrentSpeed, Is.EqualTo(3));
            });
        }

        [Test]
        public void CurrentStats_WithItem_ReturnsTransformedStats()
        {
            // Arrange
            var character = TestHelpers.CreateBasicCharacter(attack: 10, defence: 20, speed: 30);

            var item = TestHelpers.CreateItem(
                actionContainer: TestHelpers.CreateActionContainer(
                    attackValueTransforms: new StatValueTransform[]
                    {
                        v => (int) (v * 1.1),
                    },
                    defenceValueTransforms: new StatValueTransform[]
                    {
                        v => (int) (v * 1.2),
                    },
                    speedValueTransforms: new StatValueTransform[]
                    {
                        v => (int) (v * 1.3),
                    }
                )
            );
            _ = character.EquipItem(item);

            // Act and Assert
            Assert.Multiple(() =>
            {
                Assert.That(character.CurrentAttack, Is.EqualTo(11));
                Assert.That(character.CurrentDefence, Is.EqualTo(24));
                Assert.That(character.CurrentSpeed, Is.EqualTo(39));
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
            var user = TestHelpers.CreateBasicCharacter();
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5);

            // Act
            _ = target.ReceiveDamage<string>(2, user);

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(3));
        }

        [Test]
        public void ReceiveDamage_WithProtectCounter_TakesNoDamage()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5);
            target.AddProtect<string>(user);

            // Act
            _ = target.ReceiveDamage<string>(2, user);

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(5));
        }

        [Test]
        public void ReceiveDamage_IsDeadIfNoHealthLeft()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5);

            // Act
            _ = target.ReceiveDamage<string>(6, user);

            // Assert
            Assert.That(target.IsDead, Is.True);
        }

        [Test]
        public void ReceiveBuff_ChangesStatMultipliers()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var target = TestHelpers.CreateBasicCharacter(attack: 10, defence: 10, speed: 10);

            // Act
            target.ReceiveBuff<string>(new Dictionary<StatCategory, double>
            {
                [StatCategory.Attack] = 0.2,
                [StatCategory.Defence] = -0.3,
                [StatCategory.Speed] = -0.1,
            }, user);

            // Assert
            Assert.That(target.CurrentAttack, Is.EqualTo(12));
            Assert.That(target.CurrentDefence, Is.EqualTo(7));
            Assert.That(target.CurrentSpeed, Is.EqualTo(9));
        }

        [Test]
        public void Heal_AddsHealth()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 5);
            _ = target.ReceiveDamage<string>(2, user);

            // Act
            target.Heal<string>(2, user);

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(5));
        }

        [Test]
        public void AddProtect_AddsProtectActionToQueue()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var target = TestHelpers.CreateBasicCharacter();

            // Act
            _ = target.AddProtect<string>(user);

            // Assert
            Assert.That(target.ProtectCount, Is.EqualTo(1));
        }

        [Test]
        public void AddProtect_LimitReached_ProtectActionNotAddedToQueue()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var target = TestHelpers.CreateBasicCharacter();
            _ = target.AddProtect<string>(user);

            // Act
            _ = target.AddProtect<string>(target);

            // Assert
            Assert.That(target.ProtectCount, Is.EqualTo(1));
        }

        [Test]
        public void ChangeProtectLimit_ChangesProtectLimit()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var target = TestHelpers.CreateBasicCharacter();
            _ = target.AddProtect<string>(user);
            _ = target.ChangeProtectLimit<string>(1, target); // ensures this isn't protected against

            // Act
            _ = target.AddProtect<string>(target); // ensures this isn't protected against

            // Assert
            Assert.That(target.ProtectCount, Is.EqualTo(2));
        }

        [Test]
        public void ConsumeProtect_ReturnsProtectUser()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var target = TestHelpers.CreateBasicCharacter();
            _ = target.AddProtect<string>(user);

            // Act
            var protectingUser = target.ConsumeProtect();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(target.ProtectCount, Is.Zero);
                Assert.That(protectingUser, Is.EqualTo(user));
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
            var user = TestHelpers.CreateBasicCharacter();
            var target = TestHelpers.CreateBasicCharacter();
            _ = target.AddProtect<string>(user);
            _ = target.AddProtect<string>(user);

            // Act
            target.ClearProtectQueue();

            // Act and Assert
            Assert.That(target.ProtectCount, Is.Zero);
        }
    }
}
