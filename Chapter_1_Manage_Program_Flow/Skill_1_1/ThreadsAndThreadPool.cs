using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace Chapter_1_Manage_Program_Flow.Skill_1_1
{
    public class ThreadsAndThreadPool
    {
        static bool tickRunning;
        public static ThreadLocal<Random> RandomGenerator =
            new ThreadLocal<Random>(() =>
            {
                return new Random(2);
            });
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

        [Test]
        public void AbortAThread()
        {

            tickRunning = true;
            Thread tickThread = new Thread(() =>
            {
                while (tickRunning)
                {
                    Console.WriteLine("Tick");
                    Thread.Sleep(1000);
                }
            });
            tickThread.Start();
            tickRunning = false;
        }

        [Test]
        public void ThreadLocal()
        {
            Thread t1 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("t1: {0}", RandomGenerator.Value.Next(10));
                    Thread.Sleep(500);
                }
            });
            Thread t2 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("t2: {0}", RandomGenerator.Value.Next(10));
                    Thread.Sleep(500);
                }
            });
            t1.Start();
            t2.Start();
        }

        [Test]
        public void ThreadExecutionContext()
        {
            DisplayThread(Thread.CurrentThread);
        }

        public void DisplayThread(Thread t)
        {
            Console.WriteLine("Name: {0}", t.Name);
            Console.WriteLine("Culture: {0}", t.CurrentCulture);
            Console.WriteLine("Priority: {0}", t.Priority);
            Console.WriteLine("Context: {0}", t.ExecutionContext);
            Console.WriteLine("IsBackground?: {0}", t.IsBackground);
            Console.WriteLine("IsPool?: {0}", t.IsThreadPoolThread);
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
