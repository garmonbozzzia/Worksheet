using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace HsEmulator
{
    public class ActorTestZone
    {
        private string testDeck = "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2";

        public class Card
        {
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
                return new CardInstance {Card = this};
            }
        }


        public class Player
        {
            public List<CardInstance> Deck;
            public List<CardInstance> Hand;
            public List<CardInstance> Board;
            public List<CardInstance> Garbage;

            public IActor Actor { get; set; }

            public Player(IEnumerable<CardInstance> deck)
            {
                Deck = deck.ToList();
            }
        }

        [Test]
        public void SimplestActor()
        {
            var actor = new SimplestPlayer();
            actor.Pick(GetPossibilities());
        }

        public class SimplestPlayer : IActor
        {
            public IAction Pick(IEnumerable<IAction> possibilities)
            {
                return possibilities.First();
            }
        }

        public interface IAction : IEffect
        {
            //action->[Effect]
            //IEnumerable<IEffect> Do();
        }

        public interface IActor
        {
            IAction Pick(IEnumerable<IAction> possibilities);
            //get possibilities
            //pick
            //get state changes
            //wait for reaction
            //possibilities: attack(c1,c2) end_turn play_card playcard(target)
            //engine: action->dimhp(c1)->dimhp(c2)->(maybe)dead->send possibilities+states
        }
    }
}