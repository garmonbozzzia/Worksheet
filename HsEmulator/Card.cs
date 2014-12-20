using System;

namespace HsEmulator
{
    public class HeroCard : Card
    {
        public HeroCard()
        {
            Health = 30;
            Mana = 0;
            AttackValue = 0;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", Name, Health);
        }
    }

    public class BoardCard : Card
    {
        public override string ToString()
        {
            return String.Format("A:{0} H:{1}", AttackValue, Health);
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
            var mana = Engine.RandomGen.Next(10);
            return new Card
            {
                Mana = mana,
                AttackValue = mana + Engine.RandomGen.Next(1),
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
