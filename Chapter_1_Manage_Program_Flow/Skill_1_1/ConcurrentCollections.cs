using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Chapter_1_Manage_Program_Flow.Skill_1_1
{
    public class ConcurrentCollections
    {
        [Test]
        public void SampleForBlockingCollection()
        {
            BlockingCollection<int> data = new BlockingCollection<int>(5);
            Task.Run(() =>
            {
                // attempt to add 10 items to the collection - blocks after 5th
                for (int i = 0; i < 11; i++)
                {
                    data.Add(i);
                    Console.WriteLine("Data {0} added sucessfully.", i);
                }
                // indicate we have no more to add
                data.CompleteAdding();
            });
            Console.WriteLine("Reading collection");
            Task.Run(() =>
            {
                while (!data.IsCompleted)
                {
                    try
                    {
                        int v = data.Take();
                        Console.WriteLine("Data {0} taken sucessfully.", v);
                    }
                    catch (InvalidOperationException) { }
                }
            });
        }

        [Test]
        public void SampleForConcurrentQueue()
        {
            ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
            queue.Enqueue("Rob");
            queue.Enqueue("Miles");
            if (queue.TryPeek(out var str))
                Console.WriteLine("Peek: {0}", str);
            if (queue.TryDequeue(out str))
                Console.WriteLine("Dequeue: {0}", str);
        }

        [Test]
        public void SampleForConcurrentStack()
        {
            ConcurrentStack<string> stack = new ConcurrentStack<string>();
            stack.Push("Rob");
            stack.Push("Miles");
            if (stack.TryPeek(out var str))
                Console.WriteLine("Peek: {0}", str);
            if (stack.TryPop(out str))
                Console.WriteLine("Pop: {0}", str);
        }

        [Test]
        public void SampleForConcurrentBag()
        {
            ConcurrentBag<string> bag = new ConcurrentBag<string> {"Rob", "Miles", "Hull"};
            if (bag.TryPeek(out var str))
                Console.WriteLine("Peek: {0}", str);
            if (bag.TryTake(out str))
                Console.WriteLine("Take: {0}", str);
        }

        [Test]
        public void SampleForConcurrentDictionary()
        {
            ConcurrentDictionary<string, int> ages = new ConcurrentDictionary<string, int>();
            if (ages.TryAdd("Rob", 21))
                Console.WriteLine("Rob added successfully.");
            Console.WriteLine("Rob's age: {0}", ages["Rob"]);
            // Set Rob's age to 22 if it is 21
            if (ages.TryUpdate("Rob", 22, 21))
                Console.WriteLine("Age updated successfully");
            Console.WriteLine("Rob's new age: {0}", ages["Rob"]);
            // Increment Rob's age atomically using factory method
            Console.WriteLine("Rob's age updated to: {0}",
                ages.AddOrUpdate("Rob", 1, (name, age) => age = age + 1));
            Console.WriteLine("Rob's new age: {0}", ages["Rob"]);
        }
    }
}
