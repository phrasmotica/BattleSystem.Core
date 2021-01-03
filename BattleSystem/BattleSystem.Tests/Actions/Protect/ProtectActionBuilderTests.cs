using System;
using BattleSystem.Actions.Protect;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Protect
{
    /// <summary>
    /// Unit tests for <see cref="ProtectActionBuilder"/>.
    /// </summary>
    [TestFixture]
    public class ProtectActionBuilderTests
    {
        [Test]
        public void WithActionTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new ProtectActionBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithActionTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new ProtectActionBuilder().TargetsAll();

            // Act
            var protect = builder.Build();

            // Assert
            Assert.That(protect, Is.Not.Null);
        }

        [Test]
        public void Build_MissingActionTargetCalculator_Throws()
        {
            // Arrange
            var builder = new ProtectActionBuilder();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
