using System;

class ErrorHandling
{
    static void Main(String[] args)
    {
        var number1 = 100;
        var number2 = 50;
        var number3 = 0;
        try
        {
            //it means it will try to execute
            Console.WriteLine($"Addition result: {number1 + number2}");
            Console.WriteLine($"Addition result: {number1 / number2}");
        }
        catch ( Exception ex )
        {
            //if during execution theres an error
            //it will print this
            Console.WriteLine(ex.Message);
        }
    }
}