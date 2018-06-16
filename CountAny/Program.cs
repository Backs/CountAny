using BenchmarkDotNet.Running;

namespace CountAny
{
    public class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<SimpleCollections>();
            BenchmarkRunner.Run<ConcurrentCollections>();
            //BenchmarkRunner.Run<ConcurrentDictionaryCount>();
            //BenchmarkRunner.Run<SortedSetEnumerator>();
        }
    }
}
