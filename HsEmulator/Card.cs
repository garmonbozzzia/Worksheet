using System;
using System.Collections.Generic;
using System.Linq;

namespace HsEmulator
{
    public static class CardCollection
    {
        public static IEnumerable<Card> Shuffle(this IEnumerable<Card> deck)
        {
            return deck.OrderBy(x=>Engine.Random.Next());
        }
    }

    public class Card
    {
        public int Mana { get; set; }
        public int AttackValue { get; set; }
        public int Health { get; set; }
        public string Name { get; set; }
        public static Card Coin = new Card {AttackValue = 0, Mana = -1, Health = -1, Name = "Coin"};

        public override string ToString()
        {
            return String.Format("M:{0}, A:{1}, H:{2}", Mana, AttackValue, Health);
        }

        public static Card Generate()
        {
            var mana = Engine.Random.Next(10);
            return new Card
            {
                Mana = mana,
                AttackValue = mana + Engine.Random.Next(1),
                Health = 1
            };
        }

        public static Card FixedMana(int mana)
        {
            return new Card
            {
                Mana = mana,
                AttackValue = mana + Engine.Random.Next(1),
                Health = 1
            };
        }

        public void Attack(Card card)
        {
            card.Health -= AttackValue;
            Health -= card.AttackValue;
        }
    }
}