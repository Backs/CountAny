using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Configs;

namespace CountAny
{
    [CategoriesColumn]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class SimpleCollections
    {
        [Params(1000)]
        public int _size;

        private List<int> _list;
        private int[] _array;
        private HashSet<int> _hashSet;
        private Dictionary<int, int> _dictionary;
        private Stack<int> _stack;
        private Queue<int> _queue;
        private SortedList<int, int> _sortedList;
        private SortedSet<int> _sortedSet;

        [GlobalSetup]
        public void SetUp()
        {
            _list = new List<int>();
            _array = new int[_size];
            _hashSet = new HashSet<int>();
            _dictionary = new Dictionary<int, int>();
            _stack = new Stack<int>();
            _queue = new Queue<int>();
            _sortedList = new SortedList<int, int>();
            _sortedSet = new SortedSet<int>();

            for (int i = 0; i < _size; i++)
            {
                _list.Add(i);
                _array[i] = i;
                _hashSet.Add(i);
                _dictionary[i] = i;
                _stack.Push(i);
                _queue.Enqueue(i);
                _sortedList.Add(i, i);
                _sortedSet.Add(i);
            }
        }

        [Benchmark(Baseline = true, Description = "Any"), BenchmarkCategory("List")]
        public bool ListAny()
        {
            return _list.Any();
        }

        [Benchmark(Description = "Custom"), BenchmarkCategory("List")]
        public bool ListCount()
        {
            return _list.CustomAny();
        }

        [Benchmark(Baseline = true, Description = "Any"), BenchmarkCategory("Array")]
        public bool ArrayAny()
        {
            return _array.Any();
        }

        [Benchmark(Description = "Custom"), BenchmarkCategory("Array")]
        public bool ArrayCount()
        {
            return _array.CustomAny();
        }

        [Benchmark(Baseline = true, Description = "Any"), BenchmarkCategory("HashSet")]
        public bool HashSetAny()
        {
            return _hashSet.Any();
        }

        [Benchmark(Description = "Custom"), BenchmarkCategory("HashSet")]
        public bool HashSetCount()
        {
            return _hashSet.CustomAny();
        }

        [Benchmark(Baseline = true, Description = "Any"), BenchmarkCategory("Dictinary")]
        public bool DictionaryAny()
        {
            return _dictionary.Any();
        }

        [Benchmark(Description = "Custom"), BenchmarkCategory("Dictinary")]
        public bool DictionaryCount()
        {
            return _dictionary.CustomAny();
        }

        [Benchmark(Baseline = true, Description = "Any"), BenchmarkCategory("Stack")]
        public bool StackAny()
        {
            return _stack.Any();
        }

        [Benchmark(Description = "Custom"), BenchmarkCategory("Stack")]
        public bool StackCount()
        {
            return _stack.CustomAny();
        }

        [Benchmark(Baseline = true, Description = "Any"), BenchmarkCategory("Queue")]
        public bool QueueAny()
        {
            return _queue.Any();
        }

        [Benchmark(Description = "Custom"), BenchmarkCategory("Queue")]
        public bool QueueCount()
        {
            return _queue.CustomAny();
        }

        [Benchmark(Baseline = true, Description = "Any"), BenchmarkCategory("SortedList")]
        public bool SortedListAny()
        {
            return _sortedList.Any();
        }

        [Benchmark(Description = "Custom"), BenchmarkCategory("SortedList")]
        public bool SortedListCount()
        {
            return _sortedList.CustomAny();
        }

        [Benchmark(Baseline = true, Description = "Any"), BenchmarkCategory("SortedSet")]
        public bool SortedSetAny()
        {
            return _sortedSet.Any();
        }

        [Benchmark(Description = "Custom"), BenchmarkCategory("SortedSet")]
        public bool SortedSetCount()
        {
            return _sortedSet.CustomAny();
        }
    }
}
