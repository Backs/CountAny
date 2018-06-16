using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Configs;

namespace CountAny
{
    [CategoriesColumn]
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

        [Benchmark, BenchmarkCategory("Any")]
        public void ListAny()
        {
            _list.Any();
        }

        [Benchmark, BenchmarkCategory("Count")]
        public void ListCount()
        {
            _list.CustomAny();
        }

        [Benchmark, BenchmarkCategory("Any")]
        public void ArrayAny()
        {
            _array.Any();
        }

        [Benchmark, BenchmarkCategory("Count")]
        public void ArrayCount()
        {
            _array.CustomAny();
        }

        [Benchmark, BenchmarkCategory("Any")]
        public void HashSetAny()
        {
            _hashSet.Any();
        }

        [Benchmark, BenchmarkCategory("Count")]
        public void HashSetCount()
        {
            _hashSet.CustomAny();
        }

        [Benchmark, BenchmarkCategory("Any")]
        public void DictionaryAny()
        {
            _dictionary.Any();
        }

        [Benchmark, BenchmarkCategory("Count")]
        public void DictionaryCount()
        {
            _dictionary.CustomAny();
        }

        [Benchmark, BenchmarkCategory("Any")]
        public void StackAny()
        {
            _stack.Any();
        }

        [Benchmark, BenchmarkCategory("Count")]
        public void StackCount()
        {
            _stack.CustomAny();
        }

        [Benchmark, BenchmarkCategory("Any")]
        public void QueueAny()
        {
            _queue.Any();
        }

        [Benchmark, BenchmarkCategory("Count")]
        public void QueueCount()
        {
            _queue.CustomAny();
        }

        [Benchmark, BenchmarkCategory("Any")]
        public void SortedListAny()
        {
            _sortedList.Any();
        }

        [Benchmark, BenchmarkCategory("Count")]
        public void SortedListCount()
        {
            _sortedList.CustomAny();
        }

        [Benchmark, BenchmarkCategory("Any")]
        public void SortedSetAny()
        {
            _sortedSet.Any();
        }

        [Benchmark, BenchmarkCategory("Count")]
        public void SortedSetCount()
        {
            _sortedSet.CustomAny();
        }
    }
}
