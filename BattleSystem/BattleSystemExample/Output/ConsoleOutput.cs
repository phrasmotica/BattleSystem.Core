using System;

namespace BattleSystemExample.Output
{
    /// <summary>
    /// Class for game output via the console.
    /// </summary>
    public class ConsoleOutput : IGameOutput
    {
        /// <inheritdoc />
        public void WriteLine()
        {
            Console.WriteLine();
        }

        /// <inheritdoc />
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
