using System.Collections;
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
        [SetUp]
        public void Setup()
        {
        }
        
        //Test for Parallel Invoke
        [Test]
        public void CodeForParallelInvoke()
        {
            Parallel.Invoke(TaskForParallelInvoke_1, TaskForParallelInvoke_2);
            SampleContentForParallelInvoke.ShouldBeOneOf(new string[]
            {
                "1342",
                "3142"
            });
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


    }
}
