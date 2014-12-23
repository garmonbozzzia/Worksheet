using System;
using System.Collections.Generic;
using System.Linq;
using HsEmulator;

namespace HsEngine
{
    public class CardInstance
    {
        public Card Card { get; set; }
        public Player Owner { get; set; }

        public int Mana { get; set; }
        public int AttackValue { get; set; }
        public int Hp { get; set; }
        public List<string> Buffs { get; set; }

        public IEffect Deathrattle { get; set; }

        public IEffect Attack(CardInstance target)
        {
            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect =>
                GetDamage(target.AttackValue)
                    .Next(target.GetDamage(AttackValue))
                    .SelectMany(x => x.Apply())
                );
            return new Effect(apply) { Type = "Attack" };
        }

        public IEffect GetDamage(int val)
        {
            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect =>
            {
                Hp -= val;
                return Hp <= 0 ? Death().Apply() : Enumerable.Empty<IEffect>();
            });

            return new Effect(apply) { Type = "GetDamage" };
        }

        public IEffect Death()
        {
            //MoveToGarbage
            Owner.MoveFromBoardToGarbage(this);
            //Deathrattle
            return new Effect { Type = "Death" };
        }
    }
}