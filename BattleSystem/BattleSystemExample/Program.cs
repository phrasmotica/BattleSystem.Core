using BattleSystem.Characters;
using BattleSystem.Damage;
using BattleSystem.Moves;
using BattleSystem.Stats;
using BattleSystemExample.Battles;
using BattleSystemExample.Characters;
using BattleSystemExample.Input;

namespace BattleSystemExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var gameOutput = new ConsoleOutput();

            gameOutput.WriteLine("Welcome to the Console Battle System!");

            var userStats = new StatSet
            {
                Attack = new Stat(5),
                Defence = new Stat(4),
                Speed = new Stat(4),
            };

            var userMoves = new MoveSet
            {
                Move1 = Attack.ByStatDifference("Sword Strike", 15, 20),
                Move2 = Buff.RaiseUserAttack("Sharpen", 10),
                Move3 = Heal.HealByPercentage("Restore", 10, 10),
            };

            var playerInput = new ConsoleInput();
            var user = new Player(playerInput, gameOutput, "Warrior", 100, userStats, userMoves);

            var enemyStats = new StatSet
            {
                Attack = new Stat(6),
                Defence = new Stat(3),
                Speed = new Stat(5),
            };

            var enemyMoves = new MoveSet
            {
                Move1 = Attack.ByStatDifference("Magic Missile", 15, 15),
                Move2 = Buff.RaiseUserAttack("Meditate", 15),
                Move3 = Heal.HealByPercentage("Refresh", 10, 30),
            };

            var enemy = new BasicCharacter("Mage", 100, enemyStats, enemyMoves);

            new Battle(new MoveProcessor(), gameOutput, user, enemy).Start();

            playerInput.Confirm("The battle is over! Press any key to continue.");
        }
    }
}
