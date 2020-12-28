using System;
using BattleSystem.Actions;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions
{
    /// <summary>
    /// Unit tests for <see cref="BuffBuilder"/>.
    /// </summary>
    [TestFixture]
    public class BuffBuilderTests
    {
        [Test]
        public void WithMoveTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new BuffBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithMoveTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new BuffBuilder()
                                .TargetsUser()
                                .WithRaiseAttack(0.1);

            // Act
            var buff = builder.Build();

            // Assert
            Assert.That(buff, Is.Not.Null);
        }

        [Test]
        public void Build_MissingMoveTargetCalculator_Throws()
        {
            // Arrange
            var builder = new BuffBuilder()
                                .WithRaiseDefence(0.1);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
