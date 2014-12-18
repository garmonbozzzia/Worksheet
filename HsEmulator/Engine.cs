using System.Collections.Generic;
using System.Linq;

namespace HsEmulator
{
    public class Engine
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public void Init()
        {
            Player1 = new Player(3, "Player1");
            Player2 = new Player(4, "Player2");
        }
    }

    public class Player
    {
        public List<Card> Deck { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> Board { get; set; }

        public Player(int numCards, string name)
        {
            Deck = Enumerable.Range(0, 30).Select(x => Card.Generate()).ToList();
            Hand = Deck.Take(3).ToList();
            Board = Enumerable.Repeat(new Card { Health = 30, Mana = 0, Attack = 0, Name = name }, 1).ToList();
            Deck.RemoveRange(0, 3);
        }
    }
}