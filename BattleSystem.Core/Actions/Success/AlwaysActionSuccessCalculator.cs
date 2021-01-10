using BattleSystem.Core.Success;

namespace BattleSystem.Core.Actions.Success
{
    /// <summary>
    /// Calculates an action to always succeed.
    /// </summary>
    public class AlwaysActionSuccessCalculator : ISuccessCalculator<IAction, bool>
    {
        /// <inheritdoc />
        public bool Calculate(IAction input)
        {
            return true;
        }
    }
}
