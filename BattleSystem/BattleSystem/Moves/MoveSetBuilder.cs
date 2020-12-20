namespace BattleSystem.Moves
{
    /// <summary>
    /// Builder class for move sets.
    /// </summary>
    public class MoveSetBuilder
    {
        /// <summary>
        /// The move set to build.
        /// </summary>
        private readonly MoveSet _moveSet;

        /// <summary>
        /// Creates a new <see cref="MoveSetBuilder"/> instance.
        /// </summary>
        public MoveSetBuilder()
        {
            _moveSet = new MoveSet();
        }

        /// <summary>
        /// Adds the given move to the built move set.
        /// </summary>
        /// <param name="move">The move to add.</param>
        public MoveSetBuilder WithMove(Move move)
        {
            _moveSet.AddMove(move);
            return this;
        }

        /// <summary>
        /// Returns the built move set.
        /// </summary>
        public MoveSet Build()
        {
            return _moveSet;
        }
    }
}
