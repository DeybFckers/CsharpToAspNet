using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
class TextParser
{
    static void Main(string[] args)
    {
        String filePath = @"D:\roadmap\CsharpToAspNet\txtparser.csv";
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                String line;
                int counter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    counter++;
                    Console.WriteLine(line.Count());
                }
                Console.WriteLine("Total lines: " + counter);
            }
        }catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        var mostCommonWord =
        File.ReadLines(filePath)                 // Read the CSV file line by line
            .SelectMany(line =>                  // Flatten all words from all lines into one sequence
                Regex.Split(
                    line.ToLower(),              // Convert line to lowercase (case-insensitive comparison)
                    @"\W+"                       // Split by non-word characters (commas, spaces, symbols)
                ))
            .Where(word =>                       // Filter the words
                !string.IsNullOrWhiteSpace(word) // Remove empty or whitespace-only entries
            )
            .GroupBy(word => word)               // Group identical words together
            .OrderByDescending(group =>          // Sort groups by frequency (highest first)
                group.Count()
            )
            .First();                            // Take the most frequent word


        Console.WriteLine($"Most common word: {mostCommonWord.Key}");
        Console.WriteLine($"Count: {mostCommonWord.Count()}");

        string columnName = "Sales";                     // Name of the column to total

        var lines = File.ReadLines(filePath).ToList();   // Read all lines from the CSV into a list

        var headers = lines[0].Split(",");               // Split the first line (header row) into column names

        int columnIndex =                                // Find the index of the "Sales" column
            Array.IndexOf(headers, columnName);

        var totalSales =
            lines.Skip(1)                                // Skip the header row
                 .Select(line =>                         // Extract the Sales column value from each row
                     line.Split(",")[columnIndex]
                 )
                 .Where(value =>                         // Keep only valid numeric values
                     double.TryParse(value, out _)
                 )
                 .Sum(value =>                           // Convert each value to double and sum them
                     double.Parse(value)
                 );


        Console.WriteLine("Total Sales: " + totalSales);

        Console.Write("Enter your Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter your Email: ");
        string email = Console.ReadLine();

        Console.Write("Enter your Sales: ");
        double sales = Convert.ToDouble(Console.ReadLine());

        var rec = new Record(name, email, sales);
        Record.AddRecord(rec, filePath);

        Console.ReadKey();
    }
    
    record Record(String name, String email, double sales)
    {
        public string Email
        {
            get => email;
            init
            {
                if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    throw new ArgumentException("Invalid email format.");
                email = value;
            }
        }
        public static void AddRecord(Record r, string filePath)
        {
            try
            {
                File.AppendAllText(filePath, $"{r.name},{r.email},{r.sales}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing record: " + ex.Message);
            }
        }
    }
}
