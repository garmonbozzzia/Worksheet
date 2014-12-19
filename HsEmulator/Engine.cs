using System;
using System.Linq;

namespace HsEmulator
{
    public class Engine
    {
        public static class RandomGen
        {
            private static readonly Random Global = new Random();
            [ThreadStatic]
            private static Random _local;

            public static int Next()
            {
                Random inst = _local;
                if (inst == null)
                {
                    int seed;
                    lock (Global) seed = Global.Next();
                    _local = inst = new Random(seed);
                }
                return inst.Next();
            }

            public static int Next(int maxValue)
            {
                Random inst = _local;
                if (inst == null)
                {
                    int seed;
                    lock (Global) seed = Global.Next();
                    _local = inst = new Random(seed);
                }
                return inst.Next(maxValue);
            }

            public static int Next(int minValue, int maxValue)
            {
                Random inst = _local;
                if (inst == null)
                {
                    int seed;
                    lock (Global) seed = Global.Next();
                    _local = inst = new Random(seed);
                }
                return inst.Next(minValue, maxValue);
            }
        }

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public int TurnNumber { get; set; }

        public Engine()
        {
            Init();
        }

        public void Init()
        {
            var deck1 = CardCollection.MixedDeck("1-1-1 2-2-2", "20 10");
            var deck2 = CardCollection.MixedDeck("1-1-1 2-2-2", "10 20");
            Player1 = new Player(3, "Player1", deck1);
            Player2 = new Player(4, "Player2", deck2);
            Player2.Hand.Insert(0, Card.Coin);
            TurnNumber = 0;
        }

        public string Battle()
        {
            Init();
            var res = "";
            do
            {
                res = Turn();
            } while (String.IsNullOrEmpty(res));
            return res;
        }

        public string Turn()
        {
            TurnNumber++;
            Turn(Player1, Player2);
            if (Player1.Health <= 0) return Player2.Name;
            if (Player2.Health <= 0) return Player1.Name;
            Turn(Player2, Player1);
            if (Player1.Health <= 0) return Player2.Name;
            if (Player2.Health <= 0) return Player1.Name;

            return "";
        }

        private static void Turn(Player player, Player opponent)
        {
            //Succ Mana
            player.Mana = player.Mana.NewStep();

            //GetCard
            if (player.Deck.Any())
            {
                player.Hand.Add(player.Deck.First());
                player.Deck.RemoveAt(0);
            }
            else
            {
                player.Health -= Player.FatigueValue;
                Player.FatigueValue++;
            }

            if(player.Health <= 0)
                return;

            //Attack
            player.Board.ForEach(x=>x.Attack(opponent));
            player.Board.RemoveAll(x => x.Health <= 0);
            opponent.Board.RemoveAll(x => x.Health <= 0);

            //PlayCards
            var playedCards = player.Hand.Aggregate(new {player.Mana, Cards = Enumerable.Empty<Card>()}, (res, card) =>
            {
                var restMana = res.Mana.Play(card);
                return restMana.Current >= 0 ? new {Mana = restMana, Cards = card.Cons(res.Cards)} : res;
            }).Cards.ToArray();
            player.Hand.RemoveAll(playedCards.Contains);
            player.Board.AddRange(playedCards);
        }
    }
}