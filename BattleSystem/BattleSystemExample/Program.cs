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

            var userStats = new StatSet
            {
                Attack = new Stat(5),
                Defence = new Stat(4),
                Speed = new Stat(4),
            };

            var userMoves = new MoveSetBuilder()
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
                                                .UserSelectsSingleTarget(playerInput, gameOutput)
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
                                                .UserSelectsSingleTarget(playerInput, gameOutput)
                                                .Build()
                                        )
                                        .WithAction(Buff.RaiseUserAttack())
                                        .Build()
                                )
                                .WithMove(
                                    new MoveBuilder()
                                        .Name("Harden")
                                        .Describe("The user dons additional armour to raise their Defense stat.")
                                        .WithMaxUses(10)
                                        .AlwaysSucceeds()
                                        .WithAction(Buff.RaiseUserDefence())
                                        .Build()
                                )
                                .WithMove(
                                    new MoveBuilder()
                                        .Name("Restore")
                                        .Describe("The user drinks a potion to restore 20 health.")
                                        .WithMaxUses(10)
                                        .AlwaysSucceeds()
                                        .WithAction(Heal.ByAbsoluteAmount(20))
                                        .Build()
                                )
                                .Build();

            var user = new Player(playerInput, gameOutput, "Warrior", 100, userStats, userMoves);

            var enemyStats = new StatSet
            {
                Attack = new Stat(6),
                Defence = new Stat(3),
                Speed = new Stat(5),
            };

            var enemyMoves = new MoveSetBuilder()
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
                                        .WithAction(Buff.RaiseUserAttack())
                                        .Build()
                                )
                                .WithMove(
                                    new MoveBuilder()
                                        .Name("Refresh")
                                        .Describe("The user regenerates 30% of their max health.")
                                        .WithMaxUses(10)
                                        .AlwaysSucceeds()
                                        .WithAction(Heal.ByPercentage(30))
                                        .Build()
                                )
                                .Build();

            var enemy = new BasicCharacter("Mage", 100, enemyStats, enemyMoves);

            new Battle(new MoveProcessor(), gameOutput, user, enemy).Start();

            playerInput.Confirm("The battle is over! Press any key to continue.");
        }
    }
}
