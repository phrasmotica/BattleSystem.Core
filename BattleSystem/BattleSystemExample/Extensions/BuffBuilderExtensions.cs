using BattleSystem.Actions;
using BattleSystemExample.Input;
using BattleSystemExample.Actions.Targets;
using BattleSystemExample.Output;

namespace BattleSystemExample.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="BuffBuilder"/>.
    /// </summary>
    public static class BuffBuilderExtensions
    {
        /// <summary>
        /// Sets the built buff to target a single character chosen by the user.
        /// </summary>
        /// <param name="builder">The buff builder.</param>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public static BuffBuilder UserSelectsSingleTarget(this BuffBuilder builder, IUserInput userInput, IGameOutput gameOutput)
        {
            return builder.WithActionTargetCalculator(new SingleActionTargetCalculator(userInput, gameOutput));
        }

        /// <summary>
        /// Sets the built buff to target a single other character chosen by the user.
        /// </summary>
        /// <param name="builder">The buff builder.</param>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public static BuffBuilder UserSelectsSingleOtherTarget(this BuffBuilder builder, IUserInput userInput, IGameOutput gameOutput)
        {
            return builder.WithActionTargetCalculator(new SingleOtherActionTargetCalculator(userInput, gameOutput));
        }

        /// <summary>
        /// Sets the built buff to target a single ally chosen by the user.
        /// </summary>
        /// <param name="builder">The buff builder.</param>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public static BuffBuilder UserSelectsSingleAlly(this BuffBuilder builder, IUserInput userInput, IGameOutput gameOutput)
        {
            return builder.WithActionTargetCalculator(new SingleAllyActionTargetCalculator(userInput, gameOutput));
        }

        /// <summary>
        /// Sets the built buff to target a single enemy chosen by the user.
        /// </summary>
        /// <param name="builder">The buff builder.</param>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public static BuffBuilder UserSelectsSingleEnemy(this BuffBuilder builder, IUserInput userInput, IGameOutput gameOutput)
        {
            return builder.WithActionTargetCalculator(new SingleEnemyActionTargetCalculator(userInput, gameOutput));
        }
    }
}
