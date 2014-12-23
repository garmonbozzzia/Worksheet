using System.Linq;

namespace HsEngine
{
    public class Card
    {
        //static Card Coin = Card.Parse("-1 0 0")

        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Hp { get; set; }

        public static Card Parse(string card)
        {
            var parse = card.Trim().Split('-').ToArray();
            return new Card
            {
                Mana = int.Parse(parse[0]),
                Attack = int.Parse(parse[1]),
                Hp = int.Parse(parse[2])
            };
        }

        public CardInstance Instance()
        {
            return new CardInstance
            {
                Card = this,
                AttackValue = Attack,
                Mana = Mana,
                Hp = Hp,
                Deathrattle = Effect.Nothing
            };
        }
    }
}