using BattleSystem.Characters;
using BattleSystem.Moves;
using BattleSystem.Moves.Actions;
using BattleSystem.Stats;
using BattleSystemExample.Battles;
using BattleSystemExample.Characters;
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

            var playerStats = new StatSet
            {
                Attack = new Stat(5),
                Defence = new Stat(4),
                Speed = new Stat(4),
            };

            var playerMoves = new MoveSetBuilder()
                                .WithMove(
                                    new MoveBuilder()
                                        .Name("Sword Strike")
                                        .Describe("The user swings their sword to inflict damage.")
                                        .WithMaxUses(15)
                                        .WithAccuracy(100)
                                        .WithAction(
                                            new AttackBuilder()
                                                .WithPower(20)
                                                .StatDifferenceDamage()
                                                .UserSelectsSingleOtherTarget(playerInput, gameOutput)
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
                                                .UserSelectsSingleOtherTarget(playerInput, gameOutput)
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
                                        .Describe("The user intimidates the target to lower their Defence stat while also raising their own.")
                                        .WithMaxUses(5)
                                        .AlwaysSucceeds()
                                        .WithAction(
                                            new BuffBuilder()
                                                .TargetsUser()
                                                .WithRaiseDefence(0.1)
                                                .Build()
                                        )
                                        .WithAction(
                                            new BuffBuilder()
                                                .UserSelectsSingleOtherTarget(playerInput, gameOutput)
                                                .WithLowerDefence(0.1)
                                                .Build()
                                        )
                                        .Build()
                                )
                                .WithMove(
                                    new MoveBuilder()
                                        .Name("Restore")
                                        .Describe("The user drinks a potion to restore 20 health.")
                                        .WithMaxUses(10)
                                        .AlwaysSucceeds()
                                        .WithAction(
                                            new HealBuilder()
                                                .WithAmount(20)
                                                .AbsoluteHealing()
                                                .TargetsUser()
                                                .Build()
                                        )
                                        .Build()
                                )
                                .Build();

            var player = new Player(
                playerInput,
                gameOutput,
                "Warrior",
                "a",
                100,
                playerStats,
                playerMoves);

            var bardStats = new StatSet
            {
                Attack = new Stat(4),
                Defence = new Stat(3),
                Speed = new Stat(4),
            };

            var bardMoves = new MoveSetBuilder()
                                .WithMove(
                                    new MoveBuilder()
                                        .Name("Play Music")
                                        .Describe("The user shreds on their guitar to inflict 5 damage.")
                                        .WithMaxUses(25)
                                        .WithAccuracy(100)
                                        .WithAction(
                                            new AttackBuilder()
                                                .WithPower(5)
                                                .AbsoluteDamage()
                                                .TargetsFirst()
                                                .Build()
                                        )
                                        .Build()
                                )
                                .Build();

            var bard = new BasicCharacter("Bard", "a", 100, bardStats, bardMoves);

            var mageStats = new StatSet
            {
                Attack = new Stat(6),
                Defence = new Stat(3),
                Speed = new Stat(5),
            };

            var mageMoves = new MoveSetBuilder()
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
                                                .TargetsFirst()
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
                                                .TargetsFirst()
                                                .Build()
                                        )
                                        .Build()
                                )
                                .WithMove(
                                    new MoveBuilder()
                                        .Name("Meditate")
                                        .Describe("The user finds inner calm to raise their Attack stat.")
                                        .WithMaxUses(15)
                                        .AlwaysSucceeds()
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

            var rogueMoves = new MoveSetBuilder()
                                .WithMove(
                                    new MoveBuilder()
                                        .Name("Stab")
                                        .Describe("The user stabs the foe with a short knife.")
                                        .WithMaxUses(10)
                                        .WithAccuracy(100)
                                        .WithAction(
                                            new AttackBuilder()
                                                .WithPower(10)
                                                .StatDifferenceDamage()
                                                .TargetsFirst()
                                                .Build()
                                        )
                                        .Build()
                                )
                                .Build();

            var rogue = new BasicCharacter("Rogue", "b", 80, rogueStats, rogueMoves);

            new Battle(
                new MoveProcessor(),
                gameOutput,
                new Character[] { player, bard },
                new[] { mage, rogue }).Start();

            playerInput.Confirm("The battle is over! Press any key to continue.");
        }
    }
}
