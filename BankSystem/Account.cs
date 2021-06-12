using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    public class Account
    {
        public double Balance;
        public readonly string Name;
        public Account(string name, double balance)
        {
            this.Name = name;
            this.Balance = balance;
        } // Object Constructor.
        public String GetName() => this.Name;
        public double GetBalance() => this.Balance;
        public Boolean Deposit(double amount)
        {
            if (amount > 0)
            {
                this.Balance += amount;
                return true;
            }
            else
            {
                return false;
            }
        } // Add a given amount to an 'account'.
        public Boolean Withdraw(double amount)
        {
            if (amount > 0)
            {
                if (amount > this.Balance)
                {
                    return false;
                }
                else
                {
                    this.Balance -= amount;
                    return true;
                }
            }
            else
            {
                return false;
            }
            
        } // Take a given amount from an 'account'.
        public void Print()
        {
            Console.WriteLine("Account Name: " + GetName() +
                "\nAccount Balance: " + GetBalance().ToString("C"));
        } // Output information about an account.

    }
}
