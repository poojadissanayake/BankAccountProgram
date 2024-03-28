public class TransferTransaction
{
    private Account _toAccount;
    private Account _fromAccount;
    private decimal _amount;

    private DepositTransaction _deposit;
    private WithdrawTransaction _withdraw;

    private bool _executed;
    private bool _success;
    private bool _reversed;

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

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
    {
        this._fromAccount = fromAccount;
        this._toAccount = toAccount;
        this._amount = amount;
        this._deposit = new DepositTransaction(toAccount, amount);
        this._withdraw = new WithdrawTransaction(fromAccount, amount);
    }
}