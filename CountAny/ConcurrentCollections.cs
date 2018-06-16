using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;

namespace CountAny
{
    [CategoriesColumn]
    public class ConcurrentCollections
    {
        [Params(10, 100, 1000, 10000)]
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

        //[Benchmark, BenchmarkCategory("Any")]
        //public void BagAny()
        //{
        //    _bag.Any();
        //}

        //[Benchmark, BenchmarkCategory("Count")]
        //public void BagCount()
        //{
        //    _bag.CustomAny();
        //}

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

        //[Benchmark, BenchmarkCategory("Any")]
        //public void StackAny()
        //{
        //    _stack.Any();
        //}

        //[Benchmark, BenchmarkCategory("Count")]
        //public void StackCount()
        //{
        //    _stack.CustomAny();
        //}

        //[Benchmark, BenchmarkCategory("Any")]
        //public void QueueAny()
        //{
        //    _queue.Any();
        //}

        //[Benchmark, BenchmarkCategory("Count")]
        //public void QueueCount()
        //{
        //    _queue.CustomAny();
        //}
    }
}
