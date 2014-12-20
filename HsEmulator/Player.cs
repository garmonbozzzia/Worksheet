using System.Collections.Generic;
using System.Linq;

namespace HsEmulator
{
    public class Player : HeroCard
    {
        public static int DeckCardNumber = 30;

        public List<Card> Deck { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> Board { get; set; }

        public Mana Mana { get; set; }

        public static int FatigueValue { get; set; }

        public Player(int numCards, string name, IEnumerable<Card> deck)
        {
            FatigueValue = 1;
            Mana = new Mana {Limit = 0, Current = 0};
            Deck = deck.ToList();
            Hand = Deck.Take(numCards).ToList();
            Board = Enumerable.Repeat(this, 1).Cast<Card>().ToList();
            Deck.RemoveRange(0, numCards);
            Name = name;
        }
    }
}
