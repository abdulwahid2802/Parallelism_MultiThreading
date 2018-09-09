using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parallellism1.TaskCreating
{
    class MainClass
    {
        public static void Main(string[] args)
        {


            //+++++ Cancelling tasks +++++++//

            // Cancellation token to pass into task
            var cts = new CancellationTokenSource();
            var token = cts.Token;


            // Subscribe to token to wait for cancellations to get notified
            token.Register(()=>
            {
                Console.WriteLine("Cancellation has been requested...");
            });

            var task = new Task(
                ()=>
            {
                for (int i = 0; ; i++)
                {

                    //+++++ METHOD 1 of cancelling +++++//
                    if (token.IsCancellationRequested)
                        throw new OperationCanceledException();
                    else
                        Console.WriteLine(i);

                    //+++++ METHOD 2 of cancelling +++++//
                    // merges 'if and throw' statements
                    //token.ThrowIfCancellationRequested();
                    //Console.WriteLine(i);
                }
            }, token);

            task.Start();

            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle has been released, cancellation requested...");
            });

            // Sends the cancellation request when key pressed
            Console.ReadKey();
            cts.Cancel();



            //+++++ Complex Tokens +++++++//


            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            // Links the tokens
            var paranoid = CancellationTokenSource.CreateLinkedTokenSource
                                                  (
                                                      planned.Token,
                                                      preventative.Token,
                                                      emergency.Token
                                                     );

            Task.Factory.StartNew(() =>
            {
                for (int i = 0; ; i++)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i}");
                    Thread.Sleep(1000);
                }
            });


            Console.ReadKey();
            // now we can call cancellation using any token
            emergency.Cancel();




        }
    }
}
