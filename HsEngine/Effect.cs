using System;
using System.Collections.Generic;
using System.Linq;
using HsEmulator;

namespace HsEngine
{


    public class Effect:IEffect
    {
        private readonly Func<IEffect, IEnumerable<IEffect>> _apply;
        private static int IdGen = 1;

        internal class NothingEffect : IEffect
        {
            public IEnumerable<IEffect> Apply() { yield break; }
            public IEnumerable<ICardState> Result()
            {
                yield break;
            }

            public string Type { get; set; }
            public int Id { get; set; }
        }

        public static readonly IEffect Nothing = new NothingEffect(){Id = 0};

        public Effect(Func<IEffect, IEnumerable<IEffect>> apply)
        {
            _apply = apply;
            Id = IdGen++;
            Head = () => new Effect {Type = Type};
        }

        public Effect()
        {
            _apply = effect => Enumerable.Empty<IEffect>();
            Head = () => this;
            Id = IdGen++;
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

        public string Type { get; set; }
        public int Id { get; set; }

    }
}