using BattleSystem.Actions.Damage;
using BattleSystemExample.Input;
using BattleSystemExample.Actions.Targets;
using BattleSystemExample.Output;

namespace BattleSystemExample.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DamageBuilder"/>.
    /// </summary>
    public static class DamageBuilderExtensions
    {
        /// <summary>
        /// Sets the built attack to target a single character chosen by the user.
        /// </summary>
        /// <param name="builder">The attack builder.</param>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public static DamageBuilder UserSelectsSingleTarget(this DamageBuilder builder, IUserInput userInput, IGameOutput gameOutput)
        {
            return builder.WithActionTargetCalculator(new SingleActionTargetCalculator(userInput, gameOutput));
        }

        /// <summary>
        /// Sets the built attack to target a single other character chosen by the user.
        /// </summary>
        /// <param name="builder">The attack builder.</param>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public static DamageBuilder UserSelectsSingleOtherTarget(this DamageBuilder builder, IUserInput userInput, IGameOutput gameOutput)
        {
            return builder.WithActionTargetCalculator(new SingleOtherActionTargetCalculator(userInput, gameOutput));
        }

        /// <summary>
        /// Sets the built attack to target a single ally chosen by the user.
        /// </summary>
        /// <param name="builder">The attack builder.</param>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public static DamageBuilder UserSelectsSingleAlly(this DamageBuilder builder, IUserInput userInput, IGameOutput gameOutput)
        {
            return builder.WithActionTargetCalculator(new SingleAllyActionTargetCalculator(userInput, gameOutput));
        }

        /// <summary>
        /// Sets the built attack to target a single enemy chosen by the user.
        /// </summary>
        /// <param name="builder">The attack builder.</param>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public static DamageBuilder UserSelectsSingleEnemy(this DamageBuilder builder, IUserInput userInput, IGameOutput gameOutput)
        {
            return builder.WithActionTargetCalculator(new SingleEnemyActionTargetCalculator(userInput, gameOutput));
        }

        /// <summary>
        /// Sets the built damage action to target the user of the move
        /// containing the action that last affected the user.
        /// </summary>
        public static DamageBuilder Retaliates(this DamageBuilder builder)
        {
            return builder.WithActionTargetCalculator(new RetaliationActionTargetCalculator());
        }
    }
}
