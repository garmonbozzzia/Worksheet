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
            Deck.ForEach(x => x.Owner = this);
            Hand = new List<CardInstance>();
            Board = new List<CardInstance>();
            Garbage = new List<CardInstance>();

            Hero = Card.Parse("0-0-30").Instance();
            Hero.Deathrattle = Lose();
            Hero.Owner = this;
        }

        public IEffect Lose()
        {
            return new Effect(_ => Opponent.Win().Apply()) {Type = "Lose"};
        }

        public IEffect Win()
        {
            return new Effect(_ => Effects.Instance().GameOver().Apply()) {Type = "Win"};
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
            return new Effect(apply) { Type = "DrawCard" };
        }

        public IEffect MoveFromDeckToHand(CardInstance card)
        {
            Move(card, Deck, Hand);
            return new Effect { Type = "MoveFromDeckToHand" };
        }

        public IEffect MoveFromHandToBoard(CardInstance card)
        {
            Move(card, Hand, Board);
            return new Effect { Type = "MoveFromDeckToHand" };
        }

        public IEffect MoveFromDeckToGarbage(CardInstance card)
        {
            Move(card, Deck, Garbage);
            return new Effect { Type = "MoveFromDeckToGarbage" };
        }

        public IEffect MoveFromBoardToGarbage(CardInstance card)
        {
            Move(card, Board, Garbage);
            return new Effect { Type = "MoveFromBoardToGarbage" };
        }

        private void Move(CardInstance card,  List<CardInstance> from, List<CardInstance> to )
        {
            from.Remove(card);
            to.Add(card);
        }

        public IEffect EndTurn()
        {
            return new Effect { Type = "EndTurn" };
        }

        public IEnumerable<IEffect> Actions()
        {
            return EndTurn().Apply();
        }
    }
}