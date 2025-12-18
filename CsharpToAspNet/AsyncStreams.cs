using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class AsyncStreams
{
    // PRIME CHECKER
    static class IsPrimeComputer
    {
        public static bool IsPrime(int number)
        {
            if (number < 2) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            int boundary = (int)Math.Sqrt(number);
            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }

    // RESPONSE MODEL
    public record NumbersResponse(List<int> Numbers, string? NextPageToken);

    // SIMULATED API CLIENT
    public static class NumbersClient
    {
        public const int NumbersInStock = 100;
        public const int MaxPageSize = 10;

        public static async Task<NumbersResponse> ListNumberAsync(
            int pageSize = 10,
            string? pageToken = null)
        {
            await Task.Delay(500); // async, non-blocking

            pageSize = Math.Min(pageSize, MaxPageSize);
            int start = pageToken == null ? 0 : int.Parse(pageToken);
            int end = Math.Min(start + pageSize, NumbersInStock);

            return new NumbersResponse(
                Enumerable.Range(start, end - start).ToList(),
                end < NumbersInStock ? end.ToString() : null
            );
        }
    }

    //ASYNC STREAM
    static async IAsyncEnumerable<int> ListAllNumbersAsync()
    {
        string? pageToken = null;

        do
        {
            var response = await NumbersClient.ListNumberAsync(pageToken: pageToken);
            pageToken = response.NextPageToken;

            foreach (var number in response.Numbers)
            {
                yield return number; // streamed one-by-one
            }

            Console.WriteLine($"Received {response.Numbers.Count} numbers");

        } while (pageToken != null);
    }
    static async Task Main()
    {


        int count = 0;

        await foreach (var number in ListAllNumbersAsync())
        {
            if (IsPrimeComputer.IsPrime(number))
            {
                Console.WriteLine(number);
                count++;
            }

            if (count == 5)
                break;
        }

    }
}
