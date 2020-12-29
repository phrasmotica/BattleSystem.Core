using System;
using System.Linq;

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
                var input = Console.ReadLine()?.Trim();
                choiceIsValid = int.TryParse(input, out chosenIndex);

                if (!choiceIsValid)
                {
                    Console.WriteLine("Please enter a valid integer!");
                }
            }

            return chosenIndex;
        }

        /// <inheritdoc />
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        /// <inheritdoc />
        public string SelectChoice(string prompt = null, params string[] choices)
        {
            if (prompt is not null)
            {
                Console.WriteLine(prompt);
            }

            var choiceIsValid = false;
            var choice = string.Empty;
            choices = choices.Select(c => c.ToLower()).ToArray();

            while (!choiceIsValid)
            {
                choice = Console.ReadLine()?.Trim();
                choiceIsValid = choices.Contains(choice.ToLower());

                if (!choiceIsValid)
                {
                    Console.WriteLine("Please enter a valid choice!");
                }
            }

            return choice;
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
