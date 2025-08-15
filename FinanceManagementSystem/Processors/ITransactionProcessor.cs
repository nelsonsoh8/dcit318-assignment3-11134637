using FinanceManagementSystem.Models;

namespace FinanceManagementSystem.Processors
{
    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }
}