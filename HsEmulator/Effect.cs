using System;
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

        public Effect()
        {
            _apply = effect => Enumerable.Empty<IEffect>();
            //_apply = effect => effect.ListWrap();
        }

        public IEnumerable<IEffect> Apply()
        {
            return new Effect {Name = Name}.Cons(_apply(this));
        }

        public IEnumerable<ICardState> Result()
        {
            throw new NotImplementedException();
        }

        public string Name { get; set; }
    }
}