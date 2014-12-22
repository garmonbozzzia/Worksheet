using System;
using System.Collections.Generic;
using System.Linq;

namespace HsEmulator
{
    public interface IEffects
    {
        IEffect StartGame();
        IEffect NextRound();
        IEffect Turn(object player1, object player2);
        IEffect RestoreMana(object player);
        IEffect DrawCard(object player);
        IEffect BattleCry();
        IEffect GetDamage(CardInstance card);
        IEffect Deathrattle();
        IEffect Buff();
        IEffect Silence();

        IEffect Win();
        IEffect Lose();
        IEffect GameOver();

        IEffect EndTurn();
        IEffect PlayCard();
        IEffect Attack();

        IEffect PlayerActions();

        IEffect PlaceCardOnHand();
        IEffect PlaceCardOnBoard();
        IEffect BecomeSleep();
        IEffect BecomeActive();
        IEffect RemoveFromBoard();
    }

    interface IActions
    {
        IAction EndTurn();
        IAction PlayCard();
        IAction Attack();
    }

    public interface IEffect
    {
        //Effect->[Effect]
        IEnumerable<IEffect> Apply();

        //Effect->[State]
        IEnumerable<ICardState> Result();

        String Name { get; set; }
        int Id { get; set; }
    }

    public class CardInstance
    {
        public Card Card { get; set; }
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Hp { get; set; }
        public List<String> Buffs { get; set; }
    }

    public interface ICardState
    {
        IEnumerable<IAction> Possibilities();
    }

    public interface IAction
    {
        //action->[Effect]
        IEnumerable<IEffect> Do();
    }

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
            return new CardInstance { Card = this };
        }
    }

}