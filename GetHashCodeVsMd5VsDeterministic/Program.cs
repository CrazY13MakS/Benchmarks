using BenchmarkDotNet.Running;

namespace GetHashCodeVsMd5VsDeterministic
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmark>();
        }
    }
}
