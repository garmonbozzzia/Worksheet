using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HsEmulator
{
    public class Effect:IEffect
    {
        private readonly Func<IEffect, IEnumerable<IEffect>> _apply;

        public Effect(Func<IEffect, IEnumerable<IEffect>> apply)
        {
            _apply = apply;
        }

        public IEnumerable<IEffect> Apply()
        {
            return _apply(this);
        }

        public IEnumerable<ICardState> Result()
        {
            throw new NotImplementedException();
        }

        public string Name { get; set; }
    }

    public class Effects : IEffects
    {
        public IEffect StartGame()
        {
            var player1 = new object();
            var player2 = new object();

            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect =>
            {
                var p1Turn = StartTurn(player1, player2).Apply();
                var p2Turn = StartTurn(player2, player1).Apply();
                return 
                    p1Turn.Concat(p2Turn)
                    .Repeat()
                    .TakeWhile(x=>x.Name != "GameOver");
                
            });
            return new Effect(apply);
        }

        public IEffect StartTurn(object player, object opponent)
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

        public IEffect PlaceCardOnHand()
        {
            throw new NotImplementedException();
        }

        public IEffect PlaceCardOnBoard()
        {
            throw new NotImplementedException();
        }

        public IEffect BecomeSleep()
        {
            throw new NotImplementedException();
        }

        public IEffect BecomeActive()
        {
            throw new NotImplementedException();
        }

        public IEffect RemoveFromBoard()
        {
            throw new NotImplementedException();
        }
    }
}