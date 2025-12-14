using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
class TextParser
{
    static String filePath = @"D:\roadmap\CsharpToAspNet\txtparser.csv";
    static void Main(string[] args)
    {
        Console.WriteLine("Crud Text Parser App");

        var num = 0; 
        while(num != 5)
        {
            Console.WriteLine("(1) Create");
            Console.WriteLine("(2) Read");
            Console.WriteLine("(3) Update");
            Console.WriteLine("(4) Delete");
            Console.WriteLine("(5) Exit");

            Console.Write("Select a Option: ");
            num = Convert.ToInt32(Console.ReadLine());

            switch (num)
            {
                case 1:
                    Create();
                    break;
                case 2:
                    Read();
                    break;
                case 3:
                    Uselectedrow();
                    break;
                case 4:
                    Dselectedrow();
                    break;
            }
        }

        
    }

    public static void Create()
    {
        Console.WriteLine("---- Create ---");
        Console.WriteLine(" ");

        while (true)
        {
            Console.Write("Enter your Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter your Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter your Sales: ");
            double sales = Convert.ToDouble(Console.ReadLine());

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Console.WriteLine("Please Enter Valid Email");
                continue;
            }
            else
            {
                var rec = new AddRecord(name, email, sales);
                AddRecord.insertRecord(rec, filePath);
                Console.WriteLine("Record Added");
                break;
            }
        }
    }

    record AddRecord(String name, String email, double sales)
    {
        public static void insertRecord(AddRecord r, string filePath)
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

    static void Read()
    {
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                String line;
                int counter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line + " " + "Word: " + line.Count());
                    counter++;
                }
                Console.WriteLine("Total lines: " + counter);
            }
        }
        catch (Exception ex)
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
        Console.ReadKey();
    }

    static void Uselectedrow()
    {
        try
        {
            var lines = File.ReadAllLines(filePath).ToList();

            for (int id = 0; id < lines.Count; id++)
            {
                Console.WriteLine($"{id}: {lines[id]}");
            }

            Console.Write("Select a ID to modify a Record: ");
            int selectedID = Convert.ToInt32(Console.ReadLine());

            Update(selectedID);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void Update(int rowID)
    {
        try
        {
            var lines = File.ReadAllLines(filePath).ToList();

            Console.WriteLine($"You Select: {rowID} ");

            string[] column = { "Email", "Name", "Sales"};
            
            Console.Write("Select a column: ");
            string selectedColumn = Console.ReadLine();

            int colIndex = Array.FindIndex(column, c => c.Equals(selectedColumn, StringComparison.OrdinalIgnoreCase));

            if (colIndex >= 0)
            {
                var row = lines[rowID].Split(',');
                Console.WriteLine($"Current value: {row[colIndex]}");
                Console.WriteLine(" ");

                Console.Write("Enter your new value: ");
                string newValue = Console.ReadLine();

                row[colIndex] = newValue;
                lines[rowID] = string.Join(",", row);

                File.WriteAllLines(filePath, lines); 

                Console.WriteLine("Record updated successfully!");
            }
            else
            {
                Console.WriteLine("Invalid column selected.");
            }

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void Dselectedrow()
    {
        try
        {
            var lines = File.ReadAllLines(filePath).ToList();

            for (int id = 0; id < lines.Count; id++)
            {
                Console.WriteLine($"{id}: {lines[id]}");
            }

            Console.Write("Select a ID to remove a Record: ");
            int selectedID = Convert.ToInt32(Console.ReadLine());
            Delete(selectedID);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    static void Delete(int rowID)
    {
        
        try
        {
            var lines = File.ReadAllLines(filePath).ToList();

            if (rowID <= 0 || rowID > lines.Count)
            {
                Console.WriteLine("Invalid ID");
                return;
            }

            var deleteRecord = lines.ElementAtOrDefault(rowID);

            lines.Remove(deleteRecord);
            
            File.WriteAllLines(filePath, lines);

            Console.WriteLine($"Row {rowID} deleted successfully!");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadKey();
    }
}
