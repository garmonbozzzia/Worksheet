using System.Collections.Generic;
using System.Linq;

namespace HsEmulator
{
    public static class CardCollection
    {
        public static IEnumerable<Card> Shuffle(this IEnumerable<Card> deck)
        {
            return deck.OrderBy(x => Engine.RandomGen.Next());                
        }

        public static IEnumerable<Card> ConstDeck(string card)
        {
            return Enumerable.Range(0, 30).Select(x => CardCollection.Parse(card));
        }

        public static IEnumerable<Card> MixedDeck( IEnumerable<Card> cards, IEnumerable<int> distribution )
        {
            return cards.Zip(distribution, Enumerable.Repeat).Concat();
        }

        public static IEnumerable<Card> MixedDeck(string cards, string distribution)
        {
            return cards.Trim().Split(' ').Zip(
                distribution.Trim().Split(' ').Select(int.Parse),
                (c, n) => Enumerable.Range(0, n).Select(_ => Parse(c))
                ).Concat();
        }

        public static Card Parse(string card)
        {
            var parse = card.Trim().Split('-').ToArray();
            return new Card
            {
                Mana = int.Parse(parse[0]),
                AttackValue = int.Parse(parse[1]),
                Health = int.Parse(parse[2])
            };
        }
    }
}