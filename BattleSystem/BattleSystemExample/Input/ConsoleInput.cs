using System;

namespace BattleSystemExample.Input
{
    /// <summary>
    /// Class for user input via the console.
    /// </summary>
    public class ConsoleInput : IUserInput
    {
        /// <inheritdoc />
        public int SelectIndex()
        {
            var choiceIsValid = false;
            var chosenIndex = -1;

            while (!choiceIsValid)
            {
                var input = Console.ReadLine();
                choiceIsValid = int.TryParse(input, out chosenIndex);

                if (!choiceIsValid)
                {
                    Console.WriteLine("Please enter a valid integer!");
                }
            }

            return chosenIndex;
        }

        /// <inheritdoc />
        public void Confirm(string prompt = null)
        {
            if (prompt is not null)
            {
                Console.WriteLine(prompt);
            }

            Console.ReadKey();
        }
    }
}
