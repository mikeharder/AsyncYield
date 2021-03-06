using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncYield
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            await Test(Sleep, "Sleep");
            await Test(async () => { await Task.Yield(); await Sleep(); }, "YieldThenSleep");
            await Test(() => Task.Run(Sleep), "TaskRunSleep");
            await Test(NoOpLoop, "NoOpLoop");
            await Test(() => Task.Run(NoOpLoop), "TaskRunNoOpLoop");
        }

        private static async Task Test(Func<Task> func, string description)
        {
            Console.WriteLine(description);

            var sw = Stopwatch.StartNew();

            Log("Before task1", sw);
            var task1 = func();

            Log("Before task2", sw);
            var task2 = func();

            Log("Before await", sw);
            await Task.WhenAll(task1, task2);
            Log("After await", sw);

            Console.WriteLine();
        }

        private static async Task Sleep()
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        private static async Task NoOpLoop()
        {
            var startTime = DateTime.Now;
            Console.WriteLine($"startTime: {startTime:hh:mm:ss.fff}");
            while ((DateTime.Now - startTime) < TimeSpan.FromSeconds(1))
            {
                await NoOp();
            }
        }

        private static async Task NoOp()
        {
        }

        private static void Log(string message, Stopwatch stopwatch)
        {
            Console.WriteLine($"[{stopwatch.Elapsed.TotalSeconds}] {message}");
        }
    }
}
