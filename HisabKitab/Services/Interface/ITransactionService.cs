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

        public List<Transactions> GetTop5Highest();

        public List<Transactions> GetTop5Lowest();

        public void UpdateTransaction(Guid transactionId, Transactions updatedTransaction);
        // List<Transactions> GetHighestOrLowestTransactions(string transactionType, int count);

        public List<Transactions> GetTop5LowestDebt();
        public List<Transactions> GetTop5HighestDebt();
        public List<Transactions> GetTop5LowestOutflow();
        public List<Transactions> GetTop5HighestOutflow();
        public List<Transactions> GetTop5LowestInflow();
        public List<Transactions> GetTop5HighestInflow();
    }
}
    

