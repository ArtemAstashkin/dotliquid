using System;
using BenchmarkDotNet.Running;

namespace DotLiquid.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<DotLiquidPerfTest>();

            Console.WriteLine("Press Enter to close app");
            Console.ReadLine();
        }
    }
}
