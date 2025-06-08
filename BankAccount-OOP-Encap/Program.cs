using System;

namespace BankingApp
{   
    public class BankAccount
    {
        private decimal balance;  // Private field

        public decimal Balance => balance;  // Read-only property

        public BankAccount(decimal initialBalance)
        {
            if (initialBalance < 0)
                throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance cannot be negative.");

            balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive.");

            balance += amount;
            OnBalanceChanged();
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive.");

            if (amount > balance)
                return false;

            balance -= amount;
            OnBalanceChanged();
            return true;
        }

        public event EventHandler? BalanceChanged;

        protected virtual void OnBalanceChanged()
        {
            BalanceChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
