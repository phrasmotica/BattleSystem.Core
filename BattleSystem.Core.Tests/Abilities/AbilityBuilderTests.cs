using System;
using BattleSystem.Core.Abilities;
using BattleSystem.Core.Actions;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Abilities
{
    /// <summary>
    /// Unit tests for <see cref="AbilityBuilder"/>.
    /// </summary>
    [TestFixture]
    public class AbilityBuilderTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Name_BadArgument_Throws(string name)
        {
            // Arrange
            var builder = new AbilityBuilder();
            
            // Act and Assert
            Assert.Throws<ArgumentException>(() => _ = builder.Name(name));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Describe_BadArgument_Throws(string description)
        {
            // Arrange
            var builder = new AbilityBuilder();

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _ = builder.Describe(description));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new AbilityBuilder()
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
            var builder = new AbilityBuilder()
                                .Describe("andorra");

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }

        [Test]
        public void Build_MissingDescription_Throws()
        {
            // Arrange
            var builder = new AbilityBuilder()
                                .Name("caribou");

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
