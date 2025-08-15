using System;
using System.Collections.Generic;
using FinanceManagementSystem.Models;
using FinanceManagementSystem.Processors;

namespace FinanceManagementSystem
{
    public class FinanceApp
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public void Run()
        {
            var account = new SavingsAccount("SAV-001", 1000m);

            var transactions = new List<Transaction>
            {
                new Transaction(1, DateTime.Now, 150m, "Groceries"),
                new Transaction(2, DateTime.Now, 75m, "Utilities"),
                new Transaction(3, DateTime.Now, 200m, "Entertainment")
            };

            var processors = new Dictionary<string, ITransactionProcessor>
            {
                ["Groceries"] = new MobileMoneyProcessor(),
                ["Utilities"] = new BankTransferProcessor(),
                ["Entertainment"] = new CryptoWalletProcessor()
            };

            foreach (var transaction in transactions)
            {
                if (processors.TryGetValue(transaction.Category, out var processor))
                {
                    processor.Process(transaction);
                    account.ApplyTransaction(transaction);
                    _transactions.Add(transaction);
                }
            }

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