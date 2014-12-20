using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace HsEmulator
{
    public class Testzone
    {
        [Test]
        public void CreateInputAndOutput()
        {
            Console.WriteLine(Environment.CurrentDirectory);
            File.WriteAllText("Input.txt", "input");
            File.WriteAllText("Output.txt", "output");
        }

        public class Engine
        {
            //Action->[Effect]->[State]->[Action]

            IEnumerable<IAction> ProcessAction(IEnumerable<IAction> actions, IEnumerable<ICardState> states)
            {
                IEnumerable<ICardState> newstates = actions
                    .SelectMany(x=>x.Do())
                    .SelectMany(x => x.Do())
                    .SelectMany(x => x.Result());
                //todo replace oldstates
                return newstates.SelectMany(x => x.Possibilities());
            }
        }

        public interface IEffect
        {
            //Effect->[Effect]
            IEnumerable<IEffect> Do();

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

        public interface IActor
        {
            Action Pick(IEnumerable<Action> possibilities);
            //get possibilities
            //pick
            //get state changes
            //wait for reaction
            //possibilities: attack(c1,c2) end_turn play_card playcard(target)
            //engine: action->dimhp(c1)->dimhp(c2)->(maybe)dead->send possibilities+states
        }

        public class BattleObserver
        {
            //card type,instance
            //attack observer
            //turn observer
            //life length observer

        }
    }
}