using BattleSystem.Characters;

namespace BattleSystem.Moves
{
    /// <summary>
    /// Interface for the behaviour of a move.
    /// </summary>
    public interface IMove
    {
        /// <summary>
        /// Gets or sets the move's name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the move's description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the move's maximum uses.
        /// </summary>
        int MaxUses { get; set; }

        /// <summary>
        /// Gets or sets the attack's remaining uses.
        /// </summary>
        int RemainingUses { get; set; }

        /// <summary>
        /// Gets a summary of the move.
        /// </summary>
        string Summary { get; }

        /// <summary>
        /// Returns whether the move can be used.
        /// </summary>
        bool CanUse();

        /// <summary>
        /// Applies the effects of the move.
        /// </summary>
        /// <param name="user">The user of the move.</param>
        /// <param name="target">The target of the move.</param>
        void Use(Character user, Character target);
    }
}
