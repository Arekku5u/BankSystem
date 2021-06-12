using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    public class TransferTransaction : Transaction
    {
        private readonly Account _fromAccount;
        private readonly Account _toAccount;
        private readonly double _amount;
        private DepositTransaction _deposit;
        private WithdrawTransaction _withdraw;
        private bool _reversed = false;
        private bool _executed = false;
        private bool _success = false;
        
        public TransferTransaction(Account fromAccount, Account toAccount, double amount) : base(amount)
        {
            this._fromAccount = fromAccount;
            this._toAccount = toAccount;
            this._amount = amount;
        } // Object Constructor
        public override void Print()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Transfer Details:");
            Console.WriteLine("Accounts Details: " + _fromAccount.GetName() + " -> " + _toAccount.GetName());
            Console.WriteLine("Accounts Balances: " + _fromAccount.Balance.ToString("C") + " -> " + _toAccount.Balance.ToString("C"));
            Console.WriteLine("Transfer Amount: " + _amount.ToString("C"));
            Console.WriteLine("----------------------------------");
        } // Print the details of the transaction
        public override void Execute()
        {
            if (_amount > _fromAccount.GetBalance()) 
            {
                throw new InvalidOperationException("Insufficient Funds\n");
            } // Gets the balance of the account and checks the account won't go into negatives. If it will throws an exception.
            else if (_executed == true)
            {
                throw new InvalidOperationException("Process already executed!\n");
            } // Check the the operation hasn't already been done in that instance of the method being called. Throw an exception otherwise.
            else
            {
                _withdraw = new WithdrawTransaction(_fromAccount, _amount);
                _withdraw.Execute();
                _deposit = new DepositTransaction(_toAccount, _amount);
                _deposit.Execute();
                _executed = true;
                _success = true;
            } // If all checks are green. Reduce the amount from the accounts balance and change _executed and _success to true.
        } // Method for preforming the named operation.
        public override void Rollback()
        {
            if (_reversed == false)
            {
                _withdraw = new WithdrawTransaction(_fromAccount, _amount);
                _withdraw.Rollback();
                _deposit = new DepositTransaction(_toAccount, _amount);
                _deposit.Rollback();
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
        } // Method for reversing the named operation.
    
        public bool Executed()
        {
            return _executed;
        } // Executed Method
        public bool Success()
        {
            return _success;
        } // Success Method
        public bool Reversed()
        {
            return _reversed;
        } // Reversed Method
    }
}
