public class DepositTransaction : Transaction
{
    private Account _account;
    private bool _success = false;

    public override bool Success
    {
        get
        {
            return this._success;
        }
    }

    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        this._account = account;
        this._amount = amount;
    }

    public override void Execute()
    {
        if (Executed)
        {
            throw new Exception("Cannot proceed, Already executed!");
        }
        base.Execute();
        _account.Deposit(_amount);
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
            Console.WriteLine("Deposit was successful!");
            Console.WriteLine($"Amount deposited: {this._amount} at {DateStamp}\n");
            Console.WriteLine("******************************");

        }
        if (Reversed)
        {
            Console.WriteLine("Transaction was reversed!");
        }
    }
}