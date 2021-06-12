using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    public class DepositTransaction : Transaction
    {
        private readonly Account _account;
        private readonly double _amount;
        private bool _executed = false;
        private bool _success = false;
        private bool _reversed = false;
        public DepositTransaction(Account account, double amount) : base(amount)
        {
            this._account = account;
            this._amount = amount;
           
             
        }  // Object Constructor
        public override void Print()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Transaction Details:");
            Console.WriteLine("Account Number: " + _account.GetName());
            Console.WriteLine("Account Balance: " + _account.Balance.ToString("C"));
            Console.WriteLine("Deposit Amount: " + _amount.ToString("C"));
            Console.WriteLine("----------------------------------");
        }   // Print the details of the transaction
        public override void Execute()
        {
            if (_amount <= 0)
            {
                throw new InvalidOperationException("Deposit amount must be more than $0!\n");
            } // Make sure the amount given is greater 0. Throw an exception otherwise.
            else if (_executed == true)
            {
                throw new InvalidOperationException("Process already executed!\n");
            } // Check the the operation hasn't already been done in that instance of the method being called. Throw an exception otherwise.
            else
            {
                _account.Balance += _amount;
                _executed = true;
                _success = true;
            } // If all checks are green. Reduce the amount from the accounts balance and change _executed and _success to true.
        } // Method for preforming the named operation.
        public override void Rollback()
        {
            if (_reversed == false)
            {
                _account.Balance -= _amount;
                _reversed = true;
            } // Check that the rollback method hasn't already been called on that instance. Preform the rollback if it hasn't.
            else if (_reversed == true)
            {
                throw new InvalidOperationException("Rollback already occured!\n");
            } // If it has throw an exception.
            else if (_success == false)
            {
                throw new InvalidOperationException("Transaction was not successful!\n");
            } // If the operation failed somewhere. Throw an exception.
        } // Method for reversing the named operation
        
        public bool Executed()
        {
            return _executed;
        } // Executed Method
        public bool Success()
        {
            return _success;
        }  // Success Method
        public bool Reversed()
        {
            return _reversed;
        } // Reversed Method

    }
}
