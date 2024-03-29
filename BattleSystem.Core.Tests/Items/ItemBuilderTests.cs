﻿using System;
using BattleSystem.Core.Actions;
using BattleSystem.Core.Items;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Items
{
    /// <summary>
    /// Unit tests for <see cref="ItemBuilder"/>.
    /// </summary>
    [TestFixture]
    public class ItemBuilderTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Name_BadArgument_Throws(string name)
        {
            // Arrange
            var builder = new ItemBuilder();
            
            // Act and Assert
            Assert.Throws<ArgumentException>(() => _ = builder.Name(name));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Describe_BadArgument_Throws(string description)
        {
            // Arrange
            var builder = new ItemBuilder();

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _ = builder.Describe(description));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new ItemBuilder()
                                .Name("caribou")
                                .Describe("andorra")
                                .WithActionContainer(new ActionContainer());

            // Act
            var move = builder.Build();

            // Assert
            Assert.That(move, Is.Not.Null);
        }

        [Test]
        public void Build_MissingName_Throws()
        {
            // Arrange
            var builder = new ItemBuilder()
                                .Describe("andorra");

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingDescription_Throws()
        {
            // Arrange
            var builder = new ItemBuilder()
                                .Name("caribou");

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
