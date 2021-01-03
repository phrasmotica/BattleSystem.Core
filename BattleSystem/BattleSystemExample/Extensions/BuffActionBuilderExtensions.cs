using BattleSystem.Actions.Buff;
using BattleSystemExample.Input;
using BattleSystemExample.Actions.Targets;
using BattleSystemExample.Output;

namespace BattleSystemExample.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="BuffActionBuilder"/>.
    /// </summary>
    public static class BuffActionBuilderExtensions
    {
        /// <summary>
        /// Sets the built buff to target a single character chosen by the user.
        /// </summary>
        /// <param name="builder">The buff builder.</param>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public static BuffActionBuilder UserSelectsSingleTarget(this BuffActionBuilder builder, IUserInput userInput, IGameOutput gameOutput)
        {
            return builder.WithActionTargetCalculator(new SingleActionTargetCalculator(userInput, gameOutput));
        }

        /// <summary>
        /// Sets the built buff to target a single other character chosen by the user.
        /// </summary>
        /// <param name="builder">The buff builder.</param>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public static BuffActionBuilder UserSelectsSingleOtherTarget(this BuffActionBuilder builder, IUserInput userInput, IGameOutput gameOutput)
        {
            return builder.WithActionTargetCalculator(new SingleOtherActionTargetCalculator(userInput, gameOutput));
        }

        /// <summary>
        /// Sets the built buff to target a single ally chosen by the user.
        /// </summary>
        /// <param name="builder">The buff builder.</param>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public static BuffActionBuilder UserSelectsSingleAlly(this BuffActionBuilder builder, IUserInput userInput, IGameOutput gameOutput)
        {
            return builder.WithActionTargetCalculator(new SingleAllyActionTargetCalculator(userInput, gameOutput));
        }

        /// <summary>
        /// Sets the built buff to target a single enemy chosen by the user.
        /// </summary>
        /// <param name="builder">The buff builder.</param>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public static BuffActionBuilder UserSelectsSingleEnemy(this BuffActionBuilder builder, IUserInput userInput, IGameOutput gameOutput)
        {
            return builder.WithActionTargetCalculator(new SingleEnemyActionTargetCalculator(userInput, gameOutput));
        }
    }
}
