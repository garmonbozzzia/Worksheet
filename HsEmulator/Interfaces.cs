using System;
using System.Collections.Generic;

namespace HsEmulator
{
    public interface IEffects
    {
        IEffect StartGame();
        IEffect Turn(object player1, object player2);
        IEffect RestoreMana(object player);
        IEffect DrawCard(object player);
        IEffect BattleCry();
        IEffect GetDamage();
        IEffect Deathrattle();
        IEffect Buff();
        IEffect Silence();

        IEffect Win();
        IEffect Lose();
        IEffect GameOver();

        IEffect EndTurn();
        IEffect PlayCard();
        IEffect Attack();

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

}