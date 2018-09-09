using System;
using System.Threading.Tasks;

namespace Parallellism1.TaskCreating
{
    class MainClass
    {
        public static void Write(string someString)
        {
            for (int i = 1; i <= 1000;i++)
                Console.Write(someString);
        }

        public static void Write(object o)
        {
            for (int i = 1; i <= 1000; i++)
                Console.Write(o);
        }

        public static int GetLength(object o)
        {
            Console.WriteLine($"The process with id {Task.CurrentId} is processing {o}");
            return o.ToString().Length;
        }

        public static void Main(string[] args)
        {


            //+++++ METHOD 1 +++++++//

            // Directly starts the task
            Task.Factory.StartNew(() => Write("Method 1.1"));

            // Make a new task variable first and start
            var t = new Task(() => Write("Method 1.2"));
            t.Start();


            //+++++ METHOD 1 +++++++//


            Task t2 = new Task(Write, "Method 2.1");
            t2.Start();

            Task.Factory.StartNew(Write, "Method 2.2");



            //+++++ RETURNING VALUE +++++++//

            var task1 = new Task<int>(GetLength, "This is some string");
            task1.Start();

            Task<int> task2 = Task.Factory.StartNew(GetLength, "This is another string");


            Console.WriteLine($"Result of task1 {task1.Result}");
            Console.WriteLine($"Result of task2 {task2.Result}");


            // Running the Write method on main thread
            Write("Salom");

            Console.WriteLine("Hello World!");
        }
    }
}
