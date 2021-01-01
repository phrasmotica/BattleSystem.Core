using System.Collections.Generic;
using BattleSystem.Actions;
using BattleSystem.Characters;
using BattleSystem.Items;
using BattleSystem.Moves;
using BattleSystem.Stats;
using BattleSystemExample.Battles;
using BattleSystemExample.Characters;
using BattleSystemExample.Constants;
using BattleSystemExample.Extensions;
using BattleSystemExample.Input;
using BattleSystemExample.Output;

namespace BattleSystemExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var playerInput = new ConsoleInput();
            var gameOutput = new ConsoleOutput();

            gameOutput.WriteLine("Welcome to the Console Battle System!");

            var playAgain = true;
            while (playAgain)
            {
                new Battle(
                    new MoveProcessor(),
                    gameOutput,
                    CreateCharacters(playerInput, gameOutput)).Start();

                var playAgainChoice = playerInput.SelectChoice("Play again? [y/n]", "y", "n");
                playAgain = playAgainChoice == "y";
            }

            playerInput.Confirm("Thanks for playing! Press any key to exit.");
        }

        /// <summary>
        /// Creates characters for the game.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <param name="gameOutput">The game output</param>
        private static IEnumerable<Character> CreateCharacters(IUserInput userInput, IGameOutput gameOutput)
        {
            var playerStats = new StatSet
            {
                Attack = new Stat(5),
                Defence = new Stat(4),
                Speed = new Stat(4),
            };

            var playerMoves =
                new MoveSetBuilder()
                    .WithMove(
                        new MoveBuilder()
                            .Name("Sword Strike")
                            .Describe("The user swings their sword to inflict damage. This move has increased priority.")
                            .WithMaxUses(15)
                            .WithPriority(1)
                            .WithAccuracy(100)
                            .WithAction(
                                new AttackBuilder()
                                    .WithPower(20)
                                    .StatDifferenceDamage()
                                    .UserSelectsSingleEnemy(userInput, gameOutput)
                                    .Build()
                            )
                            .Build()
                    )
                    .WithMove(
                        new MoveBuilder()
                            .Name("Pierce")
                            .Describe("The user drives their weapon through the target's abdomen, and then raises their Attack stat.")
                            .WithMaxUses(5)
                            .WithAccuracy(50)
                            .WithAction(
                                new AttackBuilder()
                                    .WithPower(40)
                                    .PercentageDamage()
                                    .UserSelectsSingleEnemy(userInput, gameOutput)
                                    .Build()
                            )
                            .WithAction(
                                new BuffBuilder()
                                    .TargetsUser()
                                    .WithRaiseAttack(0.1)
                                    .Build()
                            )
                            .Build()
                    )
                    .WithMove(
                        new MoveBuilder()
                            .Name("Threaten")
                            .Describe("The user raises the Attack stat of its ally characters and lowers the Defence stat of a single enemy.")
                            .WithMaxUses(5)
                            .AlwaysSucceeds()
                            .WithAction(
                                new BuffBuilder()
                                    .TargetsAllies()
                                    .WithRaiseAttack(0.1)
                                    .Build()
                            )
                            .WithAction(
                                new BuffBuilder()
                                    .UserSelectsSingleEnemy(userInput, gameOutput)
                                    .WithLowerDefence(0.1)
                                    .Build()
                            )
                            .Build()
                    )
                    .WithMove(
                        new MoveBuilder()
                            .Name("Restore")
                            .Describe("The user drinks a potion to restore 20 health, while also increasing their protect limit by one.")
                            .WithMaxUses(10)
                            .AlwaysSucceeds()
                            .WithAction(
                                new HealBuilder()
                                    .WithAmount(20)
                                    .AbsoluteHealing()
                                    .TargetsUser()
                                    .Build()
                            )
                            .WithAction(
                                new ProtectLimitChangeBuilder()
                                    .WithAmount(1)
                                    .TargetsUser()
                                    .Build()
                            )
                            .Build()
                    )
                    .Build();

            var player = new Player(
                userInput,
                gameOutput,
                "Warrior",
                "a",
                100,
                playerStats,
                playerMoves);

            player.EquipItem(
                new ItemBuilder()
                    .Name("Might Relic")
                    .Describe("Increases the holder's Attack by 5% at the end of each turn.")
                    .WithEndTurnAction(
                        new BuffBuilder()
                            .TargetsUser()
                            .WithRaiseAttack(0.05)
                            .Build()
                    )
                    .Build()
            );

            var bardStats = new StatSet
            {
                Attack = new Stat(4),
                Defence = new Stat(3),
                Speed = new Stat(4),
            };

            var bardMoves =
                new MoveSetBuilder()
                    .WithMove(
                        new MoveBuilder()
                            .Name("Play Music")
                            .Describe("The user shreds on their guitar to inflict 5 damage on all enemies.")
                            .WithMaxUses(25)
                            .WithAccuracy(100)
                            .WithAction(
                                new AttackBuilder()
                                    .WithPower(5)
                                    .AbsoluteDamage()
                                    .TargetsEnemies()
                                    .Build()
                            )
                            .Build()
                    )
                    .Build();

            var bard = new BasicCharacter("Bard", "a", 100, bardStats, bardMoves);

            bard.EquipItem(
                new ItemBuilder()
                    .Name("Capo")
                    .Describe("Makes the holder's music better, increasing the power of their attacks by 1.")
                    .WithAttackPowerTransform(p => p + 1)
                    .Build()
            );

            var mageStats = new StatSet
            {
                Attack = new Stat(6),
                Defence = new Stat(3),
                Speed = new Stat(5),
            };

            var mageMoves =
                new MoveSetBuilder()
                    .WithMove(
                        new MoveBuilder()
                            .Name("Magic Missile")
                            .Describe("The user fires a spectral missile to inflict 20 damage.")
                            .WithMaxUses(15)
                            .WithAccuracy(100)
                            .WithAction(
                                new AttackBuilder()
                                    .WithPower(20)
                                    .AbsoluteDamage()
                                    .TargetsFirstEnemy()
                                    .Build()
                            )
                            .Build()
                    )
                    .WithMove(
                        new MoveBuilder()
                            .Name("Lightning Bolt")
                            .Describe("The user summons a lightning strike to deal damage equal to 30% of the target's health.")
                            .WithMaxUses(5)
                            .WithAccuracy(70)
                            .WithAction(
                                new AttackBuilder()
                                    .WithPower(30)
                                    .PercentageDamage()
                                    .TargetsFirstEnemy()
                                    .Build()
                            )
                            .Build()
                    )
                    .WithMove(
                        new MoveBuilder()
                            .Name("Meditate")
                            .Describe("The user finds inner calm to raise the Defence stat of all characters on their team.")
                            .WithMaxUses(10)
                            .AlwaysSucceeds()
                            .WithAction(
                                new BuffBuilder()
                                    .TargetsTeam()
                                    .WithRaiseDefence(0.1)
                                    .Build()
                            )
                            .Build()
                    )
                    .WithMove(
                        new MoveBuilder()
                            .Name("Refresh")
                            .Describe("The user regenerates 30% of their max health.")
                            .WithMaxUses(10)
                            .AlwaysSucceeds()
                            .WithAction(
                                new HealBuilder()
                                    .WithAmount(30)
                                    .PercentageHealing()
                                    .TargetsUser()
                                    .Build()
                            )
                            .Build()
                    )
                    .Build();

            var mage = new BasicCharacter("Mage", "b", 100, mageStats, mageMoves);

            var rogueStats = new StatSet
            {
                Attack = new Stat(3),
                Defence = new Stat(2),
                Speed = new Stat(3),
            };

            var rogueMoves =
                new MoveSetBuilder()
                    .WithMove(
                        new MoveBuilder()
                            .Name("Protect")
                            .Describe("The user protects themself from the next attack.")
                            .WithMaxUses(5)
                            .WithPriority(2)
                            .AlwaysSucceeds()
                            .WithAction(
                                new ProtectBuilder()
                                    .TargetsFirstAlly()
                                    .Build()
                            )
                            .Build()
                    )
                    .Build();

            var rogue = new BasicCharacter("Rogue", "b", 80, rogueStats, rogueMoves);

            return new Character[] { player, bard, mage, rogue };
        }
    }
}
