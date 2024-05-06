using System;
using System.Runtime.InteropServices;
using SplashKitSDK;
#nullable disable

namespace BankAccount
{
    public enum MenuOption
    {
        Withdraw,
        Deposit,
        Transaction,
        Print,
        NewAccount,
        PrintTranscationHistory,
        Quit

    }
    public class Program
    {
        public static void Main()
        {
            Bank bank = new Bank();

            MenuOption userSelection;

            do
            {
                userSelection = ReadUserOption();

                switch (userSelection)
                {
                    case MenuOption.Deposit:
                        DoDeposit(bank);
                        break;

                    case MenuOption.Withdraw:
                        DoWithdraw(bank);
                        break;

                    case MenuOption.Transaction:
                        DoTransfer(bank);
                        break;

                    case MenuOption.Print:
                        DoPrint(FindAccount(bank));
                        break;

                    case MenuOption.Quit:
                        Console.WriteLine("\nQuitting...");
                        break;

                    case MenuOption.NewAccount:
                        NewAccount();
                        break;

                    case MenuOption.PrintTranscationHistory:
                        PrintTransactions(bank);
                        break;
                }
            }
            while (userSelection != MenuOption.Quit);
        }

        private static MenuOption ReadUserOption()
        {
            int option;
            do
            {
                Console.WriteLine("\nChoose an option [1 - 5]: \n");
                Console.WriteLine("**********************************\n");
                Console.WriteLine("1 - Withdraw");
                Console.WriteLine("2 - Deposit");
                Console.WriteLine("3 - Transfer funds");
                Console.WriteLine("4 - Print");
                Console.WriteLine("5 - Add New Account");
                Console.WriteLine("6 - Print Transaction History");
                Console.WriteLine("7 - Quit\n");

                Console.WriteLine("**********************************");
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Error occurred!");
                    option = -1;
                }

            }
            while (option < 1 || option > 7);
            return (MenuOption)(option - 1);
        }

        private static void NewAccount()
        {
            Console.WriteLine("\nName of the account:");
            string accountName = Console.ReadLine().ToLower();

            Console.WriteLine("\nStarting balance: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            Account newAccount = new Account(accountName, amount);
            Bank.AddAccount(newAccount);
        }

        private static void PrintTransactions(Bank bank)
        {
            bank.PrintTranscationHistory();
        }

        private static Account FindAccount(Bank fromBank)
        {
            Console.Write("\nEnter account name: ");
            String name = Console.ReadLine().ToLower();
            Account result = fromBank.GetAccount(name);
            if (result == null)
            {
                Console.WriteLine($"No account found with name {name}");
            }
            return result;
        }

        private static void DoDeposit(Bank toBank)
        {
            Account toAccount = FindAccount(toBank);

            if (toAccount == null) return;

            Console.WriteLine("Enter the deposit amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            DepositTransaction depositTransaction = new DepositTransaction(toAccount, amount);

            toBank.ExecuteTransaction(depositTransaction);
            depositTransaction.Print();
        }

        private static void DoWithdraw(Bank fromBank)
        {
            decimal amount;
            Account fromAccount = FindAccount(fromBank);

            if (fromAccount == null) return;

            Console.WriteLine("Enter the withdraw amount: ");
            amount = Convert.ToDecimal(Console.ReadLine());

            WithdrawTransaction transaction = new WithdrawTransaction(fromAccount, amount);

            fromBank.ExecuteTransaction(transaction);
            transaction.Print();
        }

        private static void DoTransfer(Bank bank)
        {
            Console.WriteLine("\nFrom Account ");
            Console.WriteLine("..........................\n");

            Account fromAccount = FindAccount(bank);
            if (fromAccount == null) return;

            Console.WriteLine("\nTo Account ");
            Console.WriteLine("..........................\n");

            Account toAccount = FindAccount(bank);
            if (toAccount == null) return;

            decimal amount;
            Console.WriteLine("Enter amount to transfer: ");
            amount = Convert.ToDecimal(Console.ReadLine());

            TransferTransaction transferTransaction = new TransferTransaction(fromAccount, toAccount, amount);

            bank.ExecuteTransaction(transferTransaction);
            transferTransaction.Print();
        }

        private static void DoPrint(Account account)
        {
            try
            {
                account.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! " + ex);
            }

        }
    }
}
