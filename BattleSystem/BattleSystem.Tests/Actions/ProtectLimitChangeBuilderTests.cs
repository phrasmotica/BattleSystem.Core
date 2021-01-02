using System;
using BattleSystem.Actions;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions
{
    /// <summary>
    /// Unit tests for <see cref="ProtectLimitChangeBuilder"/>.
    /// </summary>
    [TestFixture]
    public class ProtectLimitChangeBuilderTests
    {
        [Test]
        public void WithActionTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new ProtectLimitChangeBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithActionTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new ProtectLimitChangeBuilder()
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
            var builder = new ProtectLimitChangeBuilder()
                            .TargetsUser();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingactionTargetCalculator_Throws()
        {
            // Arrange
            var builder = new ProtectLimitChangeBuilder()
                            .WithAmount(1);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
