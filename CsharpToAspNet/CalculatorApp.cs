using System;
class CalculatorApp
{
    static void Main(string[] args)
    {
        int num = 0;

        while (num != 5)
        {
            Console.WriteLine("Welcome To Calculator App");

            Console.WriteLine("(1) Addition");
            Console.WriteLine("(2) Subtraction");
            Console.WriteLine("(3) Multiplication");
            Console.WriteLine("(4) Division");
            Console.WriteLine("(5) Exit");

            Console.Write("Select a number to Operate: ");
           num = Convert.ToInt32(Console.ReadLine());

            switch (num)
            {
                case 1:
                    addition();
                    break;
                case 2:
                    Subtraction();
                    break;
                case 3:
                    Multiplication();
                    break;
                case 4:
                    Division();
                    break;
                case 5:
                    return;
                default:
                Console.WriteLine("Wrong Number!");
                break;

            } 
        }
    }

    static double addition()
    {
        Console.Write("Enter First Number: ");
        double num1 = Convert.ToSingle(Console.ReadLine());

        Console.Write("Enter Second Number: ");
        double num2 = Convert.ToSingle(Console.ReadLine());

        double sum = num1 + num2;
        Console.WriteLine("Sum: " + sum);

        return sum;
    }

    static double Subtraction()
    {
        Console.Write("Enter First Number: ");
        double num1 = Convert.ToSingle(Console.ReadLine());

        Console.Write("Enter Second Number: ");
        double num2 = Convert.ToSingle(Console.ReadLine());

        double diff = num1 - num2;
        Console.WriteLine("Difference: " + diff);

        return diff;
    }

    static double Multiplication()
    {
        Console.Write("Enter First Number: ");
        double num1 = Convert.ToSingle(Console.ReadLine());

        Console.Write("Enter Second Number: ");
        double num2 = Convert.ToSingle(Console.ReadLine());

        double prod = num1 * num2;
        
        Console.WriteLine("Product: " + prod);

        return prod;
    }

    static double Division()
    {
        Console.Write("Enter First Number: ");
        double num1 = Convert.ToSingle(Console.ReadLine());

        Console.Write("Enter Second Number: ");
        double num2 = Convert.ToSingle(Console.ReadLine());

        double quot = num1 / num2;
        
        Console.WriteLine("Quotient: " + quot);

        return quot;
    }
}