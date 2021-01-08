using System;
using BattleSystem.Core.Actions.ProtectLimitChange;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.ProtectLimitChange
{
    /// <summary>
    /// Unit tests for <see cref="ProtectLimitChangeActionBuilder"/>.
    /// </summary>
    [TestFixture]
    public class ProtectLimitChangeActionBuilderTests
    {
        [Test]
        public void WithActionTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new ProtectLimitChangeActionBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithActionTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new ProtectLimitChangeActionBuilder()
                            .WithAmount(1)
                            .TargetsAll();

            // Act
            var change = builder.Build();

            // Assert
            Assert.That(change, Is.Not.Null);
        }

        [Test]
        public void Build_MissingAmount_Throws()
        {
            // Arrange
            var builder = new ProtectLimitChangeActionBuilder()
                            .TargetsUser();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingActionTargetCalculator_Throws()
        {
            // Arrange
            var builder = new ProtectLimitChangeActionBuilder()
                            .WithAmount(1);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
