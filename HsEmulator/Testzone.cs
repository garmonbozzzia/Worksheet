using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media.Effects;
using NUnit.Framework;
using NUnit.Framework.Internal;

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

        [Test]
        public void MyMethod()
        {
            var res = new Effects().StartGame().Apply().Select(x=>x.Name).ToList();
            Console.WriteLine(res.Count);
            res.ForEach(Console.WriteLine);
        }

        [Test]
        public void T2()
        {
            new Effects()
                .EndTurnL()
                .Repeat(30)
                .SelectMany(x=>x.Apply())
                .Select(x=>x.Name)
                .ToList()
                .ForEach(Console.WriteLine);
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