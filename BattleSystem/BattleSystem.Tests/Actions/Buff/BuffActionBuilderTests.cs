using System;
using BattleSystem.Actions.Buff;
using NUnit.Framework;

namespace BattleSystem.Tests.Actions.Buff
{
    /// <summary>
    /// Unit tests for <see cref="BuffActionBuilder"/>.
    /// </summary>
    [TestFixture]
    public class BuffActionBuilderTests
    {
        [Test]
        public void WithActionTargetCalculator_NullArgument_Throws()
        {
            // Arrange
            var builder = new BuffActionBuilder();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => _ = builder.WithActionTargetCalculator(null));
        }

        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new BuffActionBuilder()
                                .TargetsUser()
                                .WithRaiseAttack(0.1);

            // Act
            var buff = builder.Build();

            // Assert
            Assert.That(buff, Is.Not.Null);
        }

        [Test]
        public void Build_MissingActionTargetCalculator_Throws()
        {
            // Arrange
            var builder = new BuffActionBuilder()
                                .WithRaiseDefence(0.1);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _ = builder.Build());
        }
    }
}
