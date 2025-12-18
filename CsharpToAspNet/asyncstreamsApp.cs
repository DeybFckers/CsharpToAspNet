using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

class AsyncStockFetcher
{
    static async Task Main()
    {
        var companies = new List<string> { "AAPL", "MSFT", "GOOG", "AMZN", "TSLA" };
        string filePath = @"D:\roadmap\CsharpToAspNet\stocks.csv";

        Console.WriteLine("Fetching stock prices...\n");

        // Start fetching prices in parallel
        var priceTasks = companies.Select(c => GetStockPriceAsync(c)).ToList();

        // Stream results as they arrive
        await foreach (var result in StreamPricesAsync(priceTasks, companies))
        {
            Console.WriteLine($"{result.Company}: {result.Price:F2}");
        }

        // Await all to get final list
        var allPrices = await Task.WhenAll(priceTasks);

        // Calculate metrics asynchronously
        double average = await Task.Run(() => allPrices.Average());
        double max = await Task.Run(() => allPrices.Max());
        double min = await Task.Run(() => allPrices.Min());

        Console.WriteLine($"\nAverage Price: {average:F2}");
        Console.WriteLine($"Highest Price: {max:F2}");
        Console.WriteLine($"Lowest Price: {min:F2}");

        // Save results to CSV asynchronously
        await SaveToCsvAsync(companies, allPrices, filePath);
        Console.WriteLine($"\nResults saved to {filePath}");
    }

    // Simulated async API
    static async Task<double> GetStockPriceAsync(string company)
    {
        var random = new Random();
        await Task.Delay(random.Next(500, 2000)); // simulate network delay
        return random.NextDouble() * 3000 + 100;   // random stock price
    }

    // Async stream to yield results as they complete
    static async IAsyncEnumerable<(string Company, double Price)> StreamPricesAsync(
        List<Task<double>> tasks, List<string> companies)
    {
        var remainingTasks = tasks.ToList();
        var remainingCompanies = companies.ToList();

        while (remainingTasks.Count > 0)
        {
            var finished = await Task.WhenAny(remainingTasks);
            int index = remainingTasks.IndexOf(finished);
            double price = await finished;

            yield return (remainingCompanies[index], price);

            remainingTasks.RemoveAt(index);
            remainingCompanies.RemoveAt(index);
        }
    }

    // Async file save
    static async Task SaveToCsvAsync(List<string> companies, double[] prices, string filePath)
    {
        using var writer = new StreamWriter(filePath, append: false);
        await writer.WriteLineAsync("Company,Price");

        for (int i = 0; i < companies.Count; i++)
        {
            await writer.WriteLineAsync($"{companies[i]},{prices[i]:F2}");
        }
    }
}
