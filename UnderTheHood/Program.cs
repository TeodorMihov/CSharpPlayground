using System;
using System.Threading.Tasks;
using UnderTheHood.AsyncAwait;

namespace UnderTheHood
{
    // https://www.markopapic.com/csharp-under-the-hood-async-await/
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static async Task AwaitMyawaitable()
        {
            var awaitableObj = new MyAwaitableClass();

            await awaitableObj;
        }

        // Normal c# method
        static async Task FooAsync()
        {
            Console.WriteLine("Async method that doesn't have await");
        }

        //// Decompiled method
        //private static Task FooAsync()
        //{
            // Program.<FooAsync > d__1 stateMachine;
            // stateMachine.<> t__builder = AsyncTaskMethodBuilder.Create();
            // stateMachine.<> 1__state = -1;
            // stateMachine.<> t__builder.Start < Program.< FooAsync > d__1 > (ref stateMachine);
            // return stateMachine.<> t__builder.Task;
        //}

        static async Task BarAsync()
        {
            Console.WriteLine("This happens before await");

            int i = await QuxAsync();

            Console.WriteLine("This happens after await. The result of await is " + i);
        }

        public static async Task<int> QuxAsync()
        {
            return 5;
        }

        //// Decompiled method - same as fooasync
        //private static Task BarAsync()
        //{
        //    Program.< BarAsync > d__2 stateMachine;
        //    stateMachine.<> t__builder = AsyncTaskMethodBuilder.Create();
        //    stateMachine.<> 1__state = -1;
        //    stateMachine.<> t__builder.Start < Program.< BarAsync > d__2 > (ref stateMachine);
        //    return stateMachine.<> t__builder.Task;
        //}
    }
}
