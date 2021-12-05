using BattleSystem.Core.Actions;
using Moq;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Actions
{
    /// <summary>
    /// Unit tests for <see cref="ActionContainerBuilder"/>.
    /// </summary>
    [TestFixture]
    public class ActionContainerBuilderTests
    {
        [Test]
        public void Build_CallsPresent_Succeeds()
        {
            // Arrange
            var builder = new ActionContainerBuilder()
                                .WithIncreaseAttack()
                                .WithDefenceValueTransform(v => v + 1)
                                .WithSpeedValueTransform(v => v + 2)
                                .WithDamagePowerTransform(v => v + 3)
                                .WithTaggedAction(Mock.Of<IAction>(), "mtg");

            // Act
            var container = builder.Build();

            // Assert
            Assert.That(container, Is.Not.Null);
        }
    }
}
