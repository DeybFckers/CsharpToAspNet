using System;

class ValueReferenceTypes
{
    static void Main(string[] args)
    {
        CreateObjects();

        #region --CHALLENGE--
        Console.WriteLine(" ");
        Console.WriteLine("CHALLENGE");
        int a = 10;
        var b = a;
        b = 99;
        Console.WriteLine(a);
        Console.WriteLine(b);
        Console.WriteLine(" ");

        List<String> names = new() { "Ana", "Ben" };
        var others = names;

        others.Add("Chris");

        Console.WriteLine(string.Join(", ", names));
        Console.WriteLine(string.Join(", ", others));
        #endregion
    }
    #region -- PRACTICE --
    static void CreateObjects()
    {
        //stack
        Point p1 = new Point { x = 4, y = 5 };
        Point p2 = new Point { x = 4, y = 5 };

        ProcessPoint(p1); //result 5,5 because you pass the value and it becomes a copy
                          //so it wont affect the original value if any changes
        //this is original value
        Console.WriteLine(p1);

        /*if the declare a value in constructor it will be put on heap */
        Person fred = new Person("Fred");
        Console.WriteLine(fred.Name);

        //you just pass the object variable
        ProcessPerson(fred);

        /*the reason it change because theres an object of Person and 
         * thats what happens if you move the reference type */
        Console.WriteLine(fred.Name);

        /* so the value type is the one who accept a value and have a copy
         * while the reference type it will override the original value */
    }
    static void ProcessPerson(Person p)
    {
        p.Name = "Barney";
    }
    
    static void ProcessPoint(Point p)
    {
        p.x++;

        Console.WriteLine(p);
    }
    #endregion
}
#region -- PRACTICE --
//struct is an Value type
public struct Point
{
    public int x { get; set; } 
    public int y { get; set; }

    public override string ToString() => $"({x}, {y})";
}

//class is an reference type
public class Person
{
    public string Name { get; set; }

    public Person()
    {

    }
    
    public Person(string name)
    {
        Name = name;
    }

}

#endregion