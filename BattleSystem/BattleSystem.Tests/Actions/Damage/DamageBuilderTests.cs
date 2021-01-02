using System;
using BattleSystem.Actions.Damage;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Damage
{
    /// <summary>
    /// Unit tests for <see cref="DamageBuilder"/>.
    /// </summary>
    [TestFixture]
    public class DamageBuilderTests
    {
        [Test]
        public void WithDamageCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new DamageBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithDamageCalculator(null));
        }

        [Test]
        public void WithActionTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new DamageBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithActionTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new DamageBuilder()
                            .WithPower(20)
                            .AbsoluteDamage()
                            .TargetsAll();

            // Act
            var damage = builder.Build();

            // Assert
            Assert.That(damage, Is.Not.Null);
        }

        [Test]
        public void Build_MissingPower_Throws()
        {
            // Arrange
            var builder = new DamageBuilder()
                            .PercentageDamage()
                            .TargetsFirstEnemy();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingDamageCalculator_Throws()
        {
            // Arrange
            var builder = new DamageBuilder()
                            .WithPower(20)
                            .TargetsOthers();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingactionTargetCalculator_Throws()
        {
            // Arrange
            var builder = new DamageBuilder()
                            .WithPower(20)
                            .StatDifferenceDamage();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
