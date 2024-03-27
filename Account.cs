using System;
using System.Reflection.Emit;

public class Account
{
    private decimal _balance;
    private string _name;

    // The underscore before a variable name _val is nothing more than a convention.
    //  In C#, it is used when defining the private member variable for a public property. 
    // The "this." keyword refers to the current instance of the class and 
    // is also used as a modifier of the first parameter of an extension method.

    // Constructor
    public Account(string name, decimal startingBalance)
    {
        this._name = name;
        this._balance = startingBalance;
    }


    public bool Deposit(decimal amountToDeposit)
    {
        if (amountToDeposit > 0)
        {
            this._balance += amountToDeposit;
            return true;
        }
        return false;
    }

    public bool Withdraw(decimal amountToDeduct)
    {
        if (amountToDeduct < this._balance && amountToDeduct > 0)
        {
            this._balance -= amountToDeduct;
            return true;
        }
        return false;
    }

    // a getter to access the name 
    public string Name
    {
        get
        {
            return this._name;
        }
    }

    public void Print()
    {
        Console.WriteLine($"Hello Name: {this._name}!");
        Console.WriteLine($"Account Balance: {this._balance}");
    }
}
