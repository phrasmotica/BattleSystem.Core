using BattleSystem.Core.Stats;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Stats
{
    /// <summary>
    /// Unit tests for <see cref="Stat"/>.
    /// </summary>
    [TestFixture]
    public class StatTests
    {
        [TestCase(5, 0.6, 3)]
        [TestCase(5, 0.3, 1)]
        public void CurrentValue_UsesMultiplier(int baseValue, double multiplier, int expectedCurrentValue)
        {
            // Arrange
            var stat = new Stat(baseValue, multiplier);

            // Act and Assert
            Assert.That(stat.CurrentValue, Is.EqualTo(expectedCurrentValue));
        }
    }
}
