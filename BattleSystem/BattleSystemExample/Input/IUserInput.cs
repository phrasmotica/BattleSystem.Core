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
        /// Returns once the user has confirmed the given prompt.
        /// </summary>
        /// <param name="prompt">The prompt.</param>
        void Confirm(string prompt = null);
    }
}
