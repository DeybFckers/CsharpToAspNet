using System;

class GarbageCollection
{
    class Person
    {
        public string Name { get; set; }
        public Person ChildOne { get; set; }
        public Person ChildTwo { get; set; }

        //destructor
        ~Person()
        {
            Console.WriteLine($"   Collection {Name}.");
        }
    }
    static void ShortLives(Person parent)//The parameters means wilma
    {
        Person fred = new Person // new parent because we create a new object
        {
            Name = "Fred",
            ChildOne = new Person { Name = "Bamm-Bamm" }
        };
        // parent = wilma    //new parent = fred
        parent.ChildTwo = fred.ChildOne;
        /* wilma.childtwo = fred.childone it means
         * wilma.childtwo has a value now which is Bamm-Bamm
         * then fred.childone has a value also which is Bamm-Bamm
         * but childtwo of fred has no value
         */
    }
    static void Run()
    {
        Person wilma = new Person
        {
            Name = "Wilma",
            ChildOne = new Person { Name = "Pebbles" }
        };
        // childone = pebbles, childtwo = none
        ShortLives(wilma); //then we pass the wilma to parameters of shortlives which is object

        Console.WriteLine("Leaving 'ShortLives'...");
        //since Fred no references then we can collect
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

    static void Main()
    {
        Run();

        Console.WriteLine("\nLeaving 'Run'...");

        //since in Run function we collect the fred then the wilma no more reference also
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

}
