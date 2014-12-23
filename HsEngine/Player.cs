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

        public Player Opponent { get; set; }

        public Player(IEnumerable<CardInstance> deck)
        {
            Deck = deck.ToList();
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