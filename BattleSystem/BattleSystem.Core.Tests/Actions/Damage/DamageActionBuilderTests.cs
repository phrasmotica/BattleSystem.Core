using System;
using BattleSystem.Core.Actions.Damage;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions.Damage
{
    /// <summary>
    /// Unit tests for <see cref="DamageActionBuilder"/>.
    /// </summary>
    [TestFixture]
    public class DamageActionBuilderTests
    {
        [Test]
        public void WithDamageCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new DamageActionBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithDamageCalculator(null));
        }

        [Test]
        public void WithActionTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new DamageActionBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithActionTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new DamageActionBuilder()
                            .AbsoluteDamage(20)
                            .TargetsAll();

            // Act
            var damage = builder.Build();

            // Assert
            Assert.That(damage, Is.Not.Null);
        }

        [Test]
        public void Build_MissingDamageCalculator_Throws()
        {
            // Arrange
            var builder = new DamageActionBuilder()
                            .TargetsOthers();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingActionTargetCalculator_Throws()
        {
            // Arrange
            var builder = new DamageActionBuilder()
                            .WithBasePower(20);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
