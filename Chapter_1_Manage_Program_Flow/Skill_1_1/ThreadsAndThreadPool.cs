using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace Chapter_1_Manage_Program_Flow.Skill_1_1
{
    public class ThreadsAndThreadPool
    {
        [Test]
        public void CreateAThread()
        {
            var thread = new Thread(ThreadHello);
            thread.Start();
        }

        [Test]
        public void LambdaExpressionOfThread()
        {
            Thread thread = new Thread(() =>
            {
                Console.WriteLine("Hello from the thread");
                Thread.Sleep(1000);
            });
            thread.Start();
        }

        [Test]
        public void PassingDataIntoThread()
        {
            Thread thread = new Thread(WorkOnData);
            thread.Start(99);
        }

        public void ThreadHello()
        {
            Console.WriteLine("Hello from the thread");
            Thread.Sleep(2000);
        }

        public void WorkOnData(object data)
        {
            Console.WriteLine("Working on: {0}", data);
            Thread.Sleep(1000);
        }
    }
}
