using System.Collections.Generic;
using System.Linq;
using HsEmulator;
using NUnit.Framework;

namespace HsEngine
{
    public class ActorTestZone
    {
        private string testDeck = "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2" +
                                  "3-4-2";




        [Test]
        public void SimplestActor()
        {
            var actor = new SimplestPlayer();
            //actor.Pick(GetPossibilities());
        }

        public class SimplestPlayer : IActor
        {
            public IAction Pick(IEnumerable<IAction> possibilities)
            {
                return possibilities.First();
            }
        }

        public interface IAction : IEffect
        {
            //action->[Effect]
            //IEnumerable<IEffect> Do();
        }

        public interface IActor
        {
            IAction Pick(IEnumerable<IAction> possibilities);
            //get possibilities
            //pick
            //get state changes
            //wait for reaction
            //possibilities: attack(c1,c2) end_turn play_card playcard(target)
            //engine: action->dimhp(c1)->dimhp(c2)->(maybe)dead->send possibilities+states
        }
    }
}