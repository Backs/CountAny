using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;

namespace CountAny
{
    [CategoriesColumn]
    public class ConcurrentDictionaryCount
    {
        [Params(10, 100, 1000, 10000)]
        public int _size;

        private ConcurrentDictionary<int, int> _dictionary;

        [GlobalSetup]
        public void SetUp()
        {
            _dictionary = new ConcurrentDictionary<int, int>();

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
                        _dictionary[j] = j;
                    }
                });
            }

            Task.WaitAll(tasks);
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
    }
}
