namespace Tests
{
    public class DictionaryBenchmark
    {
        // [Test, Performance]
        // public void TestFasterRandomInsert()
        // {
        //     Measure.Method(() =>
        //             {
        //                 using (Measure.Scope("Insert"))
        //                 {
        //                     for (int index = 0; index < dictionarySize; index++)
        //                     {
        //                         ref var randomIndex = ref randomIndices[index];
        //                         fasterDictionary[randomIndex] = new Test((int) randomIndex);
        //                     }
        //                 }
        //                 using (Measure.Scope("ReadAndCheck"))
        //                 {
        //                     for (int index = 0; index < dictionarySize; index++)
        //                     {
        //                         ref var randomIndex = ref randomIndices[index];
        //                         if (fasterDictionary[randomIndex].a != randomIndex)
        //                             throw new Exception("what");
        //                     }
        //                 }
        //             })
        //            .WarmupCount(3)
        //            .MeasurementCount(10)
        //            .IterationsPerMeasurement(10)
        //            .Run();
        // }
        
        // [Test, Performance]
        // public void TestSpanRandomInsert()
        // {
        //     Measure.Method(() =>
        //             {
        //                 using (Measure.Scope("Insert"))
        //                 {
        //                     for (int index = 0; index < dictionarySize; index++)
        //                     {
        //                         ref var randomIndex = ref randomIndices[index];
        //                         spanDictionary[randomIndex] = new Test((int) randomIndex);
        //                     }
        //                 }
        //                 using (Measure.Scope("ReadAndCheck"))
        //                 {
        //                     for (int index = 0; index < dictionarySize; index++)
        //                     {
        //                         ref var randomIndex = ref randomIndices[index];
        //                         if (spanDictionary[randomIndex].a != randomIndex)
        //                             throw new Exception("what");
        //                     }
        //                 }
        //             })
        //            .WarmupCount(3)
        //            .MeasurementCount(10)
        //            .IterationsPerMeasurement(1)
        //            .Run();
        // }
        
        // [Test, Performance]
        // public void TestSveltoRandomInsert()
        // {
        //     Measure.Method(() =>
        //             {
        //                 using (Measure.Scope("Insert"))
        //                 {
        //                     for (int index = 0; index < dictionarySize; index++)
        //                     {
        //                         ref var randomIndex = ref randomIndices[index];
        //                         sveltoDictionary[randomIndex] = new Test((int) randomIndex);
        //                     }
        //                 }
        //                 using (Measure.Scope("ReadAndCheck"))
        //                 {
        //                     for (int index = 0; index < dictionarySize; index++)
        //                     {
        //                         ref var randomIndex = ref randomIndices[index];
        //                         if (sveltoDictionary[randomIndex].a != randomIndex)
        //                             throw new Exception("what");
        //                     }
        //                 }
        //             })
        //            .WarmupCount(3)
        //            .MeasurementCount(10)
        //            .IterationsPerMeasurement(10)
        //            .Run();
        // }
        
        // [Test, Performance]
        // public void TestStandardRandomInsert()
        // {
        //     Measure.Method(() =>
        //             {
        //                 using (Measure.Scope("Insert"))
        //                 {
        //                     for (int index = 0; index < dictionarySize; index++)
        //                     {
        //                         ref var randomIndex = ref randomIndices[index];
        //                         standardDictionary[randomIndex] = new Test((int) randomIndex);
        //                     }
        //                 }
        //                 using (Measure.Scope("ReadAndCheck"))
        //                 {
        //                     for (int index = 0; index < dictionarySize; index++)
        //                     {
        //                         ref var randomIndex = ref randomIndices[index];
        //                         if (standardDictionary[randomIndex].a != randomIndex)
        //                             throw new Exception("what");
        //                     }
        //                 }
        //             })
        //            .WarmupCount(3)
        //            .MeasurementCount(10)
        //            .IterationsPerMeasurement(1)
        //            .Run();
        // }
        
        // [Test, Performance]
        // public void TestSveltoGenericsRandomInsert()
        // {
        //     Measure.Method(() =>
        //             {
        //                 using (Measure.Scope("Insert"))
        //                 {
        //                     for (int index = 0; index < dictionarySize; index++)
        //                     {
        //                         ref var randomIndex = ref randomIndices[index];
        //                         sveltoDictionaryGenerics[randomIndex] = new Test((int) randomIndex);
        //                     }
        //                 }
        //                 using (Measure.Scope("ReadAndCheck"))
        //                 {
        //                     for (int index = 0; index < dictionarySize; index++)
        //                     {
        //                         ref var randomIndex = ref randomIndices[index];
        //                         if (sveltoDictionaryGenerics[randomIndex].a != randomIndex)
        //                             throw new Exception("what");
        //                     }
        //                 }
        //             })
        //            .WarmupCount(3)
        //            .MeasurementCount(10)
        //            .IterationsPerMeasurement(1)
        //            .Run();
        // }
        
        // [Test, Performance]
        // public void TestSveltoGenericsNativeRandomInsert()
        // {
        //     Measure.Method(() =>
        //             {
        //                 using (Measure.Scope("Insert"))
        //                 {
        //                     for (int index = 0; index < dictionarySize; index++)
        //                     {
        //                         ref var randomIndex = ref randomIndices[index];
        //                         sveltoNativeDictionaryGenerics[randomIndex] = new Test((int) randomIndex);
        //                     }
        //                 }
        //                 using (Measure.Scope("ReadAndCheck"))
        //                 {
        //                     for (int index = 0; index < dictionarySize; index++)
        //                     {
        //                         ref var randomIndex = ref randomIndices[index];
        //                         if (sveltoNativeDictionaryGenerics[randomIndex].a != randomIndex)
        //                             throw new Exception("what");
        //                     }
        //                 }
        //             })
        //            .WarmupCount(3)
        //            .MeasurementCount(10)
        //            .IterationsPerMeasurement(1)
        //            .Run();
        // }

//         public DictionaryBenchmark()
//         {
//             UnityEngine.Random.InitState(123456);
//             randomIndices = new uint[dictionarySize];
//             for (int i = 0; i < dictionarySize; i++) randomIndices[i] = (uint) UnityEngine.Random.Range(0, dictionarySize);
//             fasterDictionary = new FasterDictionary<uint, Test>(dictionarySize);
// //            spanDictionary = new SpanDictionary<uint, Test>(dictionarySize, new ManagedAllocationStrategy());
//             sveltoDictionary = new SveltoDictionary<uint, Test>(dictionarySize, new ManagedStrategy<Test>());
//   //          sveltoDictionaryGenerics = new SveltoDictionaryGeneric<uint, Test, ManagedStrategy<Test>>(dictionarySize);
//     //        sveltoNativeDictionaryGenerics = new SveltoDictionaryGeneric<uint, Test, NativeStrategy<Test>>(dictionarySize);
//             standardDictionary = new Dictionary<uint, Test>(dictionarySize);
//         }
//
//         const    int                          dictionarySize = 1_000_000;
//         readonly uint[] randomIndices;
//         
//         readonly FasterDictionary<uint, Test>                               fasterDictionary;
//       //  SpanDictionary<uint, Test>                                          spanDictionary;
//         readonly SveltoDictionary<uint, Test>                               sveltoDictionary;
//         readonly Dictionary<uint, Test>                                     standardDictionary;
// //        readonly SveltoDictionaryGeneric<uint, Test, ManagedStrategy<Test>> sveltoDictionaryGenerics;
//   //      readonly SveltoDictionaryGeneric<uint, Test, NativeStrategy<Test>>  sveltoNativeDictionaryGenerics;
//
//         struct Test
//         {
//             public int a;
//             int b;
//             int c;
//             int d;
//             public Test(int i):this() { a = i; }
//         }
    }
}
