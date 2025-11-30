using System;

class crudApp
{
    //use list String if you input a lot of products
    public static List<String> productNames = new List<string>();
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to CRUD app");

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
                    Update();
                    break;
                case 4:
                    Delete();
                    break;
            }

        }
    }

    static void Create()
    {
        Console.WriteLine("---- Create ---");
        Console.WriteLine(" ");

        while (true)
        {
            Console.Write("Enter Product Name or exit: ");
            String input = Console.ReadLine();

            if(input == "exit")
            {
                break;
            }

            if (productNames.Contains(input))
            {
                Console.WriteLine("Product Already Exist");
                continue;

            }
            else
            {
                productNames.Add(input);
                Console.WriteLine("Product Added");
                Console.WriteLine(" ");
                break;
                
            }

        }
    }

    static void Read()
    {
        Console.WriteLine("---- Read ---");

        if (productNames.Count == 0)
        {
            Console.WriteLine("Empty Products");
        }
        else
        {
            Console.WriteLine("Product List");
            foreach(String products in productNames)
            {
                Console.WriteLine(products);
                Console.WriteLine(" ");
            }
        }

    }

    static void Update()
    {

        if(productNames.Count == 0)
        {
            Console.WriteLine("No Product - Create a product first");
            Create();
            for(int i = 0; i < productNames.Count; i++)
            {
                Console.WriteLine(i + " " +productNames[i]);
            }
            Console.WriteLine(" ");
        }
        else
        {
            Console.WriteLine("Select a Product to update");
            for (int i = 0; i < productNames.Count; i++)
            {
                Console.WriteLine(i + " " + productNames[i]);
            }
            Console.WriteLine(" ");

        }

        while (true)
        {
            int digit;

            Console.Write("Enter a product number or press Q to exit: ");
            string input = Console.ReadLine();

            if (input.ToUpper() == "Q")
            {
                break;
            }
            if(!int.TryParse(input, out digit))
            {
                Console.WriteLine("Please enter a valid number!");
                continue;
            }
            if (digit >= productNames.Count)
            {
                Console.WriteLine("Please input a correct number!");
                continue;
            }
            else
            {
                Console.WriteLine(digit + " " + productNames[digit]);

                Console.Write("Enter new name of Product: ");
                string newName = Console.ReadLine();

                if (productNames.Contains(newName))
                {
                    Console.WriteLine("Product Name Already Exist");
                    break;
                }

                productNames[digit] = newName;

                Console.WriteLine("Updated Product List");
                Console.WriteLine(" ");

                for (int i = 0; i < productNames.Count; i++)
                {
                    Console.WriteLine(i + " " + productNames[i]);
                }
            }
            
        }

    }

    static void Delete()
    {

        if (productNames.Count == 0)
        {
            Console.WriteLine("No Product - Create a product first");
            Console.WriteLine(" ");
            return;
        }
        else
        {
            Console.WriteLine("Select a Product to remove: ");
            Console.WriteLine(" ");

            for (int i = 0; i < productNames.Count; i++)
            {
                Console.WriteLine(i + " " + productNames[i]);
                
            }
            Console.WriteLine(" ");
        }

        while (true)
        {
            Console.Write("Enter a product number or press Q to exit: ");
            string input = Console.ReadLine();

            int digit;
            if (input.ToUpper() == "Q")
            {
                break;
            }
            if (!int.TryParse(input, out digit))
            {
                Console.WriteLine("Please enter a valid number!");
                continue;
            }
            if (digit >= productNames.Count)
            {
                Console.WriteLine("Please input a correct number!");
                continue;
            }
            
            productNames.RemoveAt(digit);

            Console.WriteLine("Updated Products");
            for (int i = 0; i < productNames.Count; i++)
            {
                Console.WriteLine(i + " " + productNames[i]);

            }
            Console.WriteLine(" ");
        }
    }
}