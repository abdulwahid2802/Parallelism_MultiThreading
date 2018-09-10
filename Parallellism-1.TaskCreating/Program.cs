using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parallellism1.TaskCreating
{
    class MainClass
    {
        public static void Main(string[] args)
        {


            //+++++ Exception Handling +++++++//

            var task_1 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("You do not have permission!") { Source = "task_1" };
            });

            var task_2 = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("You cannot do this!") { Source = "task_2" };
            });


            try
            {
                Task.WaitAll(task_1, task_2);
            }
            catch(AggregateException ae)
            {
                foreach(var e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Exception {e.GetType()} from {e.Source} has been thrown...");
                }
            }


            Console.WriteLine("Main Thread is done!");

        }
    }
}
