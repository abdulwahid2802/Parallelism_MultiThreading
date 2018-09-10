using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parallellism1.TaskCreating
{
    class MainClass
    {
        public static void Main(string[] args)
        {


            //+++++ Waiting for tasks +++++++//

            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var task_1 = new Task(() =>
            {
                for (int i = 0; i < 5;i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Task 1 is done!");
            }, token);
            task_1.Start();

            var task_2 = new Task(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Task 2 is done!");
            }, token);
            task_2.Start();

            // .WaitAny() waits for any thread  that is passed to it to finish
            //Task.WaitAny(task_1, task_2);

            // .WaitAll() waits for the longest thread to finish
            //Task.WaitAll(task_1, task_2);


            // pass multiple tasks as an array and a time to wait for
            Task.WaitAny(new[] { task_1, task_2 }, 4000, token);

            // Lets look at the thread status
            Console.WriteLine($"Task 1 status is {task_1.Status}");
            Console.WriteLine($"Task 2 status is {task_2.Status}");

            Console.WriteLine("Main Thread is done!");

        }
    }
}
