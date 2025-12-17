using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#region TPL OVERVIEW
/*
 * =========================================================
 * TASK PARALLEL LIBRARY (TPL) – COMPLETE LEARNING FILE
 * =========================================================
 *
 * - Tasks run on thread pool threads
 * - Faster sleep = finishes earlier
 * - Slower sleep = finishes later
 * - Order of execution is NOT guaranteed
 * - Includes basic and advanced functions
 * =========================================================
 */
#endregion

class TaskParallelLibrary
{
    #region MAIN METHOD
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== TPL LEARNING DEMO ===\n");

        #region 1_BASIC_TASK_AND_CONTINUATION
        var t1 =
            Task.Factory.StartNew(() => DoSomeVeryImportantWork(1, 1500)) // finishes SECOND
                .ContinueWith(t => DoSomeOtherVeryImportantWork(1, 1000)); // continuation finishes FIRST

        var t2 =
            Task.Factory.StartNew(() => DoSomeVeryImportantWork(2, 3000)) // finishes LAST
                .ContinueWith(t => DoSomeOtherVeryImportantWork(2, 2000)); // continuation finishes SECOND

        var t3 =
            Task.Factory.StartNew(() => DoSomeVeryImportantWork(3, 1000)) // finishes FIRST
                .ContinueWith(t => DoSomeOtherVeryImportantWork(3, 5000)); // continuation finishes LAST

        Task.WaitAll(t1, t2, t3); // WaitAll blocks until all tasks + continuations finish
        #endregion

        #region 2_PARALLEL_API_CALLS
        Console.WriteLine("\n--- Parallel API Calls ---");
        var userTask = Task.Run(() => GetUser());
        var orderTask = Task.Run(() => GetOrders());
        var paymentTask = Task.Run(() => GetPayments());
        Task.WaitAll(userTask, orderTask, paymentTask); // WaitAll blocks main thread
        #endregion

        #region 3_PARALLEL_DATABASE_OPERATIONS
        Console.WriteLine("\n--- Parallel DB Operations ---");
        var dbTasks = new[]
        {
            Task.Run(() => InsertLog()),
            Task.Run(() => UpdateStatistics()),
            Task.Run(() => SaveAuditTrail())
        };
        Task.WaitAll(dbTasks);
        #endregion

        #region 4_TASK_CONTINUATION_ORDER_TO_EMAIL
        Console.WriteLine("\n--- Continuation: Save Order → Email ---");
        Task.Run(() => SaveOrder())
            .ContinueWith(t => SendEmail(), TaskContinuationOptions.OnlyOnRanToCompletion) // advanced option
            .Wait();
        #endregion

        #region 5_PARALLEL_DATA_PROCESSING
        Console.WriteLine("\n--- Parallel Data Processing ---");
        var records = new List<int> { 1, 2, 3, 4, 5 };
        Parallel.ForEach(records, record => ProcessRecord(record));
        #endregion

        #region 6_TASK_WHENALL_ASYNC_AWAIT
        Console.WriteLine("\n--- Task.WhenAll ---");
        var services = new[]
        {
            Task.Run(() => ServiceA()),
            Task.Run(() => ServiceB()),
            Task.Run(() => ServiceC())
        };
        await Task.WhenAll(services); // non-blocking
        #endregion

        #region 7_TASK_WHENANY
        Console.WriteLine("\n--- Task.WhenAny ---");
        var taskA = Task.Run(() => ServiceA());
        var taskB = Task.Run(() => ServiceB());
        var firstFinished = await Task.WhenAny(taskA, taskB); // returns first completed task or it means any task who ever finished first
        Console.WriteLine($"First finished task status: {firstFinished.Status}");
        #endregion

        #region 8_TASK_DELAY
        Console.WriteLine("\n--- Task.Delay (non-blocking wait) ---");
        await Task.Delay(1000); // does not block main thread
        Console.WriteLine("1 second delay completed");
        #endregion

        #region 9_PARALLEL_INVOKE
        Console.WriteLine("\n--- Parallel.Invoke ---");
        Parallel.Invoke(
            () => ProcessRecord(101),
            () => ProcessRecord(102),
            () => ProcessRecord(103)
        );
        #endregion

        #region 10_TASK_COMPLETION_SOURCE
        Console.WriteLine("\n--- TaskCompletionSource Example ---");
        var tcs = new TaskCompletionSource<int>();
        Task.Run(() =>
        {
            Thread.Sleep(1000);
            tcs.SetResult(42); // manually complete the task
        });
        int result = await tcs.Task; // await result
        Console.WriteLine($"TaskCompletionSource returned: {result}");
        #endregion

        #region 11_FIRE_AND_FORGET
        Console.WriteLine("\n--- Fire-and-Forget Task ---");
        Task.Run(() => BackgroundLog());
        Console.WriteLine("Request finished (background task still running)");
        #endregion

        Console.WriteLine("\n=== END OF TPL DEMO ===");
        Console.ReadKey();
    }
    #endregion

    #region WORK_SIMULATION_METHODS

    #region CORE_WORK
    static void DoSomeVeryImportantWork(int id, int sleepTime)
    {
        Console.WriteLine($"Task {id} starting");
        Thread.Sleep(sleepTime);
        Console.WriteLine($"Task {id} finished");
    }

    static void DoSomeOtherVeryImportantWork(int id, int sleepTime)
    {
        Console.WriteLine($"Task {id} continuation starting");
        Thread.Sleep(sleepTime);
        Console.WriteLine($"Task {id} continuation finished");
    }
    #endregion

    #region API_SIMULATION
    static void GetUser()
    {
        Thread.Sleep(2000);
        Console.WriteLine("User API done");
    }

    static void GetOrders()
    {
        Thread.Sleep(3000);
        Console.WriteLine("Order API done");
    }

    static void GetPayments()
    {
        Thread.Sleep(1000);
        Console.WriteLine("Payment API done");
    }
    #endregion

    #region DATABASE_SIMULATION
    static void InsertLog()
    {
        Thread.Sleep(1500);
        Console.WriteLine("Log inserted");
    }

    static void UpdateStatistics()
    {
        Thread.Sleep(2000);
        Console.WriteLine("Statistics updated");
    }

    static void SaveAuditTrail()
    {
        Thread.Sleep(1000);
        Console.WriteLine("Audit trail saved");
    }
    #endregion

    #region ORDER_AND_EMAIL
    static void SaveOrder()
    {
        Thread.Sleep(2000);
        Console.WriteLine("Order saved");
    }

    static void SendEmail()
    {
        Thread.Sleep(1000);
        Console.WriteLine("Email sent");
    }
    #endregion

    #region DATA_PROCESSING
    static void ProcessRecord(int id)
    {
        Thread.Sleep(1000);
        Console.WriteLine($"Record {id} processed");
    }
    #endregion

    #region SERVICES
    static void ServiceA()
    {
        Thread.Sleep(1000);
        Console.WriteLine("Service A done");
    }

    static void ServiceB()
    {
        Thread.Sleep(2000);
        Console.WriteLine("Service B done");
    }

    static void ServiceC()
    {
        Thread.Sleep(1500);
        Console.WriteLine("Service C done");
    }
    #endregion

    #region BACKGROUND_TASK
    static void BackgroundLog()
    {
        Thread.Sleep(3000);
        Console.WriteLine("Background log saved");
    }
    #endregion

    #endregion
}
