using System;
using System.IO;
using System.Linq;
using HsEmulator;
using NUnit.Framework;

namespace HsEngine
{
    public class Testzone
    {
        private string testDeck = String.Concat(Enumerable.Repeat("3-4-2 ", 20)).Trim();

        [Test]
        public void CreateInputAndOutput()
        {
            Console.WriteLine(Environment.CurrentDirectory);
            File.WriteAllText("Input.txt", "");
            File.WriteAllText("Output.txt", "");
        }

        [Test]
        public void StartGame()
        {
            new Effects().StartGame()
                .Apply()
                .Select(x => String.Format("--<{0}> {1}", x.Id, x.Type))
                .ForEach(Console.WriteLine);
        }

        [Test]
        public void DrawCard()
        {
            var deck = testDeck.Split(' ')
                .Select(Card.Parse)
                .Select(x => x.Instance());
            var hero = Card.Parse("0-0-30").Instance();
            var p = new Player(deck);
            p.Opponent = p;
            1.To()
                .Select(_ => Effects.Instance().DrawCard(p))
                .SelectMany(x => x.Apply())
                .TakeWhileIncluding(x => x.Type != "GameOver")
                .Select(x => String.Format("--<{0}> {1}", x.Id, x.Type))
                .ForEach(Console.WriteLine);
        }

        [Test]
        public void NextRound()
        {
            new Effects().NextRound()
                .Apply()
                .Select(x => "--" + x.Type)
                .ForEach(Console.WriteLine);
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
                .Select(x => x.Type)
                .ForEach(Console.WriteLine);
        }

        [Test]
        public void EndTurn()
        {
            var effects = new Effects();
            1.To(10)
                .Select(x => effects.EndTurn())
                .SelectMany(x => x.Apply())
                .Select(x => x.Type)
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
                .Select(x => x.Type)
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
                .Select(x => x.Type)
                .TakeWhileIncluding(x => x != "GameOver")
                .ForEach(Console.WriteLine);
        }
    }
}