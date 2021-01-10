using System.Collections.Generic;

namespace BattleSystem.Core.Characters
{
    /// <summary>
    /// Represents a slot in which some asset is held. Assets are stored in a
    /// stack, where the asset at the top of the stack is active.
    /// </summary>
    /// <typeparam name="TAsset">The type of the asset.</typeparam>
    public class Slot<TAsset>
    {
        /// <summary>
        /// The assets that have occupied this slot.
        /// </summary>
        private readonly Stack<TAsset> _assets;

        /// <summary>
        /// Gets whether there is an active asset in this slot.
        /// </summary>
        protected bool HasAsset => _assets.Count > 0 && Current != null;

        /// <summary>
        /// Gets the current asset.
        /// </summary>
        public TAsset Current => _assets.Peek();

        /// <summary>
        /// Creates a new <see cref="Slot"/> instance.
        /// </summary>
        public Slot()
        {
            _assets = new Stack<TAsset>();
        }

        /// <summary>
        /// Sets the current asset to the given asset.
        /// </summary>
        /// <param name="asset">The asset.</param>
        public void Set(TAsset asset)
        {
            _assets.Push(asset);
        }

        /// <summary>
        /// Removes the current asset.
        /// </summary>
        public void Remove()
        {
            _assets.Push(default);
        }
    }
}
