using BankingApp;  // Reference your main namespace
using NUnit.Framework;
using System;

namespace BankingApp.Tests
{
    [TestFixture]
    public class BankAccountTests
    {
        private BankAccount account;

        [SetUp]
        public void Setup()
        {
            account = new BankAccount(1000m);
        }

        [Test]
        public void Constructor_ThrowsException_WhenInitialBalanceIsNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BankAccount(-1m));
        }

        [Test]
        public void Deposit_IncreasesBalance_WhenAmountIsPositive()
        {
            account.Deposit(500m);
            Assert.AreEqual(1500m, account.Balance);
        }

        [Test]
        public void Deposit_ThrowsException_WhenAmountIsNegativeOrZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Deposit(0m));
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Deposit(-100m));
        }

        [Test]
        public void Withdraw_DecreasesBalance_WhenAmountIsValid()
        {
            bool result = account.Withdraw(400m);
            Assert.IsTrue(result);
            Assert.AreEqual(600m, account.Balance);
        }

        [Test]
        public void Withdraw_ReturnsFalse_WhenAmountExceedsBalance()
        {
            bool result = account.Withdraw(2000m);
            Assert.IsFalse(result);
            Assert.AreEqual(1000m, account.Balance);
        }

        [Test]
        public void Withdraw_ThrowsException_WhenAmountIsNegativeOrZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Withdraw(0m));
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Withdraw(-50m));
        }

        [Test]
        public void BalanceChanged_EventIsRaised_WhenBalanceChanges()
        {
            bool eventRaised = false;
            account.BalanceChanged += (sender, e) => eventRaised = true;

            account.Deposit(100m);
            Assert.IsTrue(eventRaised);

            eventRaised = false;
            account.Withdraw(100m);
            Assert.IsTrue(eventRaised);
        }
    }
}
