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
        _account = account;
        _amount = amount;
    }

    public void Execute()
    {
        if (_executed)
        {
            throw new Exception("Cannot execute this transaction");
        }
        _executed = true;
        _account.Withdraw(_amount);
        _success = true;
    }

    public void Rollback()
    {
        if (_executed != true)
        {
            throw new Exception("Transaction has not been executed");
        }
        else if (!_reversed)
        {
            throw new Exception("Transaction has been reversed");
        }
        else if (_executed)
        {
            _account.Deposit(_amount);
            _reversed = true;
        }

    }

    public void Print()
    {
        if(_success) {
            Console.WriteLine("******************************");
            Console.WriteLine("Withdrawal was successful!");
            Console.WriteLine($"Amount withdrawn: {_amount}");
            Console.WriteLine("******************************");

        }
    }
}