using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parallellism1.TaskCreating
{
    class MainClass
    {
        public static void Main(string[] args)
        {


            //+++++ Waiting some time +++++++//

            var cts = new CancellationTokenSource();
            var token = cts.Token;

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("You have 5 seconds to disarm the bomb!");

                Console.WriteLine(token.WaitHandle.WaitOne(5000) ? "You have disarmed..." : "BOOM!!!");
            }, token);


            Console.ReadKey();
            cts.Cancel();

        }
    }
}
