using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace CountAny
{
    public class ArrayCast
    {
        private readonly int[] _array = new int[100];

        [Benchmark]
        public int Cast()
        {
            return ((ICollection<int>)_array).Count;
        }

        [Benchmark]
        public int Direct()
        {
            return _array.Length;
        }
    }
}
