using System;

namespace BankSystem
{
    internal enum MenuOption
    {
        TransactionHistory,
        Withdraw,
        Deposit,
        Transfer,
        Add,
        Print,
        Quit,
        Testing
    } // Enum for the user options

    internal static class BankSystem
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Banking Program Starting...");
            Console.WriteLine("Account with name: TestAccount\n" +
                              "with Balance: $1000 has been added\n" +
                              "for testing convenience.");
            
            // Test account to make new feature testing easier.
            Account testAccount = new Account("TestAccount", 1000);
            
            // Initialise a new bank.
            Bank bank = new Bank();
            // Add the test account to the bank array _accounts.
            bank.AddAccount(testAccount);
            
            bool x = false;
            while (x == false)
            {
                MenuOption menuOption = ReadUserOption();
                Account wantedAccount;
                switch (menuOption)
                {
                    case MenuOption.TransactionHistory:
                        PrintTransactionsHistory(bank);
                        break;
                    case MenuOption.Withdraw:
                        DoWithdraw(bank);
                      
                        break;
                    case MenuOption.Deposit:
                        DoDeposit(bank);
                        break;
                    case MenuOption.Transfer:
                        DoTransfer(bank);
                        break;
                    case MenuOption.Add:
                        Console.Write("Enter Account Name: ");
                        var name = Console.ReadLine(); // Grab the account name from console.
                        Console.Write("Enter Starting Balance: ");
                        var balance = Convert.ToInt32(Console.ReadLine()); // Grab a starting balance from the console.
                        Account newAccount = new Account(name, balance); // Create the account using the Account class.
                        bank.AddAccount(newAccount); // Add the account to the bank.
                        break;
                    case MenuOption.Print:
                        DoPrint(bank);
                        break;
                    case MenuOption.Quit:
                        Console.WriteLine("Thank you for banking with us!");
                        x = true; // Change the x variable to true to exit the while loop.
                        break;
                    case MenuOption.Testing:
                        Test(bank);
                        break;
                    default:
                        Console.WriteLine("Invalid Option. Try Again!");
                        break;

                }
            }
        } // Main method.
        private static MenuOption ReadUserOption()
        {
            do
            {
                Console.WriteLine("##########");
                Console.WriteLine("Main Menu");
                Console.WriteLine("##########");
                Console.WriteLine("1. Print Transaction History");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Add New Account");
                Console.WriteLine("6. Print");
                Console.WriteLine("7. Quit");
                Console.Write(": ");
                string userChoiceAsString = Console.ReadLine(); // Get the choice from the console.
                if (int.TryParse(userChoiceAsString, out var userChoice)) // Check that the input is an integer.
                {
                    switch (userChoice)
                    {
                        case 1:
                            return MenuOption.TransactionHistory; // Print all transactions that have happened in that given runtime of the application.
                        case 2:
                            return MenuOption.Withdraw; // Withdraw an amount from a given account
                        case 3:
                            return MenuOption.Deposit; // Deposit an amount into a given account
                        case 4:
                            return MenuOption.Transfer; // Transfer an amount between two accounts
                        case 5:
                            return MenuOption.Add; // Add an account to the bank.
                        case 6:
                            return MenuOption.Print; // Print the details of an account.
                        case 7:
                            return MenuOption.Quit; // Quit option to exit the application.
                        case 57:
                            return MenuOption.Testing; // Testing option for new features, user cannot see the option
                        default:
                            Console.WriteLine("Invalid Option Chosen. Try Again!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Input wasn't a number");
                }

            } while (true);

            return 0;
        }  // Output all options to the user (Minus the test option).
        private static void DoDeposit(Bank bank)
        {
            Account account = FindAccount(bank); // Find the account the user wants.
            if (account == null) // If the account can not be found output this text.
            {
                Console.WriteLine("#############################################");
                Console.WriteLine("An error has occured, check the account name!");
                Console.WriteLine("#############################################");
                return;
            }
            Console.Write("Enter Deposit Amount: ");
            int amount = Convert.ToInt32(Console.ReadLine()); // Grab the amount from user.
            DepositTransaction depositTransaction = new DepositTransaction(account, amount); // Create the deposit transaction.
            try
            {
                bank.Transaction(depositTransaction); // Call Transaction using the deposit transaction.
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            

        } // Method for depositing an amount into an account.
        private static void DoWithdraw(Bank bank)
        {
            Account account = FindAccount(bank); // Find the account the user wants.
            if (account == null) // If the account can not be found output this text.
            {
                Console.WriteLine("#############################################");
                Console.WriteLine("An error has occured, check the account name!");
                Console.WriteLine("#############################################");
                return;
            }
            Console.Write("Enter Withdraw Amount: ");
            int amount = Convert.ToInt32(Console.ReadLine()); // Grab the amount from user.
            WithdrawTransaction withdrawTransaction = new WithdrawTransaction(account, amount); // Create the withdraw transaction.
            try
            {
                bank.Transaction(withdrawTransaction); // Call Transaction using the withdraw transaction.
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        } // Method for withdrawing an amount from an account.
        private static void DoTransfer(Bank bank)
        {
            Account sender = FindAccount(bank); // Grab the account sending 'money'..
            Account receiver = FindAccount(bank); // Grab the account receiving 'money'.
            if (sender == null || receiver == null)
            {
                Console.WriteLine("##############################################");
                Console.WriteLine("An error has occured, check the account names!");
                Console.WriteLine("##############################################");
                return;
            } // If either accounts can not be found output this text.
            Console.Write("Enter Transfer Amount: ");
            int amount = Convert.ToInt32(Console.ReadLine()); // Grab the amount from the user.
            TransferTransaction transferTransaction = new TransferTransaction(sender, receiver, amount); // Create the transfer transaction 
            try
            {
                bank.Transaction(transferTransaction); // Call Transaction using the transfer transaction
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        } // Method for transferring between two accounts.
        private static Account FindAccount(Bank bank)
        {
            Console.Write("Enter wanted Account name: ");
            string wantedAccount = Console.ReadLine(); // Get the account name from the user

            if (bank.GetAccount(wantedAccount) != null) return bank.GetAccount(wantedAccount); // Check if the user account is there. If it is return it.
            return null;

        } // Method for finding the account the user wants.
        private static void DoPrint(Bank bank)
        {
            Account account = FindAccount(bank); // Find the account the user wants.
            if (account == null) // If the account doesn't exist return this error.
            {
                Console.WriteLine("#############################################");
                Console.WriteLine("An error has occured, check the account name!");
                Console.WriteLine("#############################################");
                return;
            }
            else // Otherwise call the Print() method.
            {
                account.Print();
            }
        } // Output information about a given account.
        private static void PrintTransactionsHistory(Bank bank)
        {
            bank.PrintTransactionsHistory(); // Call the PrintTransactionHistory() method from the Bank class
        } // Print the transaction history of the given application runtime.
        private static void Test(Bank bank)
        {
            Console.WriteLine("Nothing being tested right now");
        } // Method for testing new features and things being implemented.
    }
}
        
    

