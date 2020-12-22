using BattleSystem.Moves.Actions;
using BattleSystemExample.Input;
using BattleSystemExample.Moves.Targets;
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
            return builder.WithMoveTargetCalculator(new SingleMoveTargetCalculator(userInput, gameOutput));
        }
    }
}
