using System.Collections.Generic;
using System.Linq;

namespace HsEmulator
{
    public class Engine
    {
        public List<Card> Deck1 { get; set; }
        public List<Card> Deck2 { get; set; }
        public List<Card> Hand1 { get; set; }
        public List<Card> Hand2 { get; set; }
        public List<Card> Board1 { get; set; }
        public List<Card> Board2 { get; set; }

        public void Init()
        {
            Deck1 = Enumerable.Range(0, 30).Select(x => Card.Generate()).ToList();
            Deck2 = Enumerable.Range(0, 30).Select(x => Card.Generate()).ToList();

            Hand1 = Deck1.Take(3).ToList();
            Hand2 = Deck2.Take(4).ToList();

            Board1 = Enumerable.Repeat(new Card { Health = 30, Mana = 0, Attack = 0, Name = "Player1" }, 1).ToList();
            Board2 = Enumerable.Repeat(new Card { Health = 30, Mana = 0, Attack = 0, Name = "Player2" }, 1).ToList();

            Deck1.RemoveRange(0, 3);
            Deck2.RemoveRange(0, 4);
        }
    }
}