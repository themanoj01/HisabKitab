using HisabKitab.Model;

namespace HisabKitab.Abstraction
{
    public interface ITransactionService
    {
        Task AddTransactionAsync(Transactions transaction);
        Task<List<Transactions>> GetTransactionsAsync();
        Task<List<Transactions>> FilterTransactionsByTypeAsync(TransactionType type);
        Task<List<Transactions>> SearchTransactionsAsync(string searchQuery, DateTime? startDate = null, DateTime? endDate = null);
        Task<List<Transactions>> FilterTransactionsByTagAsync(string tag);
        Task<List<Transactions>> FilterTransactionsByDefaultTagAsync(DefaultTags tag);
        Task<List<Transactions>> SortTransactionsByDateAsync();
        Task<decimal> GetTotalBalanceAsync();
        Task<List<Transactions>> GetPendingDebtsAsync();
        Task<decimal> GetTotalDebtAsync();
        Task<decimal> GetTotalInflowAsync();
        Task<decimal> GetTotalOutflowAsync();
    }
}
