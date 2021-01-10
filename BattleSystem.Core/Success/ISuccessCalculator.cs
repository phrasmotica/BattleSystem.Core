namespace BattleSystem.Core.Success
{
    /// <summary>
    /// Interface for calculating whether an input task succeeds.
    /// </summary>
    public interface ISuccessCalculator<TTask, TResult>
    {
        /// <summary>
        /// Calculates how the given input task succeeds.
        /// </summary>
        /// <param name="input">The input task.</param>
        TResult Calculate(TTask input);
    }
}
