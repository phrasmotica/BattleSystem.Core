using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Moves.Targets;
using BattleSystem.Stats;

namespace BattleSystem.Moves.Actions
{
    /// <summary>
    /// Represents a buffing move action.
    /// </summary>
    public class Buff : IMoveAction
    {
        /// <summary>
        /// The move target calculator.
        /// </summary>
        private readonly IMoveTargetCalculator _moveTargetCalculator;

        /// <summary>
        /// Gets or sets the buff's stat multipliers for the user.
        /// </summary>
        public IDictionary<StatCategory, double> UserMultipliers { get; set; }

        /// <summary>
        /// Gets or sets the buff's stat multipliers for the target.
        /// </summary>
        public IDictionary<StatCategory, double> TargetMultipliers { get; set; }

        /// <summary>
        /// Creates a new <see cref="Buff"/>.
        /// </summary>
        /// <param name="moveTargetCalculator">The move target calculator.</param>
        /// <param name="userMultipliers">The user stat multipliers.</param>
        /// <param name="targetMultipliers">The target stat multipliers.</param>
        public Buff(
            IMoveTargetCalculator moveTargetCalculator,
            IDictionary<StatCategory, double> userMultipliers,
            IDictionary<StatCategory, double> targetMultipliers)
        {
            _moveTargetCalculator = moveTargetCalculator;

            UserMultipliers = userMultipliers ?? new Dictionary<StatCategory, double>();
            TargetMultipliers = targetMultipliers ?? new Dictionary<StatCategory, double>();
        }

        /// <inheritdoc />
        public virtual void Use(Character user, IEnumerable<Character> otherCharacters)
        {
            user.ReceiveBuff(UserMultipliers);

            var targets = _moveTargetCalculator.Calculate(user, otherCharacters);

            foreach (var target in targets)
            {
                target.ReceiveBuff(TargetMultipliers);
            }
        }

        /// <summary>
        /// Returns a buff that raises the user's attack by 10% of its base value.
        /// </summary>
        public static Buff RaiseUserAttack()
        {
            return new Buff(
                new UserMoveTargetCalculator(),
                new Dictionary<StatCategory, double>
                {
                    [StatCategory.Attack] = 0.1
                },
                null);
        }

        /// <summary>
        /// Returns a buff that raises the user's defence by 10% of its base value.
        /// </summary>
        public static Buff RaiseUserDefence()
        {
            return new Buff(
                new UserMoveTargetCalculator(),
                new Dictionary<StatCategory, double>
                {
                    [StatCategory.Defence] = 0.1
                },
                null);
        }
    }
}
