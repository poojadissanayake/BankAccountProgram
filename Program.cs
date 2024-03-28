using System;
using System.Runtime.InteropServices;
using SplashKitSDK;

namespace Bank
{
    public enum MenuOption
    {
        Withdraw,
        Deposit,
        Print,
        Quit
    }
    public class Program
    {
        public static void Main()
        {
            Account account = new Account("Jake's Account", 20000);
            Account myAccount = new Account("Pooja's Account", 30000);

            MenuOption userSelection;

            do
            {
                userSelection = ReadUserOption();

                switch (userSelection)
                {
                    case MenuOption.Deposit:
                        DoDeposit(myAccount);
                        break;

                    case MenuOption.Withdraw:
                        DoWithdraw(myAccount);
                        break;

                    case MenuOption.Print:
                        DoPrint(myAccount);
                        break;

                    case MenuOption.Quit:
                        Console.WriteLine("Quitting..");
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
                Console.WriteLine("Choose an option [1-4]: ");
                Console.WriteLine("**********************************");
                Console.WriteLine("1 - Withdraw");
                Console.WriteLine("2 - Deposit");
                Console.WriteLine("3 - Print");
                Console.WriteLine("4 - Quit");
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
            while (option < 1 || option > 4);
            return (MenuOption)(option - 1);
        }

        private static void DoDeposit(Account account)
        {
            decimal amount;

            Console.WriteLine("Enter the deposit amount: ");
            amount = Convert.ToDecimal(Console.ReadLine());

            DepositTransaction depositTransaction = new DepositTransaction(account,amount);
            depositTransaction.Execute();
            depositTransaction.Print();
        }

        private static void DoWithdraw(Account account)
        {
            decimal amount;

            Console.WriteLine("Enter the withdraw amount: ");
            amount = Convert.ToDecimal(Console.ReadLine());

            WithdrawTransaction transaction = new WithdrawTransaction(account,amount);
            transaction.Execute();
            transaction.Print();
        }

        private static void DoPrint(Account account)
        {
            account.Print();
        }
    }
}
