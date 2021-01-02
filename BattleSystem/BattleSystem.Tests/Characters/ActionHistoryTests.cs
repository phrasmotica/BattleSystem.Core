using BattleSystem.Actions.Results;
using BattleSystem.Characters;
using BattleSystem.Items;
using BattleSystem.Moves;
using NUnit.Framework;

namespace BattleSystem.Tests.Characters
{
    /// <summary>
    /// Unit tests for <see cref="ActionHistory"/>.
    /// </summary>
    [TestFixture]
    public class ActionHistoryTests
    {
        [Test]
        public void AddAction_ActionFromMove_AddedSuccessfully()
        {
            // Arrange
            var history = new ActionHistory();

            // Act
            history.AddAction(new DamageResult<Move>());

            // Assert
            Assert.That(history.MoveActions.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddAction_ActionFromItem_AddedSuccessfully()
        {
            // Arrange
            var history = new ActionHistory();

            // Act
            history.AddAction(new DamageResult<Item>());

            // Assert
            Assert.That(history.ItemActions.Count, Is.EqualTo(1));
        }
    }
}
