using System;

public class WithdrawTransaction
{
    private Account _account;
    private decimal _amount;
    private bool _executed = false;
    private bool _success = false;
    private bool _reversed = false;


    public bool Success
    {
        get
        {
            return this._success;
        }
    }

    public bool Executed
    {
        get
        {
            return this._executed;
        }
    }

    public bool Reversed
    {
        get
        {
            return this._reversed;
        }
    }

    public WithdrawTransaction(Account account, decimal amount)
    {
        this._account = account;
        this._amount = amount;
    }

    public void Execute()
    {
        if (this._executed)
        {
            throw new Exception("Already executed.. Cannot execute this transaction!");
        }
        this._executed = true;
        this._account.Withdraw(_amount);
        this._success = true;
    }

    public void Rollback()
    {
        if (this._executed != true)
        {
            throw new Exception("Transaction has not been executed!");
        }
        else if (_reversed == true)
        {
            throw new Exception("Transaction has been reversed!");
        }
        else if (this._executed)
        {
            this._account.Deposit(_amount);
            this._reversed = true;
        }

    }

    public void Print()
    {
        if (this._success)
        {
            Console.WriteLine("******************************\n");
            Console.WriteLine("Withdrawal was successful!");
            Console.WriteLine($"Amount withdrawn: {this._amount}");
            Console.WriteLine("\n******************************");

        }
        if (this._reversed)
        {
            Console.WriteLine("Transaction was reversed!");
        }
    }
}