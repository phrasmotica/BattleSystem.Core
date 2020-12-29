namespace BattleSystemExample.Input
{
    /// <summary>
    /// Interface for ways a user can control a player.
    /// </summary>
    public interface IUserInput
    {
        /// <summary>
        /// Returns an integer index chosen by the user.
        /// </summary>
        int SelectIndex();

        /// <summary>
        /// Returns the next line of characters chosen by the user.
        /// </summary>
        string ReadLine();

        /// <summary>
        /// Returns one of the given options, chosen by the user.
        /// </summary>
        /// <param name="prompt">The prompt.</param>
        /// <param name="choices">The choices.</param>
        string SelectChoice(string prompt = null, params string[] choices);

        /// <summary>
        /// Returns once the user has confirmed the given prompt.
        /// </summary>
        /// <param name="prompt">The prompt.</param>
        void Confirm(string prompt = null);
    }
}
