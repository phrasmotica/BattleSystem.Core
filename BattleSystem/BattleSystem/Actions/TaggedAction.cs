using System.Collections.Generic;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Represents an action associated with a set of tags.
    /// </summary>
    public class TaggedAction
    {
        /// <summary>
        /// Gets or sets the underlying action.
        /// </summary>
        public IAction Action { get; private set; }

        /// <summary>
        /// Gets or sets the set of tags for this action.
        /// </summary>
        public HashSet<string> Tags { get; private set; }

        /// <summary>
        /// Creates a new <see cref="TaggedAction"/> instance.
        /// </summary>
        public TaggedAction(IAction action, string[] tags)
        {
            Action = action;

            Tags = new HashSet<string>();
            foreach (var tag in tags)
            {
                Tags.Add(tag);
            }
        }

        /// <summary>
        /// Returns whether the action has the given tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public bool HasTag(string tag)
        {
            return Tags.Contains(tag);
        }
    }
}
