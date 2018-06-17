using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Configs;

namespace CountAny
{
    [CategoriesColumn]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class ConcurrentCollections
    {
        [Params(1000)]
        public int _size;

        private ConcurrentBag<int> _bag;
        private ConcurrentDictionary<int, int> _dictionary;
        private ConcurrentQueue<int> _queue;
        private ConcurrentStack<int> _stack;


        [GlobalSetup]
        public void SetUp()
        {
            _bag = new ConcurrentBag<int>();
            _dictionary = new ConcurrentDictionary<int, int>();
            _queue = new ConcurrentQueue<int>();
            _stack = new ConcurrentStack<int>();

            var tasksCount = 10;
            var batch = _size / tasksCount;

            var tasks = new Task[tasksCount];

            for (int i = 0; i < tasksCount; i++)
            {
                var task = i;

                tasks[task] = Task.Run(() =>
                {
                    var from = task * batch;
                    var to = (task + 1) * batch;

                    for (int j = from; j < to; j++)
                    {
                        _bag.Add(j);
                        _dictionary[j] = j;
                        _queue.Enqueue(j);
                        _stack.Push(j);
                    }
                });
            }

            Task.WaitAll(tasks);
        }

        [Benchmark(Baseline = true, Description = "Any"), BenchmarkCategory("ConcurrentBag")]
        public bool BagAny()
        {
            return _bag.Any();
        }

        [Benchmark(Description = "Custom"), BenchmarkCategory("ConcurrentBag")]
        public bool BagCount()
        {
            return _bag.CustomAny();
        }

        [Benchmark(Baseline = true, Description = "Any"), BenchmarkCategory("ConcurrentDictionary")]
        public bool DictionaryAny()
        {
            return _dictionary.Any();
        }

        [Benchmark(Description = "Custom"), BenchmarkCategory("ConcurrentDictionary")]
        public bool DictionaryCount()
        {
            return _dictionary.CustomAny();
        }

        [Benchmark(Baseline = true, Description = "Any"), BenchmarkCategory("ConcurrentStack")]
        public bool StackAny()
        {
            return _stack.Any();
        }

        [Benchmark(Description = "Custom"), BenchmarkCategory("ConcurrentStack")]
        public bool StackCount()
        {
            return _stack.CustomAny();
        }

        [Benchmark(Baseline = true, Description = "Any"), BenchmarkCategory("ConcurrentQueue")]
        public bool QueueAny()
        {
            return _queue.Any();
        }

        [Benchmark(Description = "Custom"), BenchmarkCategory("ConcurrentQueue")]
        public bool QueueCount()
        {
            return _queue.CustomAny();
        }
    }
}
