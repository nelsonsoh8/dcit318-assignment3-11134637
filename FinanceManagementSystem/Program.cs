using System;
using System.Collections.Generic;

namespace FinanceManagementSystem
{
    // 1a. Transaction record (immutable financial data)
    public record Transaction(
        int Id,
        DateTime Date,
        decimal Amount,
        string Category
    );

    // 1b. Transaction processor interface
    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }

    // 1c. Processor implementations
    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[Bank Transfer] Processed {transaction.Amount:C} for {transaction.Category}");
        }
    }

    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[Mobile Money] Processed {transaction.Amount:C} for {transaction.Category}");
        }
    }

    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[Crypto Wallet] Processed {transaction.Amount:C} for {transaction.Category}");
        }
    }

    // 1d. Base Account class
    public class Account
    {
        public string AccountNumber { get; }
        public decimal Balance { get; protected set; }

        public Account(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public virtual void ApplyTransaction(Transaction transaction)
        {
            Balance -= transaction.Amount;
            Console.WriteLine($"Deducted {transaction.Amount:C}. New balance: {Balance:C}");
        }
    }

    // 1e. Sealed SavingsAccount
    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, decimal initialBalance) 
            : base(accountNumber, initialBalance) { }

        public override void ApplyTransaction(Transaction transaction)
        {
            if (transaction.Amount > Balance)
            {
                Console.WriteLine("Insufficient funds");
                return;
            }
            
            base.ApplyTransaction(transaction);
            Console.WriteLine($"Savings account balance: {Balance:C}");
        }
    }

    // 1f. FinanceApp implementation
    public class FinanceApp
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public void Run()
        {
            // 1f.i. Create savings account
            var savingsAccount = new SavingsAccount("SAV-001", 1000m);
            
            // 1f.ii. Create transactions
            var transaction1 = new Transaction(1, DateTime.Now, 150m, "Groceries");
            var transaction2 = new Transaction(2, DateTime.Now, 75m, "Utilities");
            var transaction3 = new Transaction(3, DateTime.Now, 200m, "Entertainment");

            // 1f.iii. Process transactions with specific processors
            var mobileMoneyProcessor = new MobileMoneyProcessor();
            var bankTransferProcessor = new BankTransferProcessor();
            var cryptoWalletProcessor = new CryptoWalletProcessor();

            mobileMoneyProcessor.Process(transaction1);
            bankTransferProcessor.Process(transaction2);
            cryptoWalletProcessor.Process(transaction3);

            // 1f.iv. Apply transactions to account
            savingsAccount.ApplyTransaction(transaction1);
            savingsAccount.ApplyTransaction(transaction2);
            savingsAccount.ApplyTransaction(transaction3);

            // 1f.v. Store transactions
            _transactions.Add(transaction1);
            _transactions.Add(transaction2);
            _transactions.Add(transaction3);

            Console.WriteLine("\nAll transactions processed successfully!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Finance Management System");
            new FinanceApp().Run();
        }
    }
}