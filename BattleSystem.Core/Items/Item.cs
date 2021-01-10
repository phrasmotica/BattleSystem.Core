using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Actions;
using BattleSystem.Core.Stats;
using static BattleSystem.Core.Actions.Damage.Calculators.BasePowerDamageCalculator;

namespace BattleSystem.Core.Items
{
    /// <summary>
    /// Represents an item that a character holds in battle.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Delegate for a function that transforms the given value of some stat.
        /// </summary>
        /// <param name="statValue">The stat value.</param>
        public delegate int StatValueTransform(int statValue);

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the description of the item.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets or sets this item's tagged actions.
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
        /// Creates a new <see cref="Item"/> instance.
        /// </summary>
        public Item()
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
        /// Sets the name for this item.
        /// </summary>
        /// <param name="name">The name.</param>
        public void SetName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Sets the description for this item.
        /// </summary>
        /// <param name="description">The description.</param>
        public void SetDescription(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Adds a tagged action for this item.
        /// </summary>
        /// <param name="action">The underlying action to add.</param>
        /// <param name="tags">The tags.</param>
        public void AddTaggedAction(IAction action, params string[] tags)
        {
            var taggedAction = new TaggedAction(action, tags);
            TaggedActions.Add(taggedAction);
        }

        /// <summary>
        /// Adds an attack value transform function for this item.
        /// </summary>
        /// <param name="transform">The attack value transform function to add.</param>
        public void AddAttackValueTransform(StatValueTransform transform)
        {
            StatValueTransforms[StatCategory.Attack].Add(transform);
        }

        /// <summary>
        /// Adds an defence value transform function for this item.
        /// </summary>
        /// <param name="transform">The defence value transform function to add.</param>
        public void AddDefenceValueTransform(StatValueTransform transform)
        {
            StatValueTransforms[StatCategory.Defence].Add(transform);
        }

        /// <summary>
        /// Adds an speed balue transform function for this item.
        /// </summary>
        /// <param name="transform">The speed base value transform function to add.</param>
        public void AddSpeedValueTransform(StatValueTransform transform)
        {
            StatValueTransforms[StatCategory.Speed].Add(transform);
        }

        /// <summary>
        /// Adds an attack power transform function for this item.
        /// </summary>
        /// <param name="transform">The attack power transform function to add.</param>
        public void AddDamagePowerTransform(PowerTransform transform)
        {
            DamagePowerTransforms.Add(transform);
        }

        /// <summary>
        /// Returns the tagged character actions with the given tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public IEnumerable<TaggedAction> GetCharacterTaggedActions(string tag)
        {
            return TaggedActions.Where(a => a.HasTag(tag));
        }
    }
}
