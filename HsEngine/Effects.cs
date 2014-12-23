using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using HsEmulator;

namespace HsEngine
{
    public class Effects : IEffects
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public IEffect StartGame()
        {
            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect =>
                //Turn(player1, player2).Cons( Turn(player2, player1).ListWrap())
                //DrawCard(Player1)
                1.To(3).Select(_ => DrawCard(Player1))
                    .Next(1.To(4).Select(_ => DrawCard(Player2)))
                    .Next(1.To().Select(_ => NextRound()))
                    .SelectMany(x => x)
                    //.Next(AddCoin(Player2))
                    .SelectMany(x => x.Apply())
                    .TakeWhileIncluding(x => x.Type != "GameOver"));
            return new Effect(apply){Type = "StartGame"};
        }

        public IEffect NextRound()
        {
            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect =>
                Turn(Player1)
                    .Next(Turn(Player2))
                    .SelectMany(x => x.Apply())
                );
            return new Effect(apply) { Type = "NextRound" };
        }

        //todo remove call apply through select
        //todo check Repeat function
        public IEffect Turn(Player player)
        {
            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect =>
                RestoreMana(player)
                    .Next(DrawCard(player))
                    .Next(PlayerActions())
                    .Next(EndTurn())
                    .SelectMany(x => x.Apply())
                    .TakeWhileIncluding(y => y.Type != "EndTurn"));
            return new Effect(apply){Type = "Turn "+ player};
        }

        public IEffect GameOver()
        {
            return new Effect { Type = "GameOver" };
        }

        public IEffect RestoreMana(Player player)
        {
            return new Effect{Type = "RestoreMana"};
        }

        public IEffect DrawCard(Player player)
        {
            return player.DrawCard();
        }

        public IEffect BattleCry()
        {
            return new Effect { Type = "DrawCard" };
        }

        public IEffect GetDamage(CardInstance card, int val)
        {
            card.Hp -= val;
            if (card.Hp <= 0) Death(card);
                
            return new Effect { Type = "GetDamage" };
        }

        public IEffect Death(CardInstance card)
        {
            //MoveToGarbage
            //card.Deathrattle
            return new Effect { Type = "Death" };
        }

        public IEffect Deathrattle()
        {
            return new Effect { Type = "" };
        }

        public IEffect Buff()
        {
            return new Effect { Type = "" };
        }

        public IEffect Silence()
        {
            return new Effect { Type = "" };
        }

        public IEffect Win()
        {
            return new Effect { Type = "Win" };
        }

        public IEffect Lose()
        {
            return new Effect { Type = "Lose" };
        }

        public IEnumerable<IEffect> EndTurnL()
        {
            yield return EndTurn();
        }

        public IEffect EndTurn()
        {
            Console.WriteLine("EndTurn calling");
            return 
                Engine.RandomGen.Next(5) == 0 ? GameOver() : 
                new Effect { Type = "EndTurn" };
        }

        public IEffect PlayCard()
        {
            throw new NotImplementedException();
        }

        public IEffect Attack(CardInstance attacker, CardInstance target)
        {
            return attacker.Attack(target);
        }

        public IEffect PlayerActions()
        {
            return new Effect { Type = "PlayerActions" };
        }

        public IEffect PlaceCardOnHand()
        {
            return new Effect { Type = "" };
        }

        public IEffect PlaceCardOnBoard()
        {
            return new Effect { Type = "" };
        }

        public IEffect BecomeSleep()
        {
            return new Effect { Type = "" };
        }

        public IEffect BecomeActive()
        {
            return new Effect { Type = "" };
        }

        public IEffect RemoveFromBoard()
        {
            return new Effect { Type = "" };
        }
    }
}