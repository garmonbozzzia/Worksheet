using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace HsEmulator
{
    public class Effect:IEffect
    {
        private readonly Func<IEffect, IEnumerable<IEffect>> _apply;

        public Effect(Func<IEffect, IEnumerable<IEffect>> apply)
        {
            _apply = apply;
            Head = () => new Effect {Name = Name};
        }

        public Effect()
        {
            _apply = effect => Enumerable.Empty<IEffect>();
            Head = () => this;
            //_apply = effect => effect.ListWrap();
        }

        public Func<IEffect> Head { get; set; }

        public IEnumerable<IEffect> Apply()
        {
            return Head().Cons(_apply(this));
        }

        public IEnumerable<ICardState> Result()
        {
            throw new NotImplementedException();
        }

        public string Name { get; set; }
    }
}