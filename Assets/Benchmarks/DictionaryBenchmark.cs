using System;
using System.Collections.Generic;
using NUnit.Framework;
using Svelto.DataStructures;
using Svelto.DataStructures.Native;
using Unity.PerformanceTesting;

namespace Tests
{
    public class DictionaryBenchmark
    {
        [Test, Performance]
        public void TestSveltoRandomInsert2()
        {
            Measure.Method(
                        () =>
                        {
                            using (Measure.Scope("Insert"))
                            {
                                for (int index = 0; index < dictionarySize; index++)
                                {
                                    ref var randomIndex = ref randomIndices[index];
                                    sveltoDictionary2[randomIndex] = new Test((int)randomIndex);
                                }
                            }

                            using (Measure.Scope("ReadAndCheck"))
                            {
                                for (int index = 0; index < dictionarySize; index++)
                                {
                                    ref var randomIndex = ref randomIndices[index];
                                    if (sveltoDictionary2[randomIndex].a != randomIndex)
                                        throw new Exception("what");
                                }
                            }
                            
                            using (Measure.Scope("Remove"))
                            {
                                for (int index = 0; index < dictionarySize; index++)
                                {
                                    ref var randomIndex = ref randomIndices[index];
                                    sveltoDictionary2.Remove(randomIndex);
                                }
                            }
                        })
                   .WarmupCount(3)
                   .MeasurementCount(10)
                   .IterationsPerMeasurement(10)
                   .Run();
        }
        
        [Test, Performance]
        public void TestSveltoRandomInsert()
        {
            Measure.Method(
                        () =>
                        {
                            using (Measure.Scope("Insert"))
                            {
                                for (int index = 0; index < dictionarySize; index++)
                                {
                                    ref var randomIndex = ref randomIndices[index];
                                    sveltoDictionary[randomIndex] = new Test((int)randomIndex);
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
                            
                            using (Measure.Scope("Remove"))
                            {
                                for (int index = 0; index < dictionarySize; index++)
                                {
                                    ref var randomIndex = ref randomIndices[index];
                                    sveltoDictionary.Remove(randomIndex);
                                }
                            }
                        })
                   .WarmupCount(3)
                   .MeasurementCount(10)
                   .IterationsPerMeasurement(10)
                   .Run();
        }

        [Test, Performance]
        public void TestStandardRandomInsert()
        {
            Measure.Method(
                        () =>
                        {
                            using (Measure.Scope("Insert"))
                            {
                                for (int index = 0; index < dictionarySize; index++)
                                {
                                    ref var randomIndex = ref randomIndices[index];
                                    standardDictionary[randomIndex] = new Test((int)randomIndex);
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
                            
                            using (Measure.Scope("Remove"))
                            {
                                for (int index = 0; index < dictionarySize; index++)
                                {
                                    ref var randomIndex = ref randomIndices[index];
                                    standardDictionary.Remove(randomIndex);
                                }
                            }
                        })
                   .WarmupCount(3)
                   .MeasurementCount(10)
                   .IterationsPerMeasurement(10)
                   .Run();
        }

        public DictionaryBenchmark()
        {
            UnityEngine.Random.InitState(123456);
            randomIndices = new uint[dictionarySize];
            for (int i = 0; i < dictionarySize; i++)
                randomIndices[i] = (uint)UnityEngine.Random.Range(0, 100);
            sveltoDictionary = new SveltoDictionaryNative<uint, Test>(dictionarySize);
            sveltoDictionary2 = new  Svelto.DataStructures.Native.SveltoDictionaryNative<uint, Test>(dictionarySize);
            standardDictionary = new Dictionary<uint, Test>(dictionarySize);
        }

        const int dictionarySize = 1_000_000;
        readonly uint[] randomIndices;

        SveltoDictionaryNative<uint, Test> sveltoDictionary;
        Svelto.DataStructures.Native.SveltoDictionaryNative<uint, Test> sveltoDictionary2;
       readonly Dictionary<uint, Test> standardDictionary;

        struct Test
        {
            public int a;
            int b;
            int c;
            int d;
            public Test(int i): this() { a = i; }
        }
    }
}