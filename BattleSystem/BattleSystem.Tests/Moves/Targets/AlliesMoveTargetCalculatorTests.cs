﻿using System;
using System.Linq;
using BattleSystem.Moves.Targets;
using NUnit.Framework;

namespace BattleSystem.Tests.Moves.Targets
{
    /// <summary>
    /// Unit tests for <see cref="AlliesMoveTargetCalculator"/>
    /// </summary>
    [TestFixture]
    public class AlliesMoveTargetCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsAllies()
        {
            // Arrange
            var calculator = new AlliesMoveTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "a"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "b"),
                TestHelpers.CreateBasicCharacter(name: "154", team: "a"),
            };

            // Act
            var targets = calculator.Calculate(user, otherCharacters).ToArray();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(targets.Length, Is.EqualTo(2));
                Assert.That(targets[0].Name, Is.EqualTo("the"));
                Assert.That(targets[1].Name, Is.EqualTo("154"));
            });
        }

        [Test]
        public void Calculate_NoAllies_Throws()
        {
            // Arrange
            var calculator = new AlliesMoveTargetCalculator();

            var user = TestHelpers.CreateBasicCharacter(name: "wire", team: "a");
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(name: "the", team: "b"),
                TestHelpers.CreateBasicCharacter(name: "15th", team: "b"),
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(
                () => _ = calculator.Calculate(
                    user,
                    otherCharacters
                )
            );
        }
    }
}