using BattleSystem.Items;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Interface for an object that can be transformed by an item.
    /// </summary>
    public interface ITransformable
    {
        /// <summary>
        /// Receives the relevant transforms from the given item.
        /// </summary>
        /// <param name="item">The item.</param>
        void ReceiveTransforms(Item item);

        /// <summary>
        /// Clears any transforms this object may have received.
        /// </summary>
        void ClearTransforms();
    }
}
