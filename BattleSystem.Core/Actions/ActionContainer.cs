using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Stats;

namespace BattleSystem.Core.Actions
{
    /// <summary>
    /// Container for sets of actions that can be attached to entities in a
    /// battle.
    /// </summary>
    public class ActionContainer
    {
        /// <summary>
        /// Delegate for a function that transforms the given power.
        /// </summary>
        /// <param name="power">The power.</param>
        public delegate int PowerTransform(int power);

        /// <summary>
        /// Delegate for a function that transforms the given value of some stat.
        /// </summary>
        /// <param name="statValue">The stat value.</param>
        public delegate int StatValueTransform(int statValue);

        /// <summary>
        /// Creates a new <see cref="ActionContainer"/> instance.
        /// </summary>
        public ActionContainer()
        {
            TaggedActions = new List<TaggedAction>();

            StatValueTransforms = new Dictionary<StatCategory, List<StatValueTransform>>
            {
                [StatCategory.Attack] = new List<StatValueTransform>(),
                [StatCategory.Defence] = new List<StatValueTransform>(),
                [StatCategory.Speed] = new List<StatValueTransform>(),
            };

            DamagePowerTransforms = new List<PowerTransform>();
        }

        /// <summary>
        /// Gets or sets the tagged actions.
        /// </summary>
        public List<TaggedAction> TaggedActions { get; private set; }

        /// <summary>
        /// Gets or sets the map of stat value transform functions.
        /// </summary>
        public IDictionary<StatCategory, List<StatValueTransform>> StatValueTransforms { get; private set; }

        /// <summary>
        /// Gets or sets the damage power transform functions.
        /// </summary>
        public List<PowerTransform> DamagePowerTransforms { get; private set; }

        /// <summary>
        /// Adds a tagged action.
        /// </summary>
        /// <param name="action">The underlying action to add.</param>
        /// <param name="tags">The tags.</param>
        public void AddTaggedAction(IAction action, params string[] tags)
        {
            var taggedAction = new TaggedAction(action, tags);
            TaggedActions.Add(taggedAction);
        }

        /// <summary>
        /// Adds an attack value transform function.
        /// </summary>
        /// <param name="transform">The attack value transform function to add.</param>
        public void AddAttackValueTransform(StatValueTransform transform)
        {
            StatValueTransforms[StatCategory.Attack].Add(transform);
        }

        /// <summary>
        /// Adds an defence value transform function.
        /// </summary>
        /// <param name="transform">The defence value transform function to add.</param>
        public void AddDefenceValueTransform(StatValueTransform transform)
        {
            StatValueTransforms[StatCategory.Defence].Add(transform);
        }

        /// <summary>
        /// Adds an speed balue transform function.
        /// </summary>
        /// <param name="transform">The speed base value transform function to add.</param>
        public void AddSpeedValueTransform(StatValueTransform transform)
        {
            StatValueTransforms[StatCategory.Speed].Add(transform);
        }

        /// <summary>
        /// Adds an attack power transform function.
        /// </summary>
        /// <param name="transform">The attack power transform function to add.</param>
        public void AddDamagePowerTransform(PowerTransform transform)
        {
            DamagePowerTransforms.Add(transform);
        }

        /// <summary>
        /// Returns the tagged actions with the given tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public IEnumerable<TaggedAction> GetTaggedActions(string tag)
        {
            return TaggedActions.Where(a => a.HasTag(tag));
        }
    }
}
