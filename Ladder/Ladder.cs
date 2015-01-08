using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Utils;

namespace Ladder
{
    public class Player
    {
        public int CurrentStars { get; set; }
        public int Streak { get; set; }
        public int Rank { get; set; }
    }


    public class Ladder
    {
        private static int CalcRank(int stars)
        {
            var lastOrDefault = Enumerable.Repeat(3, 5)
                .Concat(Enumerable.Repeat(4, 5))
                .Concat(Enumerable.Repeat(5, 11))
                .Scanl(0, (x, y) => x + y)
                .Zip(20.To(0), (x, y) => new {Rank = y, Total = x})
                .LastOrDefault(x => x.Total < stars);
            if (lastOrDefault != null)
                return
                    lastOrDefault
                        .Rank;
            return 20;
        }

        public void Play(Player p1, Player p2)
        {
            if (RandomGen.Next(2) == 0)
            {
                Win(p1);
                Lose(p2);
            }
            else
            {
                Win(p2);
                Lose(p1);
            }
        }

        private void Lose(Player p)
        {
            p.Streak = 0;
            p.CurrentStars--;
        }

        private void Win(Player p)
        {
            p.Streak++;
            p.CurrentStars++;
            if(p.Streak>=3 && p.Rank > 5 ) p.CurrentStars++;
        }

        [Test]
        public void TestNegTo()
        {
            20.To(1).ForEach(Console.WriteLine);
        }

        [Test]
        public void TestOfCalcFunk()
        {
            //Enumerable.Range(1, 100).Select(CalcRank).ForEach(Console.WriteLine);
            var res = 1.To().First(x => CalcRank(x) == 0);
            Console.WriteLine(res);
        }

        [Test]
        public void Memoization()
        {
            Func<int, int> f = CalcRank;
            var cr = f.Memoize();
            //Console.WriteLine(1.To(1000000).Select(x => Utils.RandomGen.Next(87)).Select(CalcRank).ToArray().Count());
            Console.WriteLine(1.To(1000000).Select(x => Utils.RandomGen.Next(87)).Select(cr).ToArray().Count());
        }

        public class Settings
        {
            public int NumberOfPlayers; 
        }
    }
}
