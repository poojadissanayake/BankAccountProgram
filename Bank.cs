using System.Collections.Generic;
#nullable disable

public class Bank
{

    private static List<Account> _accounts = new List<Account>();
    private List<Transaction> _transactions = new List<Transaction>();

    public static void AddAccount(Account account)
    {
        _accounts.Add(account);
    }
    public Account GetAccount(string name)
    {
        foreach (Account account in _accounts)
        {
            if (account.Name == name)
            {
                return account;
            }
        }
        return null;
    }

    public void ExecuteTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
        transaction.Execute();

    }

    public void PrintTranscationHistory()
    {
        foreach (Transaction transcation in _transactions)
        {
            transcation.Print();
        }
    }
}