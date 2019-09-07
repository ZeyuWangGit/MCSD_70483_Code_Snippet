using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace Chapter_1_Manage_Program_Flow.Skill_1_1
{
    public class ParallelizationTasks
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateATask()
        {
            var newTask = new Task(DoWork);
            newTask.Start();
            newTask.Wait();
        }

        [Test]
        public void RunATask()
        {
            var newTask = Task.Run(DoWork);
            newTask.Wait();
        }

        [Test]
        public void ReturnAValueFromATask()
        {
            var task = Task.Run(CalculateResult);
            task.Result.ShouldBe(99);
        }


        /*
         *  The Task.Waitall method can be used to pause a program until a number of tasks have completed
         */
        [Test]
        public void TaskWaitAll()
        {
            var tasks = new Task[10];
            for (var i = 0; i < 10; i++)
            {
                var taskNum = i;
                tasks[i] = Task.Run(() => DoWork(taskNum));
            }
            Task.WaitAll(tasks);
        }

        public void DoWork()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
        }

        public void DoWork(int i)
        {
            Console.WriteLine("Task {0} starting", i);
            Thread.Sleep(2000);
            Console.WriteLine("Task {0} finished", i);
        }

        public int CalculateResult()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
            return 99;
        }
    }
}
