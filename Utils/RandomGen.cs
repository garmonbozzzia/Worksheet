using System;

namespace Utils
{
    public static class RandomGen
    {
        private static readonly Random Global = new Random();
        [ThreadStatic]
        private static Random _local;

        public static int Next()
        {
            var inst = _local;
            if (inst == null)
            {
                int seed;
                lock (Global) seed = Global.Next();
                _local = inst = new Random(seed);
            }
            return inst.Next();
        }

        public static int Next(int maxValue)
        {
            var inst = _local;
            if (inst == null)
            {
                int seed;
                lock (Global) seed = Global.Next();
                _local = inst = new Random(seed);
            }
            return inst.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            var inst = _local;
            if (inst == null)
            {
                int seed;
                lock (Global) seed = Global.Next();
                _local = inst = new Random(seed);
            }
            return inst.Next(minValue, maxValue);
        }
    }
}