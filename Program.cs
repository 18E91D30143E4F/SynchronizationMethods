using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace SynchronizationMethods
{
    class Program
    {
        internal static int Var = 20;

        static void Main(string[] args)
        {
            Console.WriteLine("Kill std handle? y/n");

            TestMutexThreads();

            if (Console.ReadLine() == "y")
            {
                CloseStdHandle();
            }

            Console.ReadKey();

        }


        private const uint STD_INPUT_HANDLE = 0xfffffff6;
        [DllImport("kernel32.dll")]
        private static extern int GetStdHandle(uint nStdHandle);
        private static void CloseStdHandle()
        {
            var osHandle = new OsHandle
            {
                Handle = (IntPtr)GetStdHandle(STD_INPUT_HANDLE)
            };
            osHandle.Close();
        }

        private static void TestMutexThreads()
        {
            const int n = 5;
            var mutex = new Mutex();
            var Threads = Enumerable.Range(0, n)
                .Select(i =>
                {
                    var thread = new Thread(() =>
                    {
                        var threadId = mutex.Lock();
                        Console.WriteLine($"Thread {threadId } lock");
                        Var++;
                        Thread.Sleep(400);
                        Console.WriteLine($"Thread { threadId } unlock");
                        mutex.Unlock();
                    })
                    {
                        Name = $"Thread {i}"
                    };
                    thread.Start();

                    return thread;
                }).ToList();
        }
    }
}
