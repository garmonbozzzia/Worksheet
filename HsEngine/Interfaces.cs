using System;
using System.Collections.Generic;
using System.Linq;

namespace HsEngine
{
    public interface IEffects
    {
        IEffect StartGame();
        IEffect NextRound();
        IEffect Turn(Player player1);
        IEffect RestoreMana(Player player);
        IEffect DrawCard(Player player);
        IEffect BattleCry();
        IEffect GetDamage(CardInstance card, int val);
        IEffect Death(CardInstance card);        
        IEffect Deathrattle();
        IEffect Buff();
        IEffect Silence();

        IEffect Win();
        IEffect Lose();
        IEffect GameOver();

        IEffect EndTurn();
        IEffect PlayCard();
        IEffect Attack(CardInstance attacker, CardInstance target);

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

        String Type { get; set; }
        int Id { get; set; }
    }

    //todo delete
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
            return new CardInstance {Card = this, AttackValue = Attack, Mana = Mana, Hp = Hp};
        }
    }

}