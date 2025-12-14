using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http; // if you want to get a data from internet use HTTP

class asyncawait
{
    /* async keyword is an modifier is used to specify that a method is asynchronous,
     * lambda expressions, and anonymous method.
     * await keyword suspends evaluation of the enclosing async method
     * until the async method is complete, once we are done waiting for an operation
     * to finish, we will get the result of awaitable task.
     */

    static String filePath = @"D:\roadmap\CsharpToAspNet\dog.txt";

    static async Task Main(string[] args)
    {
        string url = "https://raw.githubusercontent.com/13oxer/Doggo/main/README.md";

        // Start both tasks
        Task localTask = SummonDogLocally();
        //Task urlTask = SummonDogFromUrl(url);

        // Wait for both to finish
        await Task.WhenAll(localTask);

        Console.WriteLine("Checking wallet...");
        CheckWallet(); // fast, synchronous

        Console.WriteLine("Placing coffee order...");
        await PlaceOrderAsync(); // slow, asynchronous

        Console.WriteLine("Coffee is ready! Enjoy ☕");
    }

    //add a <String> keyword beside Task if you want to return
    //because the default method is void
    static async Task SummonDogLocally()
    {
        Console.WriteLine("1. SUmmoning Dog Locally ...");

        //read all the text inside the dog.txt async
        string dogText = await File.ReadAllTextAsync(filePath);

        //display the data inside the txt file
        Console.WriteLine($"2. Dog Summoned LOcally \n{dogText}");
    }

    //A Task return type will eventually yield a void
    static async Task SummonDogFromUrl(string url)
    {
        Console.WriteLine("1. SUmmoning Dog from URL ...");

        using(var httpClient = new HttpClient())
        {
            string result = await httpClient.GetStringAsync(url);

            /*from this line and below, the execution will resume once the above 
             * is done, using await keyword, it will do the magic of unwrapping
             * the Task<string> into string(result variable)
             */

            Console.WriteLine($"2. Dog Summoned from URL \n{result}");
        }

    }

    static void CheckWallet()
    {
        Console.WriteLine("Wallet has enough balance.");
    }

    static async Task PlaceOrderAsync()
    {
        using (var httpClient = new HttpClient())
        {
            // simulate calling coffee shop API
            await Task.Delay(3000); // pretend network delay
            Console.WriteLine("Order confirmed from coffee shop API!");
        }
    }


}
