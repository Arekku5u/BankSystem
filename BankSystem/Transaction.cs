using System;

namespace BankSystem
{
    public class Transaction
    {
        private double _amount;
        private bool _success;
        private bool _executed;
        private bool _reversed;
        public DateTime _dateStamp;

        private bool Success { get; set; }
        private bool Executed { get; set; }
        private bool Reversed { get; set; }
        private DateTime DateStamp { get; set; }
        public Transaction(double amount)
        {
            this._amount = amount;
        } // Object Constructor.
        public virtual void Print(){} // Print Method. Get's overridden by the three transaction classes.
        public virtual void Execute(){} // Execute Method. Get's overridden by the three transaction classes.
        public virtual void Rollback(){} // Rollback Method. Get's overridden by the three transaction classes.
    }
}