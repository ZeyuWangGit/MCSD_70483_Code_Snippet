using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Chapter_1_Manage_Program_Flow.Skill_1_1
{
    public class ContinuationTasks
    {
        /*
         * continuation task can be nominated to start when an existing task (the antecedent task)
         * finishes. If the antecedent task produces a result, it can be supplied as an input to the continuation
         * task.
         */
        [Test]
        public void NormalContinuationTasks()
        {
            var task = Task.Run(HelloTask);
            task.ContinueWith((prevTask) => WorldTask());
        }

        /*
         *  The parent class will not complete until all of the attached child tasks have completed
         */
        [Test]
        public void ChildTasks()
        {
            var parent = Task.Factory.StartNew(() => {
                Console.WriteLine("Parent starts");
                for (var i = 0; i < 10; i++)
                {
                    var taskNo = i;
                    Task.Factory.StartNew(DoChild, taskNo, TaskCreationOptions.AttachedToParent);
                }
            });
            parent.Wait();
        }


        public void HelloTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Hello");
        }
        public void WorldTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("World");
        }

        public void DoChild(object state)
        {
            Console.WriteLine("Child {0} starting", state);
            Thread.Sleep(2000);
            Console.WriteLine("Child {0} finished", state);
        }


    }
}
