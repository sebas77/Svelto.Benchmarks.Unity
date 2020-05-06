using System;
using System.Collections.Generic;
using NUnit.Framework;
using Svelto.DataStructures;
using Unity.PerformanceTesting;
    
namespace Tests
{
    public class DictionaryBenchmark
    {
        [Test, Performance]
        public void TestFasterRandomInsert()
        {
            Measure.Method(() =>
                    {
                        using (Measure.Scope("Insert"))
                        {
                            for (int index = 0; index < dictionarySize; index++)
                            {
                                ref var randomIndex = ref randomIndices[index];
                                fasterDictionary[randomIndex] = new Test((int) randomIndex);
                            }
                        }
                        using (Measure.Scope("ReadAndCheck"))
                        {
                            for (int index = 0; index < dictionarySize; index++)
                            {
                                ref var randomIndex = ref randomIndices[index];
                                if (fasterDictionary[randomIndex].a != randomIndex)
                                    throw new Exception("what");
                            }
                        }
                    })
                   .WarmupCount(3)
                   .MeasurementCount(10)
                   .IterationsPerMeasurement(1)
                   .Run();
        }
        
        [Test, Performance]
        public void TestSpanRandomInsert()
        {
            Measure.Method(() =>
                    {
                        using (Measure.Scope("Insert"))
                        {
                            for (int index = 0; index < dictionarySize; index++)
                            {
                                ref var randomIndex = ref randomIndices[index];
                                spanDictionary[randomIndex] = new Test((int) randomIndex);
                            }
                        }
                        using (Measure.Scope("ReadAndCheck"))
                        {
                            for (int index = 0; index < dictionarySize; index++)
                            {
                                ref var randomIndex = ref randomIndices[index];
                                if (spanDictionary[randomIndex].a != randomIndex)
                                    throw new Exception("what");
                            }
                        }
                    })
                   .WarmupCount(3)
                   .MeasurementCount(10)
                   .IterationsPerMeasurement(1)
                   .Run();
        }
        
        [Test, Performance]
        public void TestSveltoRandomInsert()
        {
            Measure.Method(() =>
                    {
                        using (Measure.Scope("Insert"))
                        {
                            for (int index = 0; index < dictionarySize; index++)
                            {
                                ref var randomIndex = ref randomIndices[index];
                                sveltoDictionary[randomIndex] = new Test((int) randomIndex);
                            }
                        }
                        using (Measure.Scope("ReadAndCheck"))
                        {
                            for (int index = 0; index < dictionarySize; index++)
                            {
                                ref var randomIndex = ref randomIndices[index];
                                if (sveltoDictionary[randomIndex].a != randomIndex)
                                    throw new Exception("what");
                            }
                        }
                    })
                   .WarmupCount(3)
                   .MeasurementCount(10)
                   .IterationsPerMeasurement(1)
                   .Run();
        }
        
        [Test, Performance]
        public void TestStandardRandomInsert()
        {
            Measure.Method(() =>
                    {
                        using (Measure.Scope("Insert"))
                        {
                            for (int index = 0; index < dictionarySize; index++)
                            {
                                ref var randomIndex = ref randomIndices[index];
                                standardDictionary[randomIndex] = new Test((int) randomIndex);
                            }
                        }
                        using (Measure.Scope("ReadAndCheck"))
                        {
                            for (int index = 0; index < dictionarySize; index++)
                            {
                                ref var randomIndex = ref randomIndices[index];
                                if (standardDictionary[randomIndex].a != randomIndex)
                                    throw new Exception("what");
                            }
                        }
                    })
                   .WarmupCount(3)
                   .MeasurementCount(10)
                   .IterationsPerMeasurement(1)
                   .Run();
        }

        public DictionaryBenchmark()
        {
            randomIndices = new uint[dictionarySize];
            for (int i = 0; i < dictionarySize; i++) randomIndices[i] = (uint) UnityEngine.Random.Range(0, dictionarySize);
            fasterDictionary = new FasterDictionary<uint, Test>(dictionarySize);
            spanDictionary = new SpanDictionary<uint, Test>(dictionarySize, new ManagedAllocationStrategy());
            sveltoDictionary = new SveltoDictionary<uint, Test>(dictionarySize, new ManagedStrategy<Test>());
            standardDictionary = new Dictionary<uint, Test>(dictionarySize);
        }

        const    int                          dictionarySize = 1_000_000;
        readonly uint[] randomIndices;
        
        readonly FasterDictionary<uint, Test> fasterDictionary;
        SpanDictionary<uint, Test> spanDictionary;
        readonly SveltoDictionary<uint, Test> sveltoDictionary;
        Dictionary<uint, Test> standardDictionary;

        struct Test
        {
            public int a;
            int b;
            int c;
            int d;
            public Test(int i):this() { a = i; }
        }
    }
}
