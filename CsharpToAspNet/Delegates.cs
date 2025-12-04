using System;
using System.Numerics;

delegate int Calculator(int x);
public delegate void delegate1(int a, int b);
delegate void UpdatedGrade(Student s, double bonus, double cap);
public class Delegates
{

    static int value = 10;

    static void Main(string[] args)
    {
        #region -- Delegates ---
        /* A delegate defines a method signature (parameters + return type).
           Any method that matches that signature can be stored inside the delegate.
           or a delegates is an function that can accept a functions inside the paramters
           but if the delegate paramters ask an 2 or more variable then the other method
           or functions should require also a parameters 2 or more variable
         */
        //instantiate the delegate
        Calculator calc1 = new Calculator(addition);
        Calculator calc2 = new Calculator(subtraction);
        Calculator calc3 = new Calculator(multiplication);
        Calculator calc4 = new Calculator(division);
        calc1(20);// calling methods using delegate
        /* in actual 10(global variable) + 20(declared value) = 30 
         * because the delegates parameter is an integer then the 
         * functions of addition is an integer but inside of addition 
         * has already a method, all you need to do is instantiate then
         * declared a value */
        Console.WriteLine("After calc1 delegate, value is: " + getValue());
        calc2(3);
        Console.WriteLine("After calc2 delegate, value is: " + getValue());
        calc3(5);
        Console.WriteLine("After calc3 delegate, value is: " + getValue());
        calc4(5);
        Console.WriteLine("After calc4 delegate, value is: " + getValue());

        Console.WriteLine("====== SINGLE DELEGATE ======");
        singledelegate obj = new singledelegate();
        delegate1 d1 = new delegate1(obj.addition);
        d1(100, 200);
        delegate1 d2 = new delegate1(obj.subtraction);
        d2(100, 200);

        Console.WriteLine("====== MUlTI DELEGATE ======");
        multidelegate mdobj = new multidelegate();
        delegate1 d3 = new delegate1(mdobj.add);
        delegate1 d4 = new delegate1(mdobj.sub);
        d3 = d3 + d4; //combine methods
        /*since i combine the methods then the d3 will have now a 2 functions
        which is (add, sub) now, when you are doing a multi delegates the 
        + sign are not add arithmatic it means combining a method
         */
        d3(100, 200);

        Console.WriteLine("====== GENERIC DELEGATE ======");
        
        Func<int, float, double, double> object1 = new Func<int, float, double, double>(genericdelegate.SumNumber1);
        double output = object1.Invoke(10, 12.15f, 45.78);
        Console.WriteLine(output);

        Action<int, float, double> object2 = new Action<int, float, double>(genericdelegate.SumNumber2);
        object2.Invoke(20, 12.15f, 45.78);

        Predicate<string> object3 = new Predicate<string>(genericdelegate.ChecktheLength);
        bool b = object3.Invoke("Hello");
        Console.WriteLine(b);

        #endregion

        #region -- Challenge --

        Console.WriteLine("====== Challenge ======");
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Grade: ");
        double grade = Convert.ToSingle(Console.ReadLine());

        Console.Write("bonus: ");
        double bonus = Convert.ToSingle(Console.ReadLine());

        Student s = new Student(name, grade);
        UpdatedGrade ug = Student.AddCurve;
        ug += Student.GradeCap;
        ug.Invoke(s, bonus, 100);

        Console.WriteLine("====== Func ======");
        Func<Student, double, double> s1 = new Func<Student, double, double>(Student.finalGrade);
        double finalGrade = s1.Invoke(s, bonus);
        Console.WriteLine(s.Name + " final Grade: " + finalGrade);

        Console.WriteLine("====== Action ======");
        Action<Student, double> s2 = new Action<Student, double>(Student.studentDetails);
        double FinalGrade = grade + bonus;
        s2.Invoke(s, FinalGrade);

        Console.WriteLine("====== Predicate ======");
        Predicate<double> s3 = new Predicate<double>(Student.studentStatus);
        bool status = s3.Invoke(FinalGrade);
        Console.WriteLine(s.Name + ": " + status);

        #endregion
    }


    #region--example method --
    public static int addition(int x)
    {
        value = value + x;
        return value;
    }
    public static int subtraction(int x)
    {
        value = value - x;
        return value;
    }
    public static int multiplication(int x)
    {
        value = value * x;
        return value;
    }
    public static int division(int x)
    {
        value = value / x;
        return value;
    }
    public static int getValue()
    {
        return value;
    }

    #endregion
}
#region -- Challenge Class --
public class Student
{
    public string Name { get; set; }
    public double Grade { get; set; }

    public Student(string name, double grade)
    {
        Name = name;
        Grade = grade;
    }

    public static void AddCurve(Student s, double bonus, double cap)
    {
        double total = s.Grade + bonus;
        Console.WriteLine("Curve Bonus: " + bonus);
        Console.WriteLine("Final Grade: " + total);
    }
    public static void GradeCap(Student s, double bonus, double cap)
    {
        double gradecap = 100;
        double totalGrade = s.Grade + bonus;
        if(totalGrade > gradecap)
        {
            Console.WriteLine("With cap: " + gradecap);
        }
    }
    public static double finalGrade(Student s, double bonus)
    {
        return s.Grade + bonus;
    }
    public static void studentDetails(Student s, double finalGrade)
    {
        Console.WriteLine("Student Name: " + s.Name + ", Grade: " + finalGrade);
    }
    public static bool studentStatus(double finalGrade)
    {
        if(finalGrade >= 75)
        {
            return true;
        }
        return false;
    }

}
#endregion
//single delegate
#region -- Single Delegate --
class singledelegate
{
    
    public void addition(int a, int b)
    {
        Console.WriteLine("The Sum is " + (a + b));
    }

    public void subtraction(int a, int b)
    {
        Console.WriteLine("The Difference is " + (a - b));
    }
}
#endregion
//multi delegate
#region -- Multi Delegate --
class multidelegate
{
    public void add(int a, int b)
    {
        Console.WriteLine("The Sum is " + (a + b));
    }

    public void sub(int a, int b)
    {
        Console.WriteLine("The Difference is " + (a - b));
    }
}
#endregion
//generic delegate
#region -- Generic Delegate --
class genericdelegate
{
    public static double SumNumber1(int number1, float number2, double number3)
    {
        return number1 + number2 + number3;
    }
    public static void SumNumber2(int number1, float number2, double number3)
    {
        Console.WriteLine(number1 + number2 + number3);
    }

    public static bool ChecktheLength(string name1)
    {
        if(name1.Length < 10)
        {
            return true;
        }
        return false;
    }
}
#endregion