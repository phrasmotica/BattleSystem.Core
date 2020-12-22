using System;
using BattleSystem.Moves.Actions;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Actions
{
    /// <summary>
    /// Unit tests for <see cref="ProtectBuilder"/>.
    /// </summary>
    [TestFixture]
    public class ProtectBuilderTests
    {
        [Test]
        public void WithMoveTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new ProtectBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithMoveTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new ProtectBuilder().TargetsAll();

            // Act
            var attack = builder.Build();

            // Assert
            Assert.That(attack, Is.Not.Null);
        }[Test]
        public void Build_MissingMoveTargetCalculator_Throws()
        {
            // Arrange
            var builder = new ProtectBuilder();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
