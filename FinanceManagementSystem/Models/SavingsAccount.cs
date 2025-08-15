namespace FinanceManagementSystem.Models
{
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
}