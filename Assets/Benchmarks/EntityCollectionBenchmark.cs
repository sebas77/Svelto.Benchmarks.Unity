using NUnit.Framework;
using Svelto.ECS;
using Svelto.ECS.DataStructures;
using Svelto.ECS.Schedulers;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.PerformanceTesting;

namespace Tests
{
    public class EntityCollectionBenchmark
    {
        public struct EntityCollection
        {
            public uint count;
        }

        [BurstCompile]
        struct Job : IJob
        {
            public NativeDynamicArray array;

            public void Execute()
            {
                for (int i = 0; i < 500; i++)
                {
                    var nativeArray = array.Get<NativeArray<uint>>(i);

                    for (int j = 0; j < 1000; j++)
                    {
                        nativeArray[j] = 0;
                    }
                }
            }
        }
        
        [BurstCompile]
        struct Job2: IJob
        {
            public NativeDynamicArray array;

            public void Execute()
            {
                for (uint i = 0; i < 500; i++)
                {
                    var nativeArrayA = array.Get<NativeArray<uint>>(i);
                    var nativeArrayB = array.Get<NativeArray<uint>>(i + 500);

                    for (int j = 0; j < 1000; j++)
                    {
                        nativeArrayA[(int) nativeArrayB[(int) i]] = 0;
                    }
                }
            }
        }
        
        [BurstCompile]
        struct Job3: IJob
        {
            public NativeDynamicArray array;

            public void Execute()
            {
                for (int i = 0; i < 500; i++)
                {
                    var nativeArray = array.Get<NativeArray<uint>>(i);

                    for (int j = 0; j < 1000; j++)
                    {
                        if (i > 500)
                            nativeArray[j] = 0;
                    }
                }
            }
        }
        
        [Test, Performance]
        public void TestEntityCollectionPerformance()
        {
            Measure.Method(() =>
                    {
                       //  EntityCollection<Test> entityCollection =
                         //    ((IUnitTestingInterface) _enginesroot).entitiesForTesting.QueryEntities<Test>(TESTGROUP);
                        //
                        // using (Measure.Scope("Iterate EC"))
                        // {
                        //     for (uint index = 0; index < entityCollection.count; index++)
                        //     {
                        //         entityCollection[index].a = 4;
                        //     }
                        // }
                        // using (Measure.Scope("Iterate Buffer"))
                        // {
                        //     IBuffer<Test> entities = entityCollection.ToBuffer();
                        //
                        //     for (int index = 0; index < entityCollection.count; index++)
                        //     {
                        //         entities[index].a = 4;
                        //     }
                        // }
                        // using (Measure.Scope("Casted Iterate Buffer"))
                        // {
                        //     var entities = entityCollection.ToNativeBuffer<Test>();
                        //
                        //     for (int index =  0; index < entityCollection.count; index++)
                        //     {
                        //         entities.buffer[index].a = 4;
                        //     }
                        // }
                        // using (Measure.Scope("Iterate Native Array"))
                        // {
                        //     unsafe
                        //     {
                        //         var entities = (Test*)entityCollection.ToNativeBuffer<Test>().buffer.ToNativeArray(out _);
                        //     
                        //         for (int index = 0; index < entityCollection.count; index++)
                        //         {
                        //             entities[index].a = 4;
                        //         }
                        //     }
                        // }
                        
                        var entityCollection = new EntityCollection() { count = dictionarySize };
                        var testArray = new Test[dictionarySize];
                        
                        using (Measure.Scope("Iterate Managed Array"))
                        {
                            for (int index = 0; index < entityCollection.count; index++)
                            {
                                testArray[index].a = 4;
                            }
                        }
                        using (Measure.Scope("Iterate Managed Array constant size"))
                        {
                            for (int index = 0; index < dictionarySize; index++)
                            {
                                testArray[index].a = 4;
                            }
                        }
                    })
                   .WarmupCount(3)
                   .MeasurementCount(10)
                   .IterationsPerMeasurement(10)
                   .Run();
           

        }

        [Test, Performance]
        public void TestCache()
        {

            var test = NativeDynamicArray.Alloc<NativeArray<uint>>(Svelto.Common.Allocator.Persistent, 1000);
            for (int i = 0; i < 1000; i++)
                test.Get<NativeArray<uint>>(i) = new NativeArray<uint>(1000, Allocator.Persistent);
            
            Measure.Method(() =>
                    {
                        using (Measure.Scope("TestCache"))
                        {
                            new Job()
                            {
                                array = test
                            }.Run();
                        }
                    }).WarmupCount(3)
                   .MeasurementCount(10)
                   .IterationsPerMeasurement(10)
                   .Run();

            for (int i = 0; i < 1000; i++)
                test.Get<NativeArray<uint>>(i).Dispose();
            test.Dispose();
        }
        
        [Test, Performance]
        public void TestCache3()
        {

            var test = NativeDynamicArray.Alloc<NativeArray<uint>>(Svelto.Common.Allocator.Persistent, 1000);
            for (int i = 0; i < 1000; i++)
                test.Get<NativeArray<uint>>(i) = new NativeArray<uint>(1000, Allocator.Persistent);
            
            Measure.Method(() =>
                    {
                        using (Measure.Scope("TestCache"))
                        {
                            new Job()
                            {
                                array = test
                            }.Run();
                        }
                    }).WarmupCount(3)
                   .MeasurementCount(10)
                   .IterationsPerMeasurement(10)
                   .Run();

            for (int i = 0; i < 1000; i++)
                test.Get<NativeArray<uint>>(i).Dispose();
            test.Dispose();
        }
        
        [Test, Performance]
        public void TestCache2()
        {

            var test = NativeDynamicArray.Alloc<NativeArray<uint>>(Svelto.Common.Allocator.Persistent, 1000);
            for (int i = 0; i < 1000; i++)
                test.Get<NativeArray<uint>>(i) = new NativeArray<uint>(1000, Allocator.Persistent);
       
            for (int i = 500; i < 1000; i++)
            {
                var array = test.Get<NativeArray<uint>>(i);

                for (int j = 0; j < 1000; j++)
                {
                    array[i] = (uint) i;
                }
            }

            Measure.Method(() =>
                    {
                        using (Measure.Scope("TestCache2"))
                        {
                            new Job2()
                            {
                                array = test
                            }.Run();
                        }
                    }).WarmupCount(3)
                   .MeasurementCount(10)
                   .IterationsPerMeasurement(10)
                   .Run();

            for (int i = 0; i < 1000; i++)
                test.Get<NativeArray<uint>>(i).Dispose();
            test.Dispose();
        }
        
        [Test, Performance]
        public void TestParallel()
        {

            var test = new NativeArray<uint>(1000000, Allocator.Temp);

            Measure.Method(() =>
                    {
                        using (Measure.Scope("TestParallelFor"))
                        {
                            new JobParallel()
                            {
                                array = test
                            }.ScheduleParallel(1000000, default).Complete();
                        }
                    }).WarmupCount(3)
                   .MeasurementCount(10)
                   .IterationsPerMeasurement(10)
                   .Run();
            
        }
        
        [Test, Performance]
        public void TestParallel2()
        {

            var test = new NativeArray<uint>(1000000, Allocator.Temp);
            
            Measure.Method(() =>
                    {
                        using (Measure.Scope("TestParallelForBatch"))
                        {
                            new JobParallelBatch()
                            {
                                array = test
                            }.ScheduleBatch(1000000, 64).Complete();
                        }
                    }).WarmupCount(3)
                   .MeasurementCount(10)
                   .IterationsPerMeasurement(10)
                   .Run();
        }

        public EntityCollectionBenchmark()
        {
            var simpleEntitiesSubmissionScheduler = new SimpleEntitiesSubmissionScheduler();
            _enginesroot = new EnginesRoot(simpleEntitiesSubmissionScheduler);
            var factory = _enginesroot.GenerateEntityFactory();

            for (uint i = 0 ; i < dictionarySize; i++)
                factory.BuildEntity<TestEntityDescriptor>(new EGID(i, TESTGROUP));
            
            testArray = new Test[dictionarySize];
            
            simpleEntitiesSubmissionScheduler.SubmitEntities();
        }

        const    int            dictionarySize = 1_000_000;
        readonly Test[]         testArray;
        readonly EnginesRoot             _enginesroot;
        readonly ExclusiveGroup TESTGROUP = new ExclusiveGroup();

        public struct Test:IEntityComponent
        {
            public int a;
            public Test(int i):this() { a = i; }
        }

        public class TestEntityDescriptor : GenericEntityDescriptor<Test> { }
    }

    [BurstCompile]
    public struct JobParallel:IJobParallelFor
    {
        public NativeArray<uint> array;

        public void Execute(int index)
        {
            array[index] = 0;
        }
    }

    [BurstCompile]
    public struct JobParallelBatch:Unity.Jobs.IJobParallelForBatch
    {
        public NativeArray<uint> array;

        public void Execute(int startIndex, int count)
        {
            for (int i = startIndex; i < count; ++i)
                array[i] = 0;
        }
    }
}