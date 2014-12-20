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
        public void StartGame()
        {
            var res = new Effects().StartGame().Apply().Select(x=>x.Name).ToList();
            Console.WriteLine(res.Count);
            res.ForEach(Console.WriteLine);
        }

        [Test]
        public void ToTest()
        {
            Console.WriteLine("1.To(35) is");
            1.To(35).ForEach(x=> Console.Write("{0} ", x));
        }

        [Test]
        public void EndTurnOneCall()
        {
            new Effects().EndTurn()
                .ListWrap()
                .Repeat(10)
                .SelectMany(x => x.Apply())
                .Select(x => x.Name)
                .ForEach(Console.WriteLine);
        }

        [Test]
        public void EndTurn()
        {
            var effects = new Effects();
            1.To(10)
                .Select(x => effects.EndTurn())
                .SelectMany(x => x.Apply())
                .Select(x => x.Name)
                .ToList()
                .ForEach(Console.WriteLine);
        }

        [Test]
        public void TakeWhileIncluding()
        {
            Console.WriteLine("1.To(10) x<5");
            1.To(10)
                .TakeWhileIncluding(x=>x<5)
                .ForEach(x=>Console.Write("{0} ",x));
        }

        [Test]
        public void EndTurnLazy()
        {
            var effects = new Effects();
            1.To(10)
                .SelectMany(x => effects.EndTurn().ListWrap())
                .SelectMany(x => x.Apply())
                .Select(x => x.Name)
                //.TakeWhileIncluding(x => x != "sGameOver")
                .ForEach(Console.WriteLine);
        }

        [Test]
        public void UntilGameOver()
        {
            var effects = new Effects();
            1.To(10)
                .SelectMany(x => effects.EndTurn().ListWrap())
                .SelectMany(x => x.Apply())
                .Select(x => x.Name)
                .TakeWhileIncluding(x => x != "GameOver")
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