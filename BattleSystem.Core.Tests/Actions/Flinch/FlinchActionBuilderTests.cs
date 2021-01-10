using System;
using BattleSystem.Core.Actions.Flinch;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.Flinch
{
    /// <summary>
    /// Unit tests for <see cref="FlinchActionBuilder"/>.
    /// </summary>
    [TestFixture]
    public class FlinchActionBuilderTests
    {
        [Test]
        public void WithActionTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new FlinchActionBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithActionTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new FlinchActionBuilder()
                                .TargetsUser();

            // Act
            var flinch = builder.Build();

            // Assert
            Assert.That(flinch, Is.Not.Null);
        }

        [Test]
        public void Build_MissingActionTargetCalculator_Throws()
        {
            // Arrange
            var builder = new FlinchActionBuilder();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
