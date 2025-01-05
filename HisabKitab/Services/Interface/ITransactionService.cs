using HisabKitab.Model;

namespace HisabKitab.Services.Interface
{
    public interface ITransactionService
    {
        void AddTransaction(Transactions transaction);
        List<Transactions> GetAllTransactions();
        // List<Transactions> GetTransactionsByType(string transactionType);
       // List<Transactions> GetTransactionsByTag(string tag);
        List<Transactions> GetTransactionsByDateRange(DateTime startDate, DateTime endDate);
        decimal CalculateTotalInflow();
        decimal CalculateTotalOutflow();
        public decimal TotalBalance();
        bool HasSufficientBalance(decimal amount);
        public int GetTotalTransactionCount();
        public decimal GetTotalTransactionAmount();
       // List<Transactions> GetHighestOrLowestTransactions(string transactionType, int count);
    }
}
    

