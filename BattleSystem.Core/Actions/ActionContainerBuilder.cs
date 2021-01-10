using static BattleSystem.Core.Actions.ActionContainer;

namespace BattleSystem.Core.Actions
{
    /// <summary>
    /// Builder class for action containers.
    /// </summary>
    public class ActionContainerBuilder
    {
        /// <summary>
        /// The action container to build.
        /// </summary>
        private readonly ActionContainer _container;

        /// <summary>
        /// Creates a new <see cref="ActionContainerBuilder"/> instance.
        /// </summary>
        public ActionContainerBuilder()
        {
            _container = new ActionContainer();
        }

        /// <summary>
        /// Adds the given attack value transform to the action container.
        /// </summary>
        /// <param name="transform">The attack value transform for the action container.</param>
        public ActionContainerBuilder WithAttackValueTransform(StatValueTransform transform)
        {
            _container.AddAttackValueTransform(transform);
            return this;
        }

        /// <summary>
        /// Adds the given defence value transform to the action container.
        /// </summary>
        /// <param name="transform">The defence value transform for the action container.</param>
        public ActionContainerBuilder WithDefenceValueTransform(StatValueTransform transform)
        {
            _container.AddDefenceValueTransform(transform);
            return this;
        }

        /// <summary>
        /// Adds the given speed value transform to the action container.
        /// </summary>
        /// <param name="transform">The speed value transform for the action container.</param>
        public ActionContainerBuilder WithSpeedValueTransform(StatValueTransform transform)
        {
            _container.AddSpeedValueTransform(transform);
            return this;
        }

        /// <summary>
        /// Adds a stats transform for increasing attack by the given factor to the action container.
        /// </summary>
        public ActionContainerBuilder WithIncreaseAttack(double factor = 0.1)
        {
            return WithAttackValueTransform(a => (int) (a * (1 + factor)));
        }

        /// <summary>
        /// Adds the given damage power transform to the action container.
        /// </summary>
        /// <param name="transform">The damage power transform for the action container.</param>
        public ActionContainerBuilder WithDamagePowerTransform(PowerTransform transform)
        {
            _container.AddDamagePowerTransform(transform);
            return this;
        }

        /// <summary>
        /// Adds a tagged action to the action container.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="tags">The tags.</param>
        public ActionContainerBuilder WithTaggedAction(IAction action, params string[] tags)
        {
            _container.AddTaggedAction(action, tags);
            return this;
        }

        /// <summary>
        /// Returns the built action container.
        /// </summary>
        public ActionContainer Build()
        {
            return _container;
        }
    }
}
