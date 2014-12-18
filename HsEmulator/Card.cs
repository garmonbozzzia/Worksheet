using System;

namespace HsEmulator
{
    public class Card
    {
        public int Mana { get; set; }
        public int AttackValue { get; set; }
        public int Health { get; set; }
        public string Name { get; set; }

        public static Random Random = new Random();

        public override string ToString()
        {
            return String.Format("M:{0}, A:{1}, H:{2}", Mana, AttackValue, Health);
        }

        public static Card Generate()
        {
            var mana = Random.Next(10);
            return new Card
            {
                Mana = mana,
                AttackValue = Random.Next(mana),
                Health = Random.Next(mana) + 1
            };
        }

        public void Attack(Card card)
        {
            card.Health -= AttackValue;
            Health -= card.AttackValue;
        }
    }
}