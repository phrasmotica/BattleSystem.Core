﻿using BattleSystem.Core.Moves;
using BattleSystem.Core.Success;
using NUnit.Framework;

namespace BattleSystem.Core.Tests.Success
{
    /// <summary>
    /// Unit tests for <see cref="AlwaysSuccessCalculator"/>.
    /// </summary>
    [TestFixture]
    public class AlwaysSuccessCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsSuccess()
        {
            // Arrange
            var calculator = new AlwaysSuccessCalculator();

            var user = TestHelpers.CreateBasicCharacter();
            var move = TestHelpers.CreateMove();
            var otherCharacters = new[]
            {
                TestHelpers.CreateBasicCharacter(maxHealth: 8)
            };

            // Act
            var result = calculator.Calculate(user, move, otherCharacters);

            // Assert
            Assert.That(result, Is.EqualTo(MoveUseResult.Success));
        }
    }
}
