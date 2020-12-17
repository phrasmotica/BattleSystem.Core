using BattleSystem.Moves;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace BattleSystem.Tests.Moves
{
    /// <summary>
    /// Unit tests for <see cref="Heal"/>.
    /// </summary>
    [TestFixture]
    public class AttackTests
    {
        [TestCase(5, true)]
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(-5, false)]
        public void CanUse_ReturnsCorrectly(int remainingUses, bool expectedCanUse)
        {
            // Arrange
            var attack = new Attack("yeti", remainingUses, 1);
            Constraint constraint = expectedCanUse ? Is.True : Is.False;

            // Act and Assert
            Assert.That(attack.CanUse(), constraint);
        }

        [Test]
        public void Use_DamagesTarget()
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter(attack: 5);
            var target = TestHelpers.CreateBasicCharacter(maxHealth: 8, defence: 3);
            var attack = TestHelpers.CreateAttack(power: 3);

            // Act
            attack.Use(user, target);

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(2));
        }

        [TestCase(5, 5, 8, 7)]
        [TestCase(5, 6, 8, 7)]
        public void Use_NonPositiveDamage_DamagesTarget_OneDamage(int userAttack, int targetDefence, int targetMaxHealth, int expectedTargetHealth)
        {
            // Arrange
            var user = TestHelpers.CreateBasicCharacter(attack: userAttack);
            var target = TestHelpers.CreateBasicCharacter(maxHealth: targetMaxHealth, defence: targetDefence);
            var attack = TestHelpers.CreateAttack(power: 3);

            // Act
            attack.Use(user, target);

            // Assert
            Assert.That(target.CurrentHealth, Is.EqualTo(expectedTargetHealth));
        }

        [Test]
        public void Use_ReducesRemainingUses()
        {
            // Arrange
            var attack = new Attack("yeti", 2, 1);

            // Act
            attack.Use(TestHelpers.CreateBasicCharacter(), TestHelpers.CreateBasicCharacter());

            // Assert
            Assert.That(attack.RemainingUses, Is.EqualTo(1));
        }
    }
}
