using System;
using BattleSystem.Moves.Actions;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Actions
{
    /// <summary>
    /// Unit tests for <see cref="HealBuilder"/>.
    /// </summary>
    [TestFixture]
    public class HealBuilderTests
    {
        [Test]
        public void WithHealingCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new HealBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithHealingCalculator(null));
        }

        [Test]
        public void WithMoveTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new HealBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithMoveTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new HealBuilder()
                                .WithAmount(20)
                                .AbsoluteHealing()
                                .TargetsUser();

            // Act
            var heal = builder.Build();

            // Assert
            Assert.That(heal, Is.Not.Null);
        }

        [Test]
        public void Build_MissingHealingCalculator_Throws()
        {
            // Arrange
            var builder = new HealBuilder()
                                .WithAmount(20)
                                .TargetsAll();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingMoveTargetCalculator_Throws()
        {
            // Arrange
            var builder = new HealBuilder()
                                .WithAmount(20)
                                .PercentageHealing();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
