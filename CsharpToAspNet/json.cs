using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


class json
{
    static void Main(string[] args)
    {
        /* serialization is TO JSON 
           deserialization is FROM JSON
        */
        Purchase purchase = new Purchase(
            "Orange Juice",
            DateTime.UtcNow,
            2.49
        );

        var options = new JsonSerializerOptions();

        options.WriteIndented = true;

        string jsonString = JsonSerializer.Serialize(purchase, options);

        
    }

    record Purchase (String ProductName, DateTime DateTime, double ProductPrice);
    
}
