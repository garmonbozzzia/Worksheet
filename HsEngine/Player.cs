using System;
using System.Collections.Generic;
using System.Linq;

namespace HsEngine
{
    public class Player
    {
        public List<CardInstance> Deck;
        public List<CardInstance> Hand;
        public List<CardInstance> Board;
        public List<CardInstance> Garbage;

        public CardInstance Hero { get; set; }
        public int Fatigue { get; set; }
        

        public Player Opponent { get; set; }

        public Player(IEnumerable<CardInstance> deck)
        {
            Deck = deck.ToList();

        }

        public IEffect DrawCard()
        {
            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect =>
                Deck.Any()
                    ? Hand.Count() < 10
                        ? MoveFromDeckToHand(Deck.First()).Apply()
                        : MoveFromDeckToGarbage(Deck.First()).Apply()
                    : Hero.GetDamage(Fatigue++).Apply()
                );
            return new Effect(apply) { Name = "DrawCard" };
        }

        public IEffect MoveFromDeckToHand(CardInstance card)
        {
            Move(card, Board, Garbage);
            return new Effect { Name = "MoveFromDeckToHand" };
        }

        public IEffect MoveFromHandToBoard(CardInstance card)
        {
            Move(card, Board, Garbage);
            return new Effect { Name = "MoveFromDeckToHand" };
        }

        public IEffect MoveFromDeckToGarbage(CardInstance card)
        {
            Move(card, Board, Garbage);
            return new Effect { Name = "MoveFromDeckToGarbage" };
        }

        public IEffect MoveFromBoardToGarbage(CardInstance card)
        {
            Move(card, Board, Garbage);
            return new Effect { Name = "MoveFromBoardToGarbage" };
        }

        private void Move(CardInstance card,  List<CardInstance> from, List<CardInstance> to )
        {
            from.Remove(card);
            to.Add(card);
        }
    }
}