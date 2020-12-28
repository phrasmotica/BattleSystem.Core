using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Moves;
using BattleSystem.Moves.Actions;
using BattleSystem.Moves.Actions.Results;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves
{
    /// <summary>
    /// Unit tests for <see cref="Move"/>.
    /// </summary>
    [TestFixture]
    public class MoveTests
    {
        [Test]
        public void SetMaxUses_SetsMaxAndRemainingUses()
        {
            // Arrange
            var move = new Move();

            // Act
            move.SetMaxUses(10);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(move.MaxUses, Is.EqualTo(10));
                Assert.That(move.RemainingUses, Is.EqualTo(10));
            });
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetMaxUses_HigherLimit_SetsMaxAndRemainingUses(bool ignoreRemainingUses)
        {
            // Arrange
            var move = new Move();
            move.SetMaxUses(10);

            // Act
            move.SetMaxUses(15, ignoreRemainingUses);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(move.MaxUses, Is.EqualTo(15));
                Assert.That(move.RemainingUses, Is.EqualTo(ignoreRemainingUses ? 10 : 15));
            });
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetMaxUses_LowerLimit_SetsMaxAndRemainingUses(bool ignoreRemainingUses)
        {
            // Arrange
            var move = new Move();
            move.SetMaxUses(10);

            // Act
            move.SetMaxUses(5, ignoreRemainingUses);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(move.MaxUses, Is.EqualTo(5));
                Assert.That(move.RemainingUses, Is.EqualTo(5));
            });
        }

        [TestCase(5, true)]
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(-1, false)]
        public void CanUse_ReturnsCorrectly(int remainingUses, bool expectedCanUse)
        {
            // Arrange
            var move = TestHelpers.CreateMove(maxUses: remainingUses);

            // Act and Assert
            Assert.That(move.CanUse(), Is.EqualTo(expectedCanUse));
        }

        [Test]
        public void Use_ReducesRemainingUses()
        {
            // Arrange
            var move = TestHelpers.CreateMove(
                maxUses: 5,
                moveActions: new Mock<IMoveAction>().Object);

            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            // Act
            _ = move.Use(user, otherCharacters);

            // Assert
            Assert.That(move.RemainingUses, Is.EqualTo(4));
        }

        [Test]
        public void Use_WhenActionNotAppliedToTarget_SubsequentActionsAreNotExecutedAgainstTarget()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(),
            };

            static IMoveActionResult MockActionResult(bool applied, string targetId)
            {
                var result = new Mock<IMoveActionResult>();
                result.SetupGet(m => m.Applied).Returns(applied);
                result.SetupGet(m => m.TargetId).Returns(targetId);
                return result.Object;
            }

            static IMoveAction MockAction(bool applied = true)
            {
                var action = new Mock<IMoveAction>();
                action
                    .Setup(
                        m => m.Use(
                            It.IsAny<Character>(),
                            It.IsAny<IEnumerable<Character>>()
                        )
                    )
                    .Returns((Character _, IEnumerable<Character> otherCharacters) =>
                    {
                        return otherCharacters.Select(c => MockActionResult(applied, c.Id));
                    });

                return action.Object;
            }

            // second action not applying to other characters means third and fourth
            // should not be applied either
            var move = TestHelpers.CreateMove(
                moveActions: new[] { MockAction(), MockAction(false), MockAction(), MockAction() });

            // Act
            var (_, actionsResults) = move.Use(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                // first action should be executed and applied to target
                var firstActionResults = actionsResults.ToArray()[0].ToArray();
                Assert.That(firstActionResults, Is.Not.Empty);
                Assert.That(firstActionResults[0].Applied, Is.True);

                // second action should be executed but not applied to target
                var secondActionResults = actionsResults.ToArray()[1].ToArray();
                Assert.That(secondActionResults, Is.Not.Empty);
                Assert.That(secondActionResults[0].Applied, Is.False);

                // third and fourth actions should not be executed at all
                Assert.That(actionsResults.ToArray()[2], Is.Empty);
                Assert.That(actionsResults.ToArray()[3], Is.Empty);
            });
        }
    }
}
