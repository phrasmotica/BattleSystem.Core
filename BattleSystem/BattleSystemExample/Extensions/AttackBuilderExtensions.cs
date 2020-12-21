using BattleSystem.Moves.Actions;
using BattleSystemExample.Input;
using BattleSystemExample.Moves.Targets;
using BattleSystemExample.Output;

namespace BattleSystemExample.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="AttackBuilder"/>.
    /// </summary>
    public static class AttackBuilderExtensions
    {
        /// <summary>
        /// Sets the built attack to target a single character chosen by the user.
        /// </summary>
        /// <param name="builder">The attack builder.</param>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output.</param>
        public static AttackBuilder UserSelectsSingleTarget(this AttackBuilder builder, IUserInput userInput, IGameOutput gameOutput)
        {
            return builder.WithMoveTargetCalculator(new SingleChosenMoveTargetCalculator(userInput, gameOutput));
        }
    }
}
