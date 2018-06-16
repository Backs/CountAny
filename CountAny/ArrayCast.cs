using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace CountAny
{
    public class ArrayCast
    {
        private readonly int[] _array = new int[100];

        [Benchmark]
        public void Cast()
        {
            var result = ((ICollection<int>)_array).Count;
        }

        [Benchmark]
        public void Direct()
        {
            var result = _array.Length;
        }
    }
}
