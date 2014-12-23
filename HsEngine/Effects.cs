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
                    .TakeWhileIncluding(x => x.Name != "GameOver"));
            return new Effect(apply){Name = "StartGame"};
        }

        public IEffect NextRound()
        {
            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect =>
                Turn(Player1)
                    .Next(Turn(Player2))
                    .SelectMany(x => x.Apply())
                );
            return new Effect(apply) { Name = "NextRound" };
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
                    .TakeWhileIncluding(y => y.Name != "EndTurn"));
            return new Effect(apply){Name = "Turn "+ player};
        }

        public IEffect GameOver()
        {
            return new Effect { Name = "GameOver" };
        }

        public IEffect RestoreMana(Player player)
        {
            return new Effect{Name = "RestoreMana"};
        }

        public IEffect DrawCard(Player player)
        {
            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect => 
                //player.Deck.Any() ? 
                    //player.Hand.Count() < 10 ?
                        //AddCardToHand(player, player.Deck.First()):
                        //AddCardFromDeckToGarbage(player, player.Deck.First()):
                    //DamageFromFatigue
                null
            );
            return new Effect { Name = "DrawCard" };
        }

        public IEffect BattleCry()
        {
            return new Effect { Name = "DrawCard" };
        }

        public IEffect GetDamage(CardInstance card, int val)
        {
            card.Hp -= val;
            if (card.Hp <= 0) Death(card);
                
            return new Effect { Name = "GetDamage" };
        }

        public IEffect Death(CardInstance card)
        {
            //MoveToGarbage
            //card.Deathrattle
            return new Effect { Name = "Death" };
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
            return 
                Engine.RandomGen.Next(5) == 0 ? GameOver() : 
                new Effect { Name = "EndTurn" };
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
            return new Effect { Name = "PlayerActions" };
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