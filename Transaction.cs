public abstract class Transaction
{

    // protected : this class member is available within this class and any child classes, but not other classes.
    protected decimal _amount;
    private bool _executed;
    private bool _reversed;
    private DateTime _dateStamp;

    // abstract read-only property - only a get accessor is needed
    public abstract bool Success { get; }
    public bool Executed { get { return _executed; } }
    public bool Reversed { get { return _reversed;}}
    public DateTime DateStamp { get { return _dateStamp;}}

    public Transaction(decimal amount){
        _amount = amount;
    }
    //abstract methods - can't have a body {}
    public abstract void Print();
    public virtual void Execute(){
        if(_executed){
            throw new Exception("Transaction already executed!");
        }
        _executed = true;
        _dateStamp = DateTime.Now;
    }
    public virtual void Rollback(){
        if(_reversed){
            throw new Exception("Transaction has been reversed!");
        }
        _reversed = true;
        _dateStamp = DateTime.Now;
    }
}
