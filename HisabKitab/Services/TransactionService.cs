using HisabKitab.Abstraction;
using HisabKitab.Model;
using System.Text.Json;

namespace HisabKitab.Services
{
    public class TransactionService : TransactionBase, ITransactionService
    {
        // Add a transaction (Credit, Debit, or Debt)
        public async Task AddTransactionAsync(Transactions transaction)
        {
            var transactions = LoadTransactions();
            transactions.Add(transaction);
            SaveTransactions(transactions);
        }

        // Get all transactions
        public async Task<List<Transactions>> GetTransactionsAsync()
        {
            return LoadTransactions();
        }

        // Filter transactions by type (Credit, Debit, Debt)
        public async Task<List<Transactions>> FilterTransactionsByTypeAsync(TransactionType type)
        {
            var transactions = LoadTransactions();
            return transactions.Where(t => t.Type == type).ToList();
        }

        // Search transactions by title, amount, or date range
        public async Task<List<Transactions>> SearchTransactionsAsync(string searchQuery, DateTime? startDate = null, DateTime? endDate = null)
        {
            var transactions = LoadTransactions();

            // Apply search filter
            var filteredTransactions = transactions.Where(t =>
                (string.IsNullOrEmpty(searchQuery) || t.Notes.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)) &&
                (!startDate.HasValue || t.Date >= startDate.Value) &&
                (!endDate.HasValue || t.Date <= endDate.Value)
            ).ToList();

            return filteredTransactions;
        }

        // Filter transactions by custom tags
        public async Task<List<Transactions>> FilterTransactionsByTagAsync(string tag)
        {
            var transactions = LoadTransactions();
            return transactions.Where(t => t.CustomTag != null && t.CustomTag.TagName.Equals(tag, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Filter transactions by default tags (e.g., Food, Rent, etc.)
        public async Task<List<Transactions>> FilterTransactionsByDefaultTagAsync(DefaultTags tag)
        {
            var transactions = LoadTransactions();
            return transactions.Where(t => t.DefaultTags == tag).ToList();
        }

        // Sort transactions by date
        public async Task<List<Transactions>> SortTransactionsByDateAsync()
        {
            var transactions = LoadTransactions();
            return transactions.OrderBy(t => t.Date).ToList();
        }

        // Get the total balance (inflows - outflows)
        public async Task<decimal> GetTotalBalanceAsync()
        {
            var transactions = LoadTransactions();
            decimal totalInflow = transactions.Where(t => t.Type == TransactionType.Credit).Sum(t => t.Amount);
            decimal totalOutflow = transactions.Where(t => t.Type == TransactionType.Debit).Sum(t => t.Amount);
            return totalInflow - totalOutflow;
        }

        // Get total debt (outstanding)
        public async Task<decimal> GetTotalDebtAsync()
        {
            var transactions = LoadTransactions();
            return transactions.Where(t => t.Type == TransactionType.Debt).Sum(t => t.Amount);
        }

        // Get total cash inflows
        public async Task<decimal> GetTotalInflowAsync()
        {
            var transactions = LoadTransactions();
            return transactions.Where(t => t.Type == TransactionType.Credit).Sum(t => t.Amount);
        }

        // Get total cash outflows
        public async Task<decimal> GetTotalOutflowAsync()
        {
            var transactions = LoadTransactions();
            return transactions.Where(t => t.Type == TransactionType.Debit).Sum(t => t.Amount);
        }

        // Get pending debts (debts that have not been cleared yet)
        public async Task<List<Transactions>> GetPendingDebtsAsync()
        {
            var transactions = LoadTransactions();
            return transactions.Where(t => t.Type == TransactionType.Debt && t.Amount > 0).ToList();
        }

        // Save transactions to the file
        private void SaveTransactions(List<Transactions> transactions)
        {
            var json = JsonSerializer.Serialize(transactions);
            File.WriteAllText(FilePath, json);
        }

        // Check if user has sufficient balance before proceeding with a debit transaction
        public async Task<bool> CanPerformDebitAsync(decimal amount)
        {
            var balance = await GetTotalBalanceAsync();
            if (balance >= amount)
            {
                return true;
            }
            else
            {
                // Handle insufficient funds (notify the user)
                return false;
            }
        }

        // Cleared debts are subtracted from the total debt
        public async Task<decimal> GetClearedDebtAsync()
        {
            var transactions = LoadTransactions();
            decimal totalDebt = GetTotalDebtAsync().Result;
            decimal totalPayments = transactions.Where(t => t.Type == TransactionType.Debt && t.Amount < 0).Sum(t => t.Amount);
            return totalPayments > 0 ? totalPayments : 0;
        }

        // Get remaining debt (total debt - cleared debt)
        public async Task<decimal> GetRemainingDebtAsync()
        {
            var totalDebt = await GetTotalDebtAsync();
            var clearedDebt = await GetClearedDebtAsync();
            return totalDebt - clearedDebt;
        }
    }
}