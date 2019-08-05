using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Shouldly;

namespace Chapter_1_Manage_Program_Flow.Skill_1_1
{
    public class TheTaskParallelLibrary
    {
        public string SampleContentForParallelInvoke = "";
        public string SampleContentForParallelForEach = "";
        public string SampleContentForParallelFor = "";
        public string SampleContentForLoopState = "";

        [SetUp]
        public void Setup()
        {
        }
        
        //Test for Parallel Invoke
        [Test]
        public void TestForParallelInvoke()
        {
            Parallel.Invoke(TaskForParallelInvoke_1, TaskForParallelInvoke_2);
            SampleContentForParallelInvoke.ShouldBeOneOf(new string[]
            {
                "1342",
                "3142"
            });
        }

        [Test]
        public void TestForParallelForEach()
        {
            var items = Enumerable.Range(0, 5);
            Parallel.ForEach(items, TaskForParallelForEach );
            TestContext.Out.WriteLine(SampleContentForParallelForEach);
            SampleContentForParallelForEach.ShouldNotBeNull();
        }

        [Test]
        public void TestForParallelFor()
        {
            var items = Enumerable.Range(0, 3).ToArray();
            Parallel.For(0, items.Length, TaskForParallelFor);
            TestContext.Out.WriteLine(SampleContentForParallelFor);
            SampleContentForParallelForEach.ShouldNotBeNull();
        }

        [Test]
        public void TestForParallelLoopState()
        {
            var items = Enumerable.Range(0, 10).ToArray();
            var result = Parallel.For(0, items.Length, (i, state) =>
            {
                if (i == 5)
                {
                    state.Stop();
                }
                TaskForParallelLoopState(i);
            } );
            TestContext.Out.WriteLine("Complete : " + result.IsCompleted);
            TestContext.Out.WriteLine("Items: " + result.LowestBreakIteration);
            SampleContentForParallelForEach.ShouldNotBeNull();
        }

        private void TaskForParallelInvoke_1()
        {
            SampleContentForParallelInvoke += "1";
            Thread.Sleep(2000);
            SampleContentForParallelInvoke += "2";
        }

        private void TaskForParallelInvoke_2()
        {
            SampleContentForParallelInvoke += "3";
            Thread.Sleep(1000);
            SampleContentForParallelInvoke += "4";
        }

        private void TaskForParallelForEach(int number)
        {
            SampleContentForParallelForEach += $"Session_{number}_Start ";
            TestContext.Out.WriteLine($"Session_{number}_Start ");
            Thread.Sleep(100);
            TestContext.Out.WriteLine($"Session_{number}_End ") ;
            SampleContentForParallelForEach += $"Session_{number}_End ";
        }

        private void TaskForParallelFor(int number)
        {
            SampleContentForParallelFor += $"Session_{number}_Start ";
            TestContext.Out.WriteLine($"Session_{number}_Start ");
            Thread.Sleep(100);
            TestContext.Out.WriteLine($"Session_{number}_End ");
            SampleContentForParallelFor += $"Session_{number}_End ";
        }

        private void TaskForParallelLoopState(int number)
        {
            SampleContentForLoopState += $"Session_{number}_Start ";
            TestContext.Out.WriteLine($"Session_{number}_Start ");
            Thread.Sleep(100);
            TestContext.Out.WriteLine($"Session_{number}_End ");
            SampleContentForLoopState += $"Session_{number}_End ";
        }

    }
}
