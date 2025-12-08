using System;
using System.Reflection.Metadata;

#region -- PRACTICE --
//Same purpose but different approach
record Account(String AccountNumber, string AccountName); 
/*record is still a class but it make simple like you dont have to hardcode and
 * theres a builtin getter/setter, deconstruct, constructor, immutable/iequatable implement, etc*/
class CAccount(String AccountNumber, string AccountName):IEquatable<CAccount>
{                                                       //in order to allowed iequatable in class
    // unlike this you need to hardcode the deconstruct, getter/setter, constructor
    public string AccountNumber { get; init; } = AccountNumber;
    public string AccountName { get; init; } = AccountName;
    //deconstrcut method or function
    public void Deconstruct(out String AccountNumber, out String AccountName)
    {
        AccountNumber = this.AccountNumber;
        AccountName = this.AccountName;
    }

    //do this for iequatable in class
    public bool Equals(CAccount? other)
    {
        return other is not null 
            && other.AccountName == this.AccountName 
            && other.AccountNumber == this.AccountNumber;
    }
    public override bool Equals(object? obj)
    {
        return this.Equals(obj as CAccount); 
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(this.AccountNumber, this.AccountName);
    }
    //bool operator
    public static bool operator ==(CAccount first, CAccount second)
    {
        return first.Equals(second);
    }
    public static bool operator !=(CAccount first, CAccount second)
    {
        return !first.Equals(second);
    }


}
#endregion

record BankRecord(String AccountNumber, String AccountHolder, double Balance);

class CBankRecord(String AccountNumber, String AccountHolder, double Balance) : IEquatable<CBankRecord>
{
    public String AccountNumber { get; init; } = AccountNumber;
    public String AccountHolder { get; init; } = AccountHolder;
    public double Balance { get; init; } = Balance;
    public void Deconstruct(out String AccountNumber, out String AccountHolder, out double Balance)
    {
        AccountNumber = this.AccountNumber;
        AccountHolder = this.AccountHolder;
        Balance = this.Balance;
    }
    public bool Equals(CBankRecord? other)
    {
        return other is not null
            && other.AccountNumber == this.AccountNumber
            && other.AccountHolder == this.AccountHolder
            && other.Balance == this.Balance;
    }
    public override bool Equals(object? obj)
    {
        return this.Equals(obj as CBankRecord);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(this.AccountNumber, this.AccountHolder, this.Balance);
    }
    public static bool operator ==(CBankRecord first, CBankRecord second)
    {
        return first.Equals(second);
    }
    public static bool operator !=(CBankRecord first, CBankRecord second)
    {
        return !first.Equals(second);
    }
}

class Record
{
    static void Main(string[] args)
    {
        #region -- PRACTICE --
        //records
        Account account1 = new("123456", "Dave");
        Account account = new Account("123456", "Dave");
        var(accountNumber,_) = account; //didn't error because record has builtin deconstruct
        //var _accountNumber = account.AccountNumber; you can use this
        //you can also change the value of specific variable like this
        Account account2 = account with { AccountNumber = "11111111" }; /*it change the value of AccountNumber
        Console.WriteLine(account2.AccountName); but it didn't change the name */
        Console.WriteLine(account2.AccountNumber);
        /*Console.WriteLine(account == account1);
        Console.WriteLine(account.Equals(account1));
        this is an iequatable method of records and both are true */

        //class
        CAccount cAccount = new CAccount("123456", "Dave");
        //CAccount cAccount1 = cAccount with { AccountNumber = "11111111" }; it cause an error because it is not a record type
        var (accountNumber2, _) = cAccount; // if you wont declare an deconstruct it will error
        Console.WriteLine(accountNumber2);
        //iequatable checker of class
        CAccount caccount1 = new("123456", "Dave");
        CAccount caccount = new CAccount("123456", "Dave");
        Console.WriteLine(caccount == caccount1);//false //in order to make it true you have to do the bool operator
        Console.WriteLine(caccount.Equals(caccount1));//true
        #endregion

        #region -- CHALLENGE --
        Console.WriteLine(" ");
        Console.WriteLine("Challenge");
        Console.WriteLine(" ");
        Console.WriteLine("Records");
        BankRecord br = new("12345", "Alice", 1000);
        BankRecord br1 = br with { Balance = 1500 };
        Console.WriteLine(br);
        Console.WriteLine(br1);
        Console.WriteLine(br != br1);
        Console.WriteLine(" ");
        Console.WriteLine("Class");
        CBankRecord cbr = new("12345", "Bob", 2000);
        CBankRecord cbr1 = new("12345", "Bob", 2500);
        Console.WriteLine(cbr != cbr1);
        #endregion
    }

}