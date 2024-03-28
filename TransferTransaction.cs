public class TransferTransaction
{
    private Account _toAccount;
    private Account _fromAccount;
    private decimal _amount;

    private DepositTransaction _deposit;
    private WithdrawTransaction _withdraw;

    private bool _executed = false;
    private bool _reversed = false;

    // this. qualifier should be used if the variable names are the same to show the current instance of the class,
    //  when using _var notation no need to identify the scope as it's already clear
    public bool Executed
    {
        get
        {
            return _executed;
        }
    }

    public bool Reversed
    {
        get
        {
            return _reversed;
        }
    }

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _amount = amount;
        _deposit = new DepositTransaction(toAccount, amount);
        _withdraw = new WithdrawTransaction(fromAccount, amount);
    }

    public void Execute()
    {
        if (_executed)
        {
            throw new Exception("Already executed.. Cannot execute this transaction!");
        }
        _withdraw.Execute();

        if (_withdraw.Success == true)
        {
            _deposit.Execute();
            if (_deposit.Succeeded != true)
            {
                _withdraw.Rollback();
            }
        }
        _executed = true;
    }

    public void Rollback()
    {
        if (_executed != true)
        {
            throw new Exception("Transaction not executed! ");
        }
        if (_reversed)
        {
            throw new Exception("Transaction has been reversed!");
        }
        if (_withdraw.Success == true)
        {
            _withdraw.Rollback();
            _reversed = true;
        }
        if (_deposit.Succeeded == true)
        {
            _deposit.Rollback();
            _reversed = true;
        }



    }

    public void Print()
    {
        _deposit.Print();
        _withdraw.Print();

        Console.WriteLine($"\n\n Summary: Transferred {_amount}($) from {_fromAccount.Name} to {_toAccount.Name}");
    }
}