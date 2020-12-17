using System.Collections.Generic;
using System.Linq;
using BattleSystem.Moves;

namespace BattleSystemExample.Battles
{
    /// <summary>
    /// Class for managing the flow of a battle.
    /// </summary>
    public class MoveProcessor
    {
        /// <summary>
        /// The move use queue.
        /// </summary>
        private List<MoveUse> Queue;

        /// <summary>
        /// Gets whether the move use queue is empty.
        /// </summary>
        public bool MoveUseQueueIsEmpty => !Queue.Any();

        /// <summary>
        /// Creates a new <see cref="MoveProcessor"/> instance.
        /// </summary>
        public MoveProcessor()
        {
            Queue = new List<MoveUse>();
        }

        /// <summary>
        /// Tries to add the given move use to the move use queue and returns whether the attempt succeeded.
        /// </summary>
        /// <param name="moveUse">The move use.</param>
        public bool Push(MoveUse moveUse)
        {
            if (moveUse.Move.CanUse())
            {
                Queue.Add(moveUse);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Processes move uses in the appropriate order and returns info about the move uses.
        /// </summary>
        public IEnumerable<MoveUse> Apply()
        {
            SortMoveUseQueue();

            var moveUseInfo = new List<MoveUse>();

            var numberOfMoves = Queue.Count;

            for (var i = 0; i < numberOfMoves; i++)
            {
                var moveUse = Queue[0];
                moveUse.Apply();
                moveUseInfo.Add(moveUse);

                Queue.RemoveAt(0);
            }

            return moveUseInfo;
        }

        /// <summary>
        /// Sorts the move use queue into the order in which the moves should be applied.
        /// </summary>
        private void SortMoveUseQueue()
        {
            // characters with higher speed go first
            var sortedQueue = Queue.OrderByDescending(m => m.User.CurrentSpeed);

            Queue = sortedQueue.ToList();
        }
    }
}
