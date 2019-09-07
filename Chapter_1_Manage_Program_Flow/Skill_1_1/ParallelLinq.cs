using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using NUnit.Framework;

namespace Chapter_1_Manage_Program_Flow.Skill_1_1
{
    public class ParallelLinq
    {
        public Person[] TestData;
        [SetUp]
        public void Setup()
        {
            this.TestData = InitTestData();
        }

        /*
         *  The AsParallel method examines the query to determine if using a parallel version would speed it up.
         *  If it is decided that executing elements of the query in parallel would improve performance,
         *  the query is broken down into a number of processes and each is run concurrently.
         *  If the AsParallel method can’t decide whether parallelization would improve performance the
         *  query is not executed in parallel.
         */
        [Test]
        public void NormalParallelLinqSample()
        {
            var result = from person in TestData.AsParallel() where person.City == "Seattle" select person;
            foreach (var person in result)
                Console.WriteLine(person.Name);
        }

        /*
         *  Force parallel execution and assign degree of parallelism
         */
        [Test]
        public void ParallelLinqWithInformingParallelization()
        {
            var result =
                from person in TestData.AsParallel().WithDegreeOfParallelism(4)
                    .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                where person.City == "Seattle"
                select person;
            foreach (var person in result)
                Console.WriteLine(person.Name);
        }

        /*
         *  The AsOrdered method doesn’t prevent the parallelization of the query, instead it organizes
         *  the output so that it is in the same order as the original data.
         */
        [Test]
        public void ParallelLinqWithOrderedOutput()
        {
            var result = from person in TestData.AsParallel().AsOrdered() where person.City == "Seattle" select person;
            foreach (var person in result)
                Console.WriteLine(person.Name);
        }

        /*
         * The AsSequential method can be used to identify parts of a query that must be sequentially executed
         * AsSequential executes the query in order whereas AsOrdered returns a sorted result but does not necessarily run the query in order.
         */
        [Test]
        public void ParallelLinqWithSequentiallyExecute()
        {
            var result =
                (from person in TestData.AsParallel()
                    where person.City == "Seattle"
                    orderby (person.Name)
                    select new {person.Name}).AsSequential().Take(4);
            foreach (var person in result)
                Console.WriteLine(person.Name);
        }

        /*
         *  The ForAll method can be used to iterate through all of the elements in a query. It differs from
         *  the foreach C# construction in that the iteration takes place in parallel and will start before the query is complete
         */
        [Test]
        public void ParallelLinqWithForAll()
        {
            var result = from person in TestData.AsParallel() where person.City == "Seattle" select person;
            result.ForAll(person => Console.WriteLine(person.Name));
        }

        [Test]
        public void ParallelLinqWithExceptions()
        {
            Assert.Throws<AggregateException>(() =>
            {
                var result = from person in
                        TestData.AsParallel()
                    where CheckCity(person.City)
                    select person;
                result.ForAll(person => Console.WriteLine(person.Name));
            });
        }


        private Person[] InitTestData()
        {
            var people = new[] {
                new Person { Name = "", City = "" },
                new Person { Name = "Beryl", City = "Seattle" },
                new Person { Name = "Charles", City = "London" },
                new Person { Name = "David", City = "Seattle" },
                new Person { Name = "Eddy", City = "Paris" },
                new Person { Name = "Fred", City = "Berlin" },
                new Person { Name = "Gordon", City = "Hull" },
                new Person { Name = "Henry", City = "Seattle" },
                new Person { Name = "Isaac", City = "Seattle" },
                new Person { Name = "James", City = "London" }};
            return people;
        }

        private bool CheckCity(string name)
        {
            if (name == "")
                throw new ArgumentException(name);
            return name == "Seattle";
        }

    }

    public class Person
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
}
