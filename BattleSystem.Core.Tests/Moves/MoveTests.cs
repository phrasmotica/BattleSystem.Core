using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Moves;
using BattleSystem.Core.Actions;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Moves
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
            Assert.That(move.CanUse, Is.EqualTo(expectedCanUse));
        }

        [Test]
        public void Use_ReducesRemainingUses()
        {
            // Arrange
            var move = TestHelpers.CreateMove(
                maxUses: 5,
                moveActions: TestHelpers.CreateDamageAction());

            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            move.SetTargets(user, otherCharacters);

            // Act
            _ = move.Use(user, otherCharacters);

            // Assert
            Assert.That(move.RemainingUses, Is.EqualTo(4));
        }

        [Test]
        public void Use_WhenUserWillFlinch_ActionsNotApplied()
        {
            // Arrange
            var move = TestHelpers.CreateMove(
                moveActions: TestHelpers.CreateDamageAction());

            var user = TestHelpers.CreateBasicCharacter();
            user.WillFlinch = true;

            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(),
            };

            // Act
            var (result, actionsResults) = move.Use(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(MoveUseResult.Flinched));
                Assert.That(actionsResults, Is.Empty);
            });
        }

        [Test]
        public void Use_WhenActionFails_SubsequentActionsNotExecuted()
        {
            // Arrange
            var move = TestHelpers.CreateMove(
                moveActions: new[]
                {
                    TestHelpers.CreateDamageAction(),
                    TestHelpers.CreateDamageAction(),
                });

            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter()
            };

            // Act
            var (_, actionsResults) = move.Use(user, otherCharacters);

            // Assert
            Assert.That(actionsResults.ToArray(), Has.Length.EqualTo(1));
        }

        [Test]
        public void Use_WhenActionDidNotTargetCharacter_CharacterStillConsideredForSubsequentActions()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(),
                TestHelpers.CreateBasicCharacter(),
            };

            static IActionResult<Move> MockActionResult(Character target)
            {
                var result = new Mock<IActionResult<Move>>();
                result.SetupGet(m => m.Applied).Returns(true);
                result.SetupGet(m => m.Target).Returns(target);
                return result.Object;
            }

            var firstAction = new Mock<IAction>();

            firstAction
                .Setup(
                    m => m.SetTargets(
                        It.IsAny<Character>(),
                        It.IsAny<IEnumerable<Character>>()
                    )
                );

            firstAction
                .Setup(
                    m => m.Use<Move>(
                        It.IsAny<Character>(),
                        It.IsAny<IEnumerable<Character>>()
                    )
                )
                .Returns((Character _, IEnumerable<Character> otherCharacters) =>
                {
                    return new ActionUseResult<Move>
                    {
                        Success = true,
                        Results = new[] { MockActionResult(otherCharacters.First()) },
                    };
                });

            var secondAction = new Mock<IAction>();

            secondAction
                .Setup(
                    m => m.SetTargets(
                        It.IsAny<Character>(),
                        It.IsAny<IEnumerable<Character>>()
                    )
                );

            secondAction
                .Setup(
                    m => m.Use<Move>(
                        It.IsAny<Character>(),
                        It.IsAny<IEnumerable<Character>>()
                    )
                )
                .Returns((Character _, IEnumerable<Character> otherCharacters) =>
                {
                    return new ActionUseResult<Move>
                    {
                        Success = true,
                        Results = otherCharacters.Select(MockActionResult),
                    };
                });

            // second action not applying to other characters means third and fourth
            // should not be executed at all
            var move = TestHelpers.CreateMove(
                moveActions: new[] { firstAction.Object, secondAction.Object });

            move.SetTargets(user, otherCharacters);

            // Act
            var (_, actionsResults) = move.Use(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                // first action should be applied to first other character
                var firstActionResults = actionsResults.ToArray()[0].Results.ToArray();
                Assert.That(firstActionResults.Length, Is.EqualTo(1));
                Assert.That(firstActionResults[0].Applied, Is.True);

                // second action should be applied to both other characters
                var secondActionResults = actionsResults.ToArray()[1].Results.ToArray();
                Assert.That(secondActionResults.Length, Is.EqualTo(2));
                Assert.That(secondActionResults[0].Applied, Is.True);
                Assert.That(secondActionResults[1].Applied, Is.True);
            });
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

            static IActionResult<Move> MockActionResult(bool applied, Character target)
            {
                var result = new Mock<IActionResult<Move>>();
                result.SetupGet(m => m.Applied).Returns(applied);
                result.SetupGet(m => m.Target).Returns(target);
                return result.Object;
            }

            static IAction MockAction(bool applied = true)
            {
                var action = new Mock<IAction>();

                action
                    .Setup(
                        m => m.SetTargets(
                            It.IsAny<Character>(),
                            It.IsAny<IEnumerable<Character>>()
                        )
                    );

                action
                    .Setup(
                        m => m.Use<Move>(
                            It.IsAny<Character>(),
                            It.IsAny<IEnumerable<Character>>()
                        )
                    )
                    .Returns((Character _, IEnumerable<Character> otherCharacters) =>
                    {
                        return new ActionUseResult<Move>
                        {
                            Success = true,
                            Results = otherCharacters.Select(c => MockActionResult(applied, c)),
                        };
                    });

                return action.Object;
            }

            // second action not applying to other characters means third and fourth
            // should not be executed at all
            var move = TestHelpers.CreateMove(
                moveActions: new[] { MockAction(), MockAction(false), MockAction(), MockAction() });

            move.SetTargets(user, otherCharacters);

            // Act
            var (_, actionsResults) = move.Use(user, otherCharacters);

            // Assert
            Assert.Multiple(() =>
            {
                // first action should be executed and applied to target
                var firstActionResults = actionsResults.ToArray()[0].Results.ToArray();
                Assert.That(firstActionResults.Length, Is.EqualTo(1));
                Assert.That(firstActionResults[0].Applied, Is.True);

                // second action should be executed but not applied to target
                var secondActionResults = actionsResults.ToArray()[1].Results.ToArray();
                Assert.That(secondActionResults.Length, Is.EqualTo(1));
                Assert.That(secondActionResults[0].Applied, Is.False);

                // third and fourth actions should not be executed at all
                Assert.That(actionsResults.ToArray()[2].Results, Is.Empty);
                Assert.That(actionsResults.ToArray()[3].Results, Is.Empty);
            });
        }
    }
}
