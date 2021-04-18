using System;

namespace HeBianGu.Common.TestData
{
    public static class FakerRandom
    {
        internal static Random Rand = new Random();

        public static void Seed(int seed)
        {
            Rand = new Random(seed);
        }
    }
}
