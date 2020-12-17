namespace BattleSystemExample.Input
{
    /// <summary>
    /// Interface for ways the game can output messages.
    /// </summary>
    public interface IGameOutput
    {
        /// <summary>
        /// Writes an empty message to the output.
        /// </summary>
        void WriteLine();

        /// <summary>
        /// Writes the given message to the output.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteLine(string message);
    }
}
