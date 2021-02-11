using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using System;
using System.Security.Cryptography;
using System.Text;

namespace GetHashCodeVsMd5VsDeterministic
{

    [Config(typeof(Config))]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser] // we need to enable it in explicit way
    [RyuJitX64Job] // let's run the benchmarks for 32 & 64 bit
    public class Benchmark
    {

        private static Random Random = new Random();
        static MD5 md5Hash = MD5.Create();

        // Setup job configuration
        private class Config : ManualConfig
        {
            public Config()
            {
                AddJob(Job.MediumRun.WithGcServer(true).WithGcForce(true).WithId("ServerForce"));
                AddJob(Job.MediumRun.WithGcServer(true).WithGcForce(false).WithId("Server"));
                // Add(Job.MediumRun.WithGcServer(false).WithGcForce(true).WithId("Workstation"));
                // Add(Job.MediumRun.WithGcServer(false).WithGcForce(false).WithId("WorkstationForce"));
            }
        }

        public string StaticString { get; set; }
        public string[] StringArray { get; set; }
        public int Iterations { get; set; } = 100000;

        //Fill array before
        [GlobalSetup]
        public void GlobalSetup()
        {
            StaticString = Helper.GenerateString(264);
            StringArray = new string[Iterations];
            for (int i = 0; i < Iterations; i++)
            {
                StringArray[i] = Helper.GenerateString(264);
            }
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            StringArray = null;
        }

        [Benchmark]
        public void NativeStatic()
        {
            for (int i = 0; i < Iterations; i++)
            {
                StaticString.GetHashCode();
            }
        }
        [Benchmark]
        public void NativeArray()
        {
            for (int i = 0; i < Iterations; i++)
            {
                StringArray[i].GetHashCode();
            }
        }


        [Benchmark]
        public void DeterministicStatic()
        {
            for (int i = 0; i < Iterations; i++)
            {
                StaticString.GetDeterministicHashCode();
            }
        }
        [Benchmark]
        public void DeterministicArray()
        {
            for (int i = 0; i < Iterations; i++)
            {
                StringArray[i].GetDeterministicHashCode();
            }
        }

        [Benchmark]
        public void MD5Static()
        {
            for (int i = 0; i < Iterations; i++)
            {
                GetMd5(StaticString);
            }
        }
        [Benchmark]
        public void MD5Array()
        {
            for (int i = 0; i < Iterations; i++)
            {
                GetMd5(StringArray[i]);
            }
        }


        private static string GetMd5(string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
