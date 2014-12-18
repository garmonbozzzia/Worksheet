﻿using System;
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

        public void Step()
        {
            Step(Player1);
            Step(Player2);
        }

        private static void Step(Player player)
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

            //PlayCards
            var playedCards = player.Hand.Aggregate(new {player.Mana, Cards = Enumerable.Empty<Card>()}, (res, card) =>
            {
                var restMana = res.Mana.Play(card);
                return restMana.Current >= 0 ? new {Mana = restMana, Cards = card.Cons(res.Cards)} : res;
            }).Cards.ToArray();
            player.Hand.RemoveAll(playedCards.Contains);
            player.Board.AddRange(playedCards);

            //Attack
        }
    }

    public static class Extensions
    {
        public static IEnumerable<T> Cons<T>(this T x, IEnumerable<T> xs)
        {
            return Enumerable.Repeat(x,1).Concat(xs);
        }
    }

    public class Player : HeroCard
    {
        public static int DeckCardNumber = 30;

        public List<Card> Deck { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> Board { get; set; }

        public Mana Mana { get; set; }

        public static int FatigueValue { get; set; }

        public Player(int numCards, string name)
        {
            FatigueValue = 1;
            Mana = new Mana {Limit = 0, Current = 0};
            Deck = Enumerable.Range(0, DeckCardNumber).Select(x => Card.Generate()).ToList();
            Hand = Deck.Take(numCards).ToList();
            Board = Enumerable.Repeat(this, 1).Cast<Card>().ToList();
            Deck.RemoveRange(0, numCards);
            Name = name;
        }
    }

    public class Mana
    {
        public static int Max = 10;
        public int Limit { get; set; }
        public int Current { get; set; }
        //+перегрузка
        public Mana NewStep() 
        {
            return new Mana {Limit = Limit == Max? Limit : Limit + 1, Current = Limit};
        }

        public Mana Play(Card card)
        {
            return new Mana() {Current = Current - card.Mana, Limit = Limit};
        }
    }
}