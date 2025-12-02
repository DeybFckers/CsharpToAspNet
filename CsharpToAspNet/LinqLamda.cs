using System;
using System.Collections.Generic;
using System.Linq;
using static LinqLamda;

class LinqLamda
{
    static void Main(string[] args)
    {
        #region ---1st sample---
        var numbers = new[] { 5, 6, 2, 9, 1 };
        //using loop and if-else
        var evenNumbers = new List<int>();
        foreach (var number in numbers)
        {
            if (number % 2 == 0)
            {
                evenNumbers.Add(number);
            }
        }
        Console.WriteLine("Foreach and If");
        foreach (var even in evenNumbers)
        {
            Console.WriteLine(even);
        }

        Console.WriteLine("------------------");
        Console.WriteLine("LINQ");

        //Using LINQ
        //linq is just like a foreach loop but in a modern way
        //from - foreach(var number in number)      select - (result)
        var linqEvenNumbers = from number in numbers where number % 2 == 0 select number;
        //where - (condition)
        foreach (var even in linqEvenNumbers)
        {
            Console.WriteLine(even);
        }
        Console.WriteLine("------------------");
        Console.WriteLine("LAMDA");

        //Using Lambda
        var lambdaEvenNumbers = numbers.Where(number => number % 2 == 0);
        foreach (var even in lambdaEvenNumbers)
        {
            Console.WriteLine(even);
        }
        #endregion

        #region --- With Order and Transformation of data ---

        //Linq
        var mixedNumbers = new[] { 3, 5, 6, 9 };
        var linqQuery = from number in mixedNumbers
                        where number < 9
                        orderby number descending
                        select $"numero: {number}";
        Console.WriteLine("------------------");
        Console.WriteLine("LINQ");
        foreach (var num in linqQuery)
        {
            Console.WriteLine(num);
        }

        Console.WriteLine("------------------");
        Console.WriteLine("LAMDA");
        var lambdaQuery = mixedNumbers
            .Where(number => number < 9)
            //use OrderBy() if you want ascending    
            .OrderByDescending(number => number)
            .Select(number => $"numero: {number}");
        foreach (var num in lambdaQuery)
        {
            Console.WriteLine(num);
        }

        #endregion

        #region -- JOINS --

        Console.WriteLine("------------------");
        Console.WriteLine("LINQ JOIN");
        var students = new List<Student>()
        {
            new Student() { Name = "frace", Age = 32, Grade = 1},
            new Student() { Name = "ace", Age = 25, Grade = 3},
            new Student() { Name = "trunks", Age = 30, Grade = 1}
        };
        var sections = new List<Section>()
        {
            new Section() {Grade = 1, SectionName = "Section Nemic"},
            new Section() {Grade = 1, SectionName = "Section land of Wano"},
        };

        //LINQ
        //foreach(var section in sections)
        var query = from section in sections
                        //its just a from but in join keyword  //it means on section grade(1) = student grade(1)
                    join student in students on section.Grade equals student.Grade
                    //condition             
                    where student.Age > 20
                    //ascending
                    orderby student.Age, section.Grade
                    select new
                    {
                        //used the variable that you declare on from which is (section)
                        //then the SectionName is from class section
                        section.SectionName,
                        //StudentName is an anonymous type
                        //then the variable of Name is from student class
                        StudentName = student.Name

                    };
        foreach (var sectionAndStudent in query)
        {
            Console.WriteLine($"Section: {sectionAndStudent.SectionName}, Student Name: {sectionAndStudent.StudentName}");
        }

        Console.WriteLine("------------------");
        Console.WriteLine("LAMDAJOIN");

        var lamdaQuery = sections
            .Join(
                students.Where(student => student.Age > 20),
                section => section.Grade,
                student => student.Grade,
                (section, student) => new
                {
                    section.SectionName,
                    StudentName = student.Name,
                    student.Age,
                    student.Grade
                }
            ).OrderBy(student => student.Age).ThenBy(student => student.Grade);
        foreach (var sectionAndStudent in lamdaQuery)
        {
            Console.WriteLine($"Section: {sectionAndStudent.SectionName}, Student Name: {sectionAndStudent.StudentName}");
        }


        #endregion

        #region --- GROUPING ---

        Console.WriteLine("------------------");
        Console.WriteLine("LINQ - GROUP");

        //LINQ
        //itirate again like foreach
        var LinqQuery = from student in students
                            //group keyword means we will group the student by student Grade
                        group student by student.Grade into gradeGroup
                        //into keywords is getting the variable name
                        //class name  //strongly type         //strongly type           
                        select new GradeCount { Grade = gradeGroup.Key, StudentCount = gradeGroup.Count() };
        //Key means the value of grade 
        foreach (var gradeCount in LinqQuery)
        {
            Console.WriteLine($"Grade: {gradeCount.Grade}, Count: {gradeCount.StudentCount}");
        }

        Console.WriteLine("------------------");
        Console.WriteLine("LAMDA - GROUP");

        var LamdaQuery = students.GroupBy(student => student.Grade, student => student)
            .Select(groupCount =>
                    new GradeCount
                    {
                        Grade = groupCount.Key,
                        StudentCount = groupCount.Count()
                    }

            );
        foreach (var gradeCount in LamdaQuery)
        {
            Console.WriteLine($"Grade: {gradeCount.Grade}, Count: {gradeCount.StudentCount}");
        }

        #endregion

        #region ---Lamda only---

        Console.WriteLine("------------------");
        Console.WriteLine("LAMDA ONLY");

        var Numbers = Enumerable.Range(1, 10); // equivalent to {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
        Console.WriteLine(string.Join(", ", Numbers));

        //Avearage
        var average = Numbers.Average();
        var num1To5Average = Numbers.Where(n => n <= 5).Average();
        Console.WriteLine($"Average: {average}, Average 1 to 5: {num1To5Average}");

        //Count or Long Count, use Long Count when expected count is more than int Capacity
        var count = Numbers.Count();

        //Max and Min
        var maxNumber = Numbers.Max();
        var minNumber = Numbers.Min();

        //Sum
        var summation = Numbers.Sum();
        Console.WriteLine($"Count: {count}, Max: {maxNumber}, Min: {minNumber}, Sum: {summation}");

        #endregion

        #region --- CONVERSION ---

        var list = new List<int>(Numbers);
        var enumValue = list.AsEnumerable(); //converted to enumerable
        var backToList = enumValue.ToList(); //converted back to list
        list = (from Number in Numbers where Number >= 5 select Number).ToList();

        #endregion

        #region -- Basic Element Access --

        var firstNumber = Numbers.FirstOrDefault(); // Output: 1
        var lastNumber = Numbers.LastOrDefault();// Output: 10

        var firstEvenNumber = Numbers.FirstOrDefault(n => n % 2 == 0);

        var firstWithoutDefault = Numbers.First();
        var lastWithoutDefault = Numbers.Last();

        #endregion

        #region -- Partioning --

        Console.WriteLine("------------------");
        Console.WriteLine("LAMDA PARTITIONING");

        var numberSkip5 = Numbers.Skip(5); //skip a 5 items
        var numberSkipWhile = Numbers.SkipWhile(n => n < 5); // skip the items that are less than or equal 5

        var numberTake5 = Numbers.Take(5);// it means ge the 5 item
        var numbertakeWhile = Numbers.TakeWhile(n => n < 5);//it means get the item below 5

        Console.WriteLine($"Skip5: {string.Join(",", numberSkip5)}");
        Console.WriteLine($"SkipWhile: {string.Join(",", numberSkipWhile)}");
        Console.WriteLine($"Take5: {string.Join(",", numberTake5)}");
        Console.WriteLine($"TakeWhile: {string.Join(",", numbertakeWhile)}");

        #endregion

        #region -- Set --

        Console.WriteLine("------------------");
        Console.WriteLine("LAMDA SET");

        var repeatedNumbers = new[] { 1, 2, 3, 1, 5, 1, 1, 2 };
        var distinctNumbers = repeatedNumbers.Distinct(); //it means unique output

        Console.WriteLine($"Distinct: {string.Join(",", distinctNumbers)}");

        var excludeNumbers = new[] { 5, 6, 7 };
        var newNumbers = Numbers.Except(excludeNumbers); //it means all will show up except (5,6,7)
        Console.WriteLine($"Exclude: {string.Join(",", newNumbers)}");

        var checkerNumbers = new int[] { 1, 9, 10, 11, 12 };
        var intersectedNumbers = numbers.Intersect(checkerNumbers);//it means it only print the same value
        Console.WriteLine($"Intersect: {string.Join(",", intersectedNumbers)}");

        var appendNumbers = new[] { 10, 11, 12, 13 };
        var unionNumbers = Numbers.Union(appendNumbers);// it means it will add the count but it starts on 11
        Console.WriteLine($"Union: {string.Join(",", unionNumbers)}");

        #endregion

        #region -- Challenge --
        //notes use where if you are filtering
        Console.WriteLine("------------------"); ;
        Console.WriteLine("Challenge");
        Console.WriteLine(" ");

        int[] nums = { 1, 3, 5, 6, 8, 10, 12 };

        Console.WriteLine("Task 1 - Filter");
        Console.WriteLine("LINQ");

        var task1 = from num in nums where num > 5 select num;

        foreach (var prod in task1)
        {
            Console.WriteLine(prod * 2);
        }
        Console.WriteLine(" ");
        Console.WriteLine("Task 2 - Sorting");
        Console.WriteLine("LAMDA");

        List<string> fruits = new List<string> { "Banana", "Apple", "Mango", "Cherry" };

        var sortedLambda = fruits
            .OrderByDescending(fruit => fruit)
            .ToList();
        foreach (var sort in sortedLambda)
            Console.WriteLine(sort);


        Console.WriteLine(" ");
        Console.WriteLine("Task 3 - Join");
        Console.WriteLine("LINQ");

        var employees = new List<Employee>
        {
            new Employee { Name = "Alice", DeptId = 1 },
            new Employee { Name = "Bob", DeptId = 2 },
            new Employee { Name = "Charlie", DeptId = 1 }
        };

        var departments = new List<Department>
        {
            new Department { Id = 1, DeptName = "HR" },
            new Department { Id = 2, DeptName = "IT" }
        };

        var task3 = from employee in employees
                    join department in departments on employee.DeptId equals department.Id
                    select new
                    {
                        employee.Name,
                        DepartmentName = department.DeptName
                    };
        foreach (var nameDepartment in task3)
            Console.WriteLine($"Employee Name: {nameDepartment.Name}, Employee Department: {nameDepartment.DepartmentName}");

        Console.WriteLine(" ");
        Console.WriteLine("Task 4 - Grouping");
        Console.WriteLine("LAMDA");

        var peoples = new List<(string Name, string City)>
        {
            ("Alice", "NY"),
            ("Bob", "LA"),
            ("Charlie", "NY"),
            ("David", "LA"),
            ("Eve", "SF")
        };

        var task4 = peoples.GroupBy(people => people.City, people => people)
            .Select(peopleCount => new World
            {
                City = peopleCount.Key,
                People = peopleCount.Count()
            }
            );
        foreach (var peopleCount in task4)
            Console.WriteLine($"City: {peopleCount.City}, People: {peopleCount.People}");

        Console.WriteLine(" ");
        Console.WriteLine("Task 5 - Aggregation");
        Console.WriteLine("LAMDA");

        int[] scores = { 80, 90, 100, 70, 60 };

        //Using LINQ/Lambda: Find the average, maximum, minimum, and sum of the scores.
        var averageScore = scores.Average(); // average
        Console.WriteLine($"Average: {averageScore}");
        var sumScore = scores.Sum();//sum
        Console.WriteLine($"Sum: {sumScore}");
        var maxScore = scores.Max();//max
        Console.WriteLine($"Max: {maxScore}");
        var minScore = scores.Min();//min
        Console.WriteLine($"Min: {minScore}");

        Console.WriteLine(" ");
        Console.WriteLine("Task 6 - Set Operations");
        Console.WriteLine("LAMDA");

        int[] arr1 = { 1, 2, 3, 4, 5 };
        int[] arr2 = { 4, 5, 6, 7 };

        //Numbers only in arr1 (Except) Numbers common in both(Intersect) Numbers in either(Union)
        var excludeArr1 = arr1.Except(arr2);
        Console.WriteLine($"Exclude: {string.Join(",", excludeArr1)}");
        var intersectedArray = arr2.Intersect(arr1);
        Console.WriteLine($"Intersect: {string.Join(",", intersectedArray)}");
        var unionArray = arr2.Union(arr1);
        Console.WriteLine($"Union: {string.Join(",", unionArray)}");

        #endregion
    }

    class Employee { public string Name; public int DeptId; }
    class Department { public int Id; public string DeptName; }

    class World
    {
        public string City { get; set; }
        public int People { get; set; } 
    }
    public class Student
    {
        public String Name { get; set; }
        public int Age { get; set;  }
        public int Grade { get; set; }
    }
    public class Section
    {
        public String SectionName { get; set; }

        public int Grade { get; set; }
    }
    
    
    public class GradeCount
    {
        public int Grade { get; set; }
        public int StudentCount { get; set;  }
    }
}