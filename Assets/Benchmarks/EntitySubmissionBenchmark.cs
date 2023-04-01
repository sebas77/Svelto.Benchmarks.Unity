using NUnit.Framework;
using Svelto.ECS;
using Svelto.ECS.Schedulers;
using Unity.PerformanceTesting;

namespace Tests
{
    // WarmupCount(int n) - number of times to to execute before measurements are collected. If unspecified, a default warmup is executed. This default warmup will wait for 100 ms. However, if less than 3 method executions have finished in that time, the warmup will wait until 3 method executions have completed.
    // MeasurementCount(int n) - number of measurements to capture. If not specified default value is 9.
    // IterationsPerMeasurement(int n) - number of method executions per measurement to use. If this value is not specified, the method will be executed as many times as possible until approximately 100 ms has elapsed.
    // SampleGroup(string name) - by default the measurement name will be "Time", this allows you to override it
    // GC() - if specified, will measure the total number of Garbage Collection allocation calls.
    // SetUp(Action action) - is called every iteration before executing the method. Setup time is not measured.
    // CleanUp(Action action) - is called every iteration after the execution of the method. Cleanup time is not measured
    
    public class EntitySubmissionBenchmark
    {
        string[] markers =
        {
            "Add operations"
        };
        
        [Test, Performance]
        public void TestEntitySubmissionPerformance()
        {
            SimpleEntitiesSubmissionScheduler scheduler = new SimpleEntitiesSubmissionScheduler();
            
            EnginesRoot    enginesRoot = default;
            IEntityFactory entityFactory = default;
            
            enginesRoot   = new EnginesRoot(scheduler);

            
            Measure.Method(() =>
            {
                using (Measure.Scope("add 10000 empty entities"))
                {
                    for (uint i = 0; i < 10000; i++)
                        entityFactory.BuildEntity<EntityDescriptor>(i, TestGroups.Group);
                }

                using (Measure.ProfilerMarkers(markers))
                {
                    using (Measure.Scope("submit 10000 empty entities"))
                    {
                        scheduler.SubmitEntities();
                    }
                }
            }).WarmupCount(5).MeasurementCount(10).SetUp(() =>
                {
                    entityFactory = enginesRoot.GenerateEntityFactory();
                    
                    entityFactory.PreallocateEntitySpace<EntityDescriptor>(TestGroups.Group, 1000);
                }
                ).Run();
            
            enginesRoot.Dispose();
            enginesRoot   = new EnginesRoot(scheduler);
            
            Measure.Method(() =>
            {
                using (Measure.Scope("add 10000 empty entities over 10 groups"))
                {
                    for (uint i = 0; i < 10000; i++)
                        entityFactory.BuildEntity<EntityDescriptor>(i, TestGroups.Group + i % 10);
                }
                
                using (Measure.Scope("Add 10000 empty entities over 10 groups"))
                {
                    scheduler.SubmitEntities(); 
                }
            }).WarmupCount(5).MeasurementCount(10).SetUp(() =>
                {
                    entityFactory = enginesRoot.GenerateEntityFactory();
                    
                    for (int i = 0; i < 10; i++)
                        entityFactory.PreallocateEntitySpace<EntityDescriptor>(TestGroups.Group + (uint) i, 100);
                   
                }
            ).Run();
            
            enginesRoot.Dispose();
        }
    }

    public class TestGroups
    {
        public static ExclusiveGroup Group = new ExclusiveGroup(10);
    }

    public class EntityDescriptor: GenericEntityDescriptor<TestStruct> { }

    public struct TestStruct : IEntityComponent { }
}