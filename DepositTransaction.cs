public class DepositTransaction
{
    private Account _account;
    private decimal _amount;
    private bool _executed = false;
    private bool _success = false;
    private bool _reversed = false;

    public bool Executed
    {
        get
        {
            return this._executed;
        }
    }

    public bool Succeeded
    {
        get
        {
            return this._success;
        }
    }

    public bool Reversed
    {
        get
        {
            return this._reversed;
        }
    }

    public DepositTransaction(Account account, decimal amount)
    {
        this._account = account;
        this._amount = amount;
    }

    public void Execute()
    {
        if (this._executed)
        {
            throw new Exception("Cannot proceed, Already executed!");
        }
        this._executed = true;
        this._account.Deposit(this._amount);
        this._success = true;
    }

    public void Rollback()
    {
        if (this._executed != true)
        {
            throw new Exception("Transaction has not been executed!");
        }
        else if (this._executed)
        {
            this._account.Deposit(_amount);
            this._reversed = true;
        }
        else if (!this._reversed)
        {
            throw new Exception("Transaction has been reversed!");
        }
    }

    public void Print()
    {
        if (this._success)
        {
            Console.WriteLine("******************************\n");
            Console.WriteLine("Deposit was successful!");
            Console.WriteLine($"Amount deposited: {this._amount}\n");
            Console.WriteLine("******************************");

        }
        if (this._reversed)
        {
            Console.WriteLine("Transaction was reversed!");
        }
    }
}