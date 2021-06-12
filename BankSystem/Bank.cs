using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace BankSystem
{
    public class Bank 
    {
        // List for different accounts
        private readonly List<Account> _accounts = new List<Account>(); // Create a list for the accounts.
        private readonly List<Transaction> _transactions = new List<Transaction>(); // Create a list for the transaction history.
        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        } // Method for adding new accounts to the accounts list.
        public Transaction PrintTransactionsHistory()
        {
            Console.WriteLine("Last Transaction was at: " + _transactions[^1]._dateStamp ); // Print the dateTime of the last element in the transactions list.
            for (var i = 0; i < _transactions.Count; i++)
            {
                Console.WriteLine((i + 1) + ": " +_transactions[i] + " State: Completed");
            } // Loop to output the all the values of _transactions.
            Console.Write("Do you wish to rollback any transactions? (Y / N) : ");
            var rollback = Convert.ToChar(Console.ReadLine()?.ToUpper() ?? string.Empty); // Get the choice from the user.
            switch (rollback)
            {
                case 'Y':
                    Console.Write("Which transaction number: ");
                    var transaction = Convert.ToInt32(Console.ReadLine()); // Get option from the user.
                    _transactions[transaction - 1].Rollback();
                    break;
                case 'N':
                    break;
                default:
                    Console.WriteLine("Please pick an option!");
                    break;
            } // Ask the user for the transaction number and call the Rollback() method of that index in _transactions.

            return null;
        } // Print the transaction history of the given application runtime.
        public Account GetAccount(string name)
        {
            for (var i = 0; i != _accounts.Count; i++)
            {
                if (name == _accounts[i].Name)
                {
                    return _accounts[i];
                }
                else
                {
                    continue;
                }
            }

            return null;
        } // Get an account from the _accounts list.
        public void Transaction(Transaction transaction)
        {
            transaction._dateStamp = DateTime.Now; // Change _dateStamp to the time when the method is called.
            _transactions.Add(transaction);
            transaction.Execute();
            transaction.Print();
        } // Method for executing transactions and adding them to the _transactions list.
        public void RollbackTransaction(Transaction transaction)
        {
            transaction.Rollback();
            transaction.Print();
        } // Reverse an given transaction 
    }
}