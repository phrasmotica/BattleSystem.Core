using System;
using BattleSystem.Actions.Heal;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Heal
{
    /// <summary>
    /// Unit tests for <see cref="HealActionBuilder"/>.
    /// </summary>
    [TestFixture]
    public class HealActionBuilderTests
    {
        [Test]
        public void WithHealingCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new HealActionBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithHealingCalculator(null));
        }

        [Test]
        public void WithActionTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new HealActionBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithActionTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new HealActionBuilder()
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
            var builder = new HealActionBuilder()
                                .WithAmount(20)
                                .TargetsAll();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingActionTargetCalculator_Throws()
        {
            // Arrange
            var builder = new HealActionBuilder()
                                .WithAmount(20)
                                .PercentageHealing();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
