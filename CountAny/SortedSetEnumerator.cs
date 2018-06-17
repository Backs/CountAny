using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;

namespace CountAny
{
    [CategoriesColumn]
    public class SortedSetEnumerator
    {
        [Params(1000, 10000, 100000)]
        public int _size;

        SortedSet<int> _sortedSet;

        [GlobalSetup]
        public void SetUp()
        {
            _sortedSet = new SortedSet<int>();

            for (int i = 0; i < _size; i++)
            {
                _sortedSet.Add(i);
            }
        }

        [Benchmark(Baseline = true)]
        public bool Any()
        {
            return _sortedSet.Any();
        }

        [Benchmark]
        public bool Custom()
        {
            return _sortedSet.CustomAny();
        }
    }
}
