﻿using BattleSystem.Characters;
using BattleSystem.Moves;
using BattleSystem.Moves.Actions;
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

            var userMoves = new MoveSet();

            userMoves.AddMove(
                new MoveBuilder()
                    .Name("Sword Strike")
                    .Describe("The user swings their sword to inflict damage.")
                    .WithMaxUses(15)
                    .WithAction(Attack.ByStatDifference(20))
                    .Build()
            );

            userMoves.AddMove(
                new MoveBuilder()
                    .Name("Pierce")
                    .Describe("The user drives their weapon through the target's abdomen, and then raises their Attack stat.")
                    .WithMaxUses(5)
                    .WithAction(Attack.ByPercentage(40))
                    .WithAction(Buff.RaiseUserAttack())
                    .Build()
            );

            userMoves.AddMove(
                new MoveBuilder()
                    .Name("Sharpen")
                    .Describe("The user dons additional armour to raise their Defense stat.")
                    .WithMaxUses(10)
                    .WithAction(Buff.RaiseUserDefence())
                    .Build()
            );

            userMoves.AddMove(
                new MoveBuilder()
                    .Name("Restore")
                    .Describe("The user drinks a potion to restore 20 health.")
                    .WithMaxUses(10)
                    .WithAction(Heal.ByAbsoluteAmount(20))
                    .Build()
            );

            var playerInput = new ConsoleInput();
            var user = new Player(playerInput, gameOutput, "Warrior", 100, userStats, userMoves);

            var enemyStats = new StatSet
            {
                Attack = new Stat(6),
                Defence = new Stat(3),
                Speed = new Stat(5),
            };

            var enemyMoves = new MoveSet();

            enemyMoves.AddMove(
                new MoveBuilder()
                    .Name("Magic Missile")
                    .Describe("The user fires a spectral missile to inflict 20 damage.")
                    .WithMaxUses(15)
                    .WithAction(Attack.ByAbsolutePower(20))
                    .Build()
            );

            enemyMoves.AddMove(
                new MoveBuilder()
                    .Name("Lightning Bolt")
                    .Describe("The user summons a lightning strike to deal damage equal to 30% of the target's health.")
                    .WithMaxUses(5)
                    .WithAction(Attack.ByPercentage(30))
                    .Build()
            );

            enemyMoves.AddMove(
                new MoveBuilder()
                    .Name("Meditate")
                    .Describe("The user finds inner calm to raise their Attack stat.")
                    .WithMaxUses(15)
                    .WithAction(Buff.RaiseUserAttack())
                    .Build()
            );

            enemyMoves.AddMove(
                new MoveBuilder()
                    .Name("Refresh")
                    .Describe("The user regenerates 30% of their max health.")
                    .WithMaxUses(10)
                    .WithAction(Heal.ByPercentage(30))
                    .Build()
            );

            var enemy = new BasicCharacter("Mage", 100, enemyStats, enemyMoves);

            new Battle(new MoveProcessor(), gameOutput, user, enemy).Start();

            playerInput.Confirm("The battle is over! Press any key to continue.");
        }
    }
}
