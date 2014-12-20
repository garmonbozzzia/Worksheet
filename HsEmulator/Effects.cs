using System;

namespace HsEmulator
{
    public class Effects : IEffects
    {
        public IEffect StartGame()
        {
            object player1 = null;
            object player2 = null;
            return StartTurn(player1, player2);
        }

        public IEffect StartTurn(object player1, object player2)
        {
            throw new NotImplementedException();
        }

        public IEffect RestoreMana()
        {
            throw new NotImplementedException();
        }

        public IEffect DrawCard()
        {
            throw new NotImplementedException();
        }

        public IEffect BattleCry()
        {
            throw new NotImplementedException();
        }

        public IEffect GetDamage()
        {
            throw new NotImplementedException();
        }

        public IEffect Deathrattle()
        {
            throw new NotImplementedException();
        }

        public IEffect Buff()
        {
            throw new NotImplementedException();
        }

        public IEffect Silence()
        {
            throw new NotImplementedException();
        }

        public IEffect Win()
        {
            throw new NotImplementedException();
        }

        public IEffect Lose()
        {
            throw new NotImplementedException();
        }
    }
}