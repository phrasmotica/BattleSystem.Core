using System;
using BattleSystem.Actions;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions
{
    /// <summary>
    /// Unit tests for <see cref="ProtectBuilder"/>.
    /// </summary>
    [TestFixture]
    public class ProtectBuilderTests
    {
        [Test]
        public void WithActionTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new ProtectBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithActionTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new ProtectBuilder().TargetsAll();

            // Act
            var protect = builder.Build();

            // Assert
            Assert.That(protect, Is.Not.Null);
        }

        [Test]
        public void Build_MissingActionTargetCalculator_Throws()
        {
            // Arrange
            var builder = new ProtectBuilder();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
