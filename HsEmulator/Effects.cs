using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HsEmulator
{
    public class Effects : IEffects
    {
        public IEffect StartGame()
        {
            var player1 = new object();
            var player2 = new object();

            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect => 
                //Turn(player1, player2).Cons( Turn(player2, player1).ListWrap())
                Turn(player1, player2)
                .Next(Turn(player2, player1))
                .Repeat(10)
                .SelectMany(x => x.Apply()
                    .TakeWhileIncluding(y => y.Name != "GameOver")));
            return new Effect(apply){Name = "StartGame"};
        }

        //todo remove call apply through select
        //todo check Repeat function
        public IEffect Turn(object player, object opponent)
        {
            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect =>
                RestoreMana(player).Next(
                    DrawCard(player)).Next(
                        //(player.Do(possibilities).Repeat)
                        EndTurn())
                    .SelectMany(x => x.Apply()
                        .TakeWhileIncluding(y => y.Name != "EndTurn")));
            return new Effect(apply){Name = "Turn"};
        }

        public IEffect GameOver()
        {
            return new Effect { Name = "GameOver" };
        }

        public IEffect RestoreMana(object player)
        {
            return new Effect{Name = "RestoreMana"};
        }

        public IEffect DrawCard(object player)
        {
            return new Effect { Name = "DrawCard" };
        }

        public IEffect BattleCry()
        {
            return new Effect { Name = "DrawCard" };
        }

        public IEffect GetDamage()
        {
            return new Effect { Name = "GetDamage" };
        }

        public IEffect Deathrattle()
        {
            return new Effect { Name = "" };
        }

        public IEffect Buff()
        {
            return new Effect { Name = "" };
        }

        public IEffect Silence()
        {
            return new Effect { Name = "" };
        }

        public IEffect Win()
        {
            return new Effect { Name = "Win" };
        }

        public IEffect Lose()
        {
            return new Effect { Name = "Lose" };
        }

        public IEnumerable<IEffect> EndTurnL()
        {
            yield return EndTurn();
        }

        public IEffect EndTurn()
        {
            Console.WriteLine("EndTurn calling");
            return Engine.RandomGen.Next(5) == 0 ? GameOver() 
                : new Effect { Name = "EndTurn" };
        }

        public IEffect PlayCard()
        {
            throw new NotImplementedException();
        }

        public IEffect Attack()
        {
            throw new NotImplementedException();
        }

        public IEffect PlaceCardOnHand()
        {
            return new Effect { Name = "" };
        }

        public IEffect PlaceCardOnBoard()
        {
            return new Effect { Name = "" };
        }

        public IEffect BecomeSleep()
        {
            return new Effect { Name = "" };
        }

        public IEffect BecomeActive()
        {
            return new Effect { Name = "" };
        }

        public IEffect RemoveFromBoard()
        {
            return new Effect { Name = "" };
        }
    }
}