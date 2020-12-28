using System;
using BattleSystem.Moves.Actions;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Actions
{
    /// <summary>
    /// Unit tests for <see cref="ProtectLimitChangeBuilder"/>.
    /// </summary>
    [TestFixture]
    public class ProtectLimitChangeBuilderTests
    {
        [Test]
        public void WithMoveTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new ProtectLimitChangeBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithMoveTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new ProtectLimitChangeBuilder()
                            .WithAmount(1)
                            .TargetsAll();

            // Act
            var attack = builder.Build();

            // Assert
            Assert.That(attack, Is.Not.Null);
        }

        [Test]
        public void Build_MissingAmount_Throws()
        {
            // Arrange
            var builder = new ProtectLimitChangeBuilder()
                            .TargetsUser();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingMoveTargetCalculator_Throws()
        {
            // Arrange
            var builder = new ProtectLimitChangeBuilder()
                            .WithAmount(1);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
