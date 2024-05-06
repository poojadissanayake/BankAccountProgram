using System;

public class WithdrawTransaction : Transaction
{
    private Account _account;
    private bool _success = false;


    public override bool Success { get { return _success; } }


    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }

    public override void Execute()
    {
        if (Executed)
        {
            throw new Exception("Already executed.. Cannot execute this transaction!");
        }

        base.Execute();
        _account.Withdraw(_amount);
        _success = true;
    }

    public override void Rollback()
    {
        if (!Executed)
        {
            throw new Exception("Transaction has not been executed!");
        }
        base.Rollback();
        _account.Deposit(_amount);

    }

    public override void Print()
    {
        if (Success)
        {
            Console.WriteLine("******************************\n");
            Console.WriteLine("Withdrawal was successful!");
            Console.WriteLine($"Amount withdrawn: {this._amount} at {DateStamp}");
            Console.WriteLine("\n******************************");

        }
        if (Reversed)
        {
            Console.WriteLine("Transaction was reversed!");
        }
    }
}