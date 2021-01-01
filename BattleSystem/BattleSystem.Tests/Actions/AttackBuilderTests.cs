﻿using System;
using BattleSystem.Actions;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions
{
    /// <summary>
    /// Unit tests for <see cref="AttackBuilder"/>.
    /// </summary>
    [TestFixture]
    public class AttackBuilderTests
    {
        [Test]
        public void WithDamageCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new AttackBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithDamageCalculator(null));
        }

        [Test]
        public void WithActionTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new AttackBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithActionTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new AttackBuilder()
                                .WithPower(20)
                                .AbsoluteDamage()
                                .TargetsAll();

            // Act
            var attack = builder.Build();

            // Assert
            Assert.That(attack, Is.Not.Null);
        }

        [Test]
        public void Build_MissingPower_Throws()
        {
            // Arrange
            var builder = new AttackBuilder()
                                .PercentageDamage()
                                .TargetsFirstEnemy();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingDamageCalculator_Throws()
        {
            // Arrange
            var builder = new AttackBuilder()
                                .WithPower(20)
                                .TargetsOthers();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingactionTargetCalculator_Throws()
        {
            // Arrange
            var builder = new AttackBuilder()
                                .WithPower(20)
                                .StatDifferenceDamage();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
