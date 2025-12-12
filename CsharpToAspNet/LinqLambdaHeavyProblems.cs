using System;
using System.Linq;


class LinqLambdaHeavyProblems
{
    static void Main(string[] args)
    {
        List<Customer> customers = new List<Customer>
        {
            new Customer{ Id = 1, Name="Alice", City="NY", Age=30},
            new Customer{ Id = 2, Name="Bob", City="LA", Age=25},
            new Customer{ Id = 3, Name="Charlie", City="NY", Age=35},
            new Customer{ Id = 4, Name="David", City="SF", Age=28},
            new Customer{ Id = 5, Name="Eve", City="LA", Age=40}
        };

        List<Booking> bookings = new List<Booking>
        {
            new Booking{ Id=1, CustomerId=1, Service="Haircut", Price=20, Date=DateTime.Parse("2025-12-01")},
            new Booking{ Id=2, CustomerId=2, Service="Shave", Price=15, Date=DateTime.Parse("2025-12-02")},
            new Booking{ Id=3, CustomerId=1, Service="Massage", Price=50, Date=DateTime.Parse("2025-12-03")},
            new Booking{ Id=4, CustomerId=3, Service="Haircut", Price=20, Date=DateTime.Parse("2025-12-03")},
            new Booking{ Id=5, CustomerId=5, Service="Haircut", Price=25, Date=DateTime.Parse("2025-12-04")},
            new Booking{ Id=6, CustomerId=4, Service="Massage", Price=50, Date=DateTime.Parse("2025-12-05")}
        };

        Console.WriteLine("CHALLENGE");
        Console.WriteLine(" ");

        #region --FILTERING--
        Console.WriteLine("Filtering");
        Console.WriteLine(" ");
        Console.WriteLine("LINQ");
        var linq1 = from customer in customers
                    join booking in bookings on customer.Id equals booking.CustomerId
                    where customer.Age >= 30 && booking.Price >= 20
                    orderby customer.Age, booking.Price
                    select new
                    {
                        customer.Name,
                        Price = booking.Price,
                    };
                  
        foreach(var linq in linq1)
        {
            Console.WriteLine($"Customer: {linq.Name}, Price: {linq.Price}");
        }
        Console.WriteLine(" ");
        Console.WriteLine("LAMBDA");

        var lambda1 = customers
            .Where(customer => customer.Age >= 30)
            .Join(
                bookings.Where(booking => booking.Price >= 20),
                customer => customer.Id,
                booking => booking.CustomerId,
                (customer, booking) => new
                {
                    customer.Name,
                    Price = booking.Price,
                }
            ).OrderBy(customer => customer.Name).ThenBy(booking => booking.Price);
        foreach (var lambda in lambda1)
        {
            Console.WriteLine($"Customer: {lambda.Name}, Price: {lambda.Price}");
        }
        Console.WriteLine(" ");
        #endregion

        #region --PROJECTION--
        Console.WriteLine("Projection");
        Console.WriteLine(" ");
        Console.WriteLine("LINQ");

        var linq2 = from customer in customers
                    join booking in bookings on customer.Id equals booking.CustomerId
                    select new
                    {
                        customer.Id,
                        customer.Name,
                        Service = booking.Service
                    };
        foreach(var linq in linq2)
        {
            Console.WriteLine($"ID: {linq.Id}, Name: {linq.Name}, Serivce: {linq.Service}");
        }

        Console.WriteLine(" ");
        Console.WriteLine("LAMBDA");

        var lambda2 = customers
            .Join(
                bookings,
                customer => customer.Id,
                booking => booking.CustomerId,
                (customer, booking) => new
                {
                    customer.Id,
                    customer.Name,
                    booking.Service
                }
            );
        foreach(var lambda in lambda2)
        {
            Console.WriteLine($"ID: {lambda.Id}, Name: {lambda.Name}, Serivce: {lambda.Service}");
        }
        Console.WriteLine(" ");
        #endregion

        #region --SORTING--
        Console.WriteLine("Sorting");
        Console.WriteLine(" ");
        Console.WriteLine("LINQ");

        var linq3 = from customer in customers
                    join booking in bookings on customer.Id equals booking.CustomerId
                    orderby customer.Age, booking.Price descending
                    select new
                    {
                        Name = customer.Name,
                        Age = customer.Age,
                        Price = booking.Price,
                    };
        foreach(var linq in linq3)
        {
            Console.WriteLine($"Name: {linq.Name}, Age: {linq.Age}, Price: {linq.Price}");
        }

        Console.WriteLine(" ");
        Console.WriteLine("LAMBDA");

        var lambda3 = customers
            .Join(
                bookings,
                customer => customer.Id,
                booking => booking.CustomerId,
                (customer, booking) => new
                {
                    Age = customer.Age, 
                    Price = booking.Price,
                }
            ).OrderBy(customer => customer.Age).ThenByDescending(booking => booking.Price);

        foreach (var lambda in lambda3)
        {
            Console.WriteLine($"Age: {lambda.Age}, Price: {lambda.Price}");
        }
        Console.WriteLine(" ");
        #endregion

        #region --GROUPING--

        Console.WriteLine("Grouping");
        Console.WriteLine(" ");
        Console.WriteLine("LINQ");

        var linq4 = from customer in customers
                    group customer by customer.City into cityGroup
                    select new City
                    {
                        name = cityGroup.Key, city = cityGroup.Count()
                    };
        foreach(var linq in linq4)
        {
            Console.WriteLine($"{linq.name}: {linq.city}");
        }

        var linq5 = from booking in bookings
                    group booking by booking.Service into serviceGroup
                    select new Service
                    {
                        name = serviceGroup.Key,
                        service = serviceGroup.Count()
                    };
        foreach (var linq in linq5)
        {
            Console.WriteLine($"{linq.name}: {linq.service}");
        }

        Console.WriteLine(" ");
        Console.WriteLine("LAMBDA");

        var lambda4 = customers.GroupBy(customer => customer.City)
            .Select(cityGroup => new City
                {
                    name = cityGroup.Key, city = cityGroup.Count()
                });
        foreach (var lambda in lambda4)
        {
            Console.WriteLine($"{lambda.name}: {lambda.city}");
        }

        var lambda5 = bookings.GroupBy(booking => booking.Service)
            .Select(serviceGroup => new Service
            {
                name = serviceGroup.Key, service = serviceGroup.Count()
            });
        foreach (var lambda in lambda5)
        {
            Console.WriteLine($"{lambda.name}: {lambda.service}");
        }
        Console.WriteLine(" ");
        #endregion

        #region --AGGREGATION--

        Console.WriteLine("Aggregation");
        Console.WriteLine(" ");
        Console.WriteLine("LINQ");

        var linqRevenue = (from booking in bookings select booking.Price).Sum();
        
        Console.WriteLine($"Total Revenue: {linqRevenue}");

        var linqAverage = (from booking in bookings select booking.Price).Average();

        Console.WriteLine($"Average: {linqAverage}");

        Console.WriteLine(" ");
        Console.WriteLine("LAMBDA");

        var lambdaRevenue = bookings.Sum(booking => booking.Price);
        Console.WriteLine($"Total Revenue: {lambdaRevenue}");

        var lambdaAverage = bookings.Average(booking => booking.Price);
        Console.WriteLine($"Average: {lambdaAverage}");
        Console.WriteLine(" ");

        #endregion

        #region -- JOINING --

        Console.WriteLine("JOINING");
        Console.WriteLine(" ");
        Console.WriteLine("LINQ");

        var linq6 = from customer in customers
                    join booking in bookings on customer.Id equals booking.CustomerId
                    select new
                    {
                        Name = customer.Name,
                        Service = booking.Service,
                    };
        foreach(var linq in linq6)
        {
            Console.WriteLine($"{linq.Name} - {linq.Service}");
        }
        Console.WriteLine("NEW YORK CITY");
        var linq7 = from customer in customers
                    join booking in bookings on customer.Id equals booking.CustomerId
                    where customer.City == "NY"
                    select new
                    {
                        Name = customer.Name,
                        Service = booking.Service
                    };
        foreach(var linq in linq7)
        {
            Console.WriteLine($"{linq.Name} - {linq.Service}");
        }
        Console.WriteLine(" ");
        Console.WriteLine("LAMBDA");

        var lambda6 = customers
            .Join(
                bookings,
                customer => customer.Id,
                booking => booking.CustomerId,
                (customer, booking) => new
                {
                    Name = customer.Name,
                    Service = booking.Service
                }
            );
        foreach(var lambda in lambda6)
        {
            Console.WriteLine($"{lambda.Name} - {lambda.Service}");
        }
        Console.WriteLine("NEW YORK CITY");

        var lambda7 = customers.Where(customer => customer.City == "NY")
            .Join(
                bookings,
                customer => customer.Id,
                booking => booking.CustomerId,
                (customer, booking) => new
                {
                    Name = customer.Name,
                    Service = booking.Service
                }
            );
        foreach (var lambda in lambda7)
        {
            Console.WriteLine($"{lambda.Name} - {lambda.Service}");
        }
        Console.WriteLine(" ");
        #endregion

        #region --COMPLEX LINQ--

        Console.WriteLine("COMPLEX");
        Console.WriteLine(" ");
        Console.WriteLine("LINQ");

        var linq8 = from customer in customers
                    join booking in bookings on customer.Id equals booking.CustomerId
                    group new { customer, booking } by customer.City into g
                    select new TotalRevenue
                    {
                        name = g.Key,
                        total = g.Sum(x => x.booking.Price)
                    };
        foreach(var linq in linq8)
        {
            Console.WriteLine($"City: {linq.name}, Total: {linq.total}");
        }

        var linq9 = from customer in customers
                    join booking in bookings on customer.Id equals booking.CustomerId
                    group new { customer, booking } by customer.Name into customerGroup
                    orderby customerGroup.Sum(x => x.booking.Price) descending
                    select new top2
                    {
                        name = customerGroup.Key,
                        total = customerGroup.Sum(x => x.booking.Price)
                    };
        var top2Customers = linq9.Take(2);

        foreach (var cust in top2Customers)
        {
            Console.WriteLine($"{cust.name}: {cust.total}");
        }

        Console.WriteLine(" ");
        Console.WriteLine("LAMBDA");

        var lambda8 = customers
            .Join(
                bookings,
                customer => customer.Id,
                booking => booking.CustomerId,
                (customer, booking) => new { customer.City, booking.Price }
            )
            .GroupBy(x => x.City)
            .Select(g => new TotalRevenue
            {
                name = g.Key,
                total = g.Sum(x => x.Price)
            });

        foreach (var city in lambda8)
        {
            Console.WriteLine($"City: {city.name}, Total: {city.total}");
        }

        var lambda9 = customers
            .Join(
                bookings,
                customer => customer.Id,
                booking => booking.CustomerId,
                (customer, booking) => new { customer.Name, booking.Price }
            )
            .GroupBy(x => x.Name)
            .Select(g => new top2
            {
                name = g.Key,
                total = g.Sum(x => x.Price)
            })
            .OrderByDescending(x => x.total)
            .Take(2);

        foreach (var cust in lambda9)
        {
            Console.WriteLine($"{cust.name}: {cust.total}");
        }


        #endregion
    }
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
    }

    public class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Service { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
    }

    public class City
    {
        public string name { get; set; }
        public int city { get; set; }
    }

    public class Service
    {
        public string name { get; set; }
        public int service { get; set; }
    }

    public class TotalRevenue
    {
        public string name { get; set; }
        public double total { get; set;  }
    }

    public class top2
    {
        public string name { get; set; }
        public double total { get; set; }
    }
}
