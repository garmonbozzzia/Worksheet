using System.Collections.Generic;

namespace HsEmulator
{
    public interface IEffects
    {
        IEffect StartGame();
        IEffect StartTurn(object player1, object player2);
        IEffect RestoreMana();
        IEffect DrawCard();
        IEffect BattleCry();
        IEffect GetDamage();
        IEffect Deathrattle();
        IEffect Buff();
        IEffect Silence();
        IEffect Win();
        IEffect Lose();
    }

    public interface IEffect
    {
        //Effect->[Effect]
        IEnumerable<IEffect> Apply();

        //Effect->[State]
        IEnumerable<ICardState> Result();
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