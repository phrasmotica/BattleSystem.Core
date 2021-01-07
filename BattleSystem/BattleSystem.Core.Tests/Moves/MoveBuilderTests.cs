using System;
using BattleSystem.Core.Moves;
using BattleSystem.Core.Random;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Moves
{
    /// <summary>
    /// Unit tests for <see cref="MoveBuilder"/>.
    /// </summary>
    [TestFixture]
    public class MoveBuilderTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Name_BadArgument_Throws(string name)
        {
            // Arrange
            var builder = new MoveBuilder();
            
            // Act and Assert
            Assert.Throws<ArgumentException>(() => _ = builder.Name(name));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Describe_BadArgument_Throws(string description)
        {
            // Arrange
            var builder = new MoveBuilder();

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _ = builder.Describe(description));
        }

        [TestCase]
        public void WithSuccessCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new MoveBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithSuccessCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new MoveBuilder()
                                .Name("caribou")
                                .Describe("andorra")
                                .WithMaxUses(9)
                                .WithPriority(1)
                                .WithAccuracy(100, new Mock<IRandom>().Object);

            // Act
            var move = builder.Build();

            // Assert
            Assert.That(move, Is.Not.Null);
        }

        [Test]
        public void Build_MissingName_Throws()
        {
            // Arrange
            var builder = new MoveBuilder()
                                .Describe("andorra")
                                .WithMaxUses(9)
                                .AlwaysSucceeds();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingDescription_Throws()
        {
            // Arrange
            var builder = new MoveBuilder()
                                .Name("caribou")
                                .WithMaxUses(9)
                                .AlwaysSucceeds();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingMaxUses_Throws()
        {
            // Arrange
            var builder = new MoveBuilder()
                                .Name("caribou")
                                .Describe("andorra")
                                .AlwaysSucceeds();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingSuccessCalculator_Throws()
        {
            // Arrange
            var builder = new MoveBuilder()
                                .Name("caribou")
                                .Describe("andorra")
                                .WithMaxUses(9);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
