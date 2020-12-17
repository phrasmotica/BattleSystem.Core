using System.Collections.Generic;
using System.Linq;
using BattleSystem.Extensions;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace BattleSystem.Tests.Extensions
{
    /// <summary>
    /// Tests for <see cref="DictionaryExtensions"/>.
    /// </summary>
    [TestFixture]
    public class DictionaryExtensionsTests
    {
        [Test]
        public void Subtract_SubtractsPairedKeys()
        {
            // Arrange
            var dict1 = new Dictionary<string, double>
            {
                ["a"] = 4,
                ["b"] = 5,
                ["c"] = 5,
            };

            var dict2 = new Dictionary<string, double>
            {
                ["a"] = 2,
                ["b"] = 1,
                ["c"] = 5,
            };

            // Act
            var resultDict = dict1.Subtract(dict2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(resultDict["a"], Is.EqualTo(2));
                Assert.That(resultDict["b"], Is.EqualTo(4));
                Assert.That(resultDict, new NotConstraint(Contains.Key("c")));
            });
        }

        [Test]
        public void Subtract_IncludeZeroes_ReturnsZeroes()
        {
            // Arrange
            var dict1 = new Dictionary<string, double>
            {
                ["a"] = 4,
                ["b"] = 5,
                ["c"] = 9,
            };

            var dict2 = new Dictionary<string, double>
            {
                ["a"] = 2,
                ["b"] = 1,
                ["c"] = 9,
            };

            // Act
            var resultDict = dict1.Subtract(dict2, includeZeroes: true);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(resultDict["a"], Is.EqualTo(2));
                Assert.That(resultDict["b"], Is.EqualTo(4));
                Assert.That(resultDict["c"], Is.EqualTo(0));
            });
        }

        [Test]
        public void Subtract_IncludeUnpairedKeys_ReturnsValuesForUnpairedKeys()
        {
            // Arrange
            var dict1 = new Dictionary<string, double>
            {
                ["a"] = 4,
                ["b"] = 5,
                ["c"] = 9,
            };

            var dict2 = new Dictionary<string, double>
            {
                ["a"] = 2,
                ["b"] = 1,
                ["d"] = 10,
            };

            // Act
            var resultDict = dict1.Subtract(dict2, includeUnpairedKeys: true);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(resultDict["a"], Is.EqualTo(2));
                Assert.That(resultDict["b"], Is.EqualTo(4));
                Assert.That(resultDict["c"], Is.EqualTo(9));
                Assert.That(resultDict["d"], Is.EqualTo(10));
            });
        }
    }
}
