public class TransferTransaction : Transaction
{
    private Account _toAccount;
    private Account _fromAccount;

    private DepositTransaction _deposit;
    private WithdrawTransaction _withdraw;

    // this. qualifier should be used if the variable names are the same to show the current instance of the class,
    //  when using _var notation no need to identify the scope as it's already clear


    // success is only successful when both withdraw and deposit are successful
    public override bool Success { get { return _withdraw.Success && _deposit.Success; } }

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _deposit = new DepositTransaction(toAccount, amount);
        _withdraw = new WithdrawTransaction(fromAccount, amount);
    }

    public override void Execute()
    {
        if (Executed)
        {
            throw new Exception("Already executed.. Cannot execute this transaction!");
        }
        base.Execute();
        _withdraw.Execute();

        if (_withdraw.Success)
        {
            _deposit.Execute();
            if (!_deposit.Success)
            {
                _withdraw.Rollback();
            }
        }
    }

    public override void Rollback()
    {
        if (!Executed)
        {
            throw new Exception("Transaction not executed! ");
        }
        if (Reversed)
        {
            throw new Exception("Transaction has been reversed!");
        }
        if (_withdraw.Success)
        {
            _withdraw.Rollback();
        }
        if (_deposit.Success)
        {
            _deposit.Rollback();
        }



    }

    public override void Print()
    {
        _deposit.Print();
        _withdraw.Print();

        Console.WriteLine($"\n\n Summary: Transferred {_amount}($) from {_fromAccount.Name} to {_toAccount.Name}");
    }
}