using HisabKitab.Services.Interface;
using HisabKitab.Abstraction;
using HisabKitab.Model;
using System.Text.Json;
using System.Transactions;

namespace HisabKitab.Services
{
    public class TransactionService : TransactionBase, ITransactionService
    {
        private List<Transactions> transactions;
        private List<Debts> debts;
        private decimal balance;
        public TransactionService()
        {
            var directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]");
            }
            transactions = LoadTransactions();
            balance = TotalBalance();
        }
        //Add transaction method to save transactions
        public void AddTransaction(Transactions transaction)
        {
            transactions.Add(transaction);
            SaveTransactions(transactions);

        }

        public decimal CalculateTotalInflow()
        {
            return transactions.Where(t => t.Type == TransactionType.Credit)
                        .Sum(t => t.Amount);
        }

        public decimal CalculateTotalOutflow()
        {
            return transactions.Where(t => t.Type == TransactionType.Debit)
                                   .Sum(t => t.Amount);
        }

        public decimal CalculateTotalDebt()
        {
            return transactions.Where(t => t.Type == TransactionType.Debt)
                                   .Sum(t => t.Amount);
        }
        // Total balance calculated to ensure sufficient balance in the account
        public decimal TotalBalance()
        {
            decimal totalInflow = CalculateTotalInflow();
            decimal totalOutflow = CalculateTotalOutflow();
            decimal totalDebt = CalculateTotalDebt();
            balance = (totalInflow + totalDebt) - totalOutflow;


            return balance;
        }

        public List<Transactions> GetAllTransactions()
        {
            return LoadTransactions();
        }

        public List<Transactions> GetTop5Highest()
        {
            List<Transactions> allTransactions = GetAllTransactions();
            return allTransactions
                    .OrderByDescending(transaction => transaction.Amount)
                    .Take(5)
                    .ToList();
        }

        public List<Transactions> GetTop5Lowest()
        {
            List<Transactions> allTransactions = GetAllTransactions();
            return allTransactions
                    .OrderBy(transaction => transaction.Amount)
                    .Take(5)
                    .ToList();
        }

        public List<Transactions> GetTransactionsByDateRange(DateTime startDate, DateTime endDate)
        {
            return transactions.Where(t => t.Date >= startDate && t.Date <= endDate).ToList();
        }
        
                    

        public void UpdateTransaction(Guid transactionId, Transactions updatedTransaction)
        {


            var transaction = transactions.FirstOrDefault(t => t.TransactionId == transactionId);
            if (transaction == null)
            {
                throw new Exception("Transaction not found with id " + transactionId);
            }


            transaction.Type = updatedTransaction.Type;
            transaction.Amount = updatedTransaction.Amount;
            transaction.DefaultTags = updatedTransaction.DefaultTags;
            transaction.Notes = updatedTransaction.Notes;

            SaveTransactions(transactions);
        }

        public void DeleteTransaction(Guid transactionId)
        {
            var transactions = LoadTransactions();

            var transaction = transactions.FirstOrDefault(t => t.TransactionId == transactionId);
            if (transaction == null)
            {
                throw new Exception("Transaction not found.");
            }

            transactions.Remove(transaction);

            SaveTransactions(transactions);
        }

        public bool HasSufficientBalance(decimal amount)
        {
            return balance >= amount;
        }
        public decimal DeductFromBalance(decimal amount)
        {
            if (HasSufficientBalance(amount))
            {
                balance -= amount;
                return balance;
            }
            else
            {
                throw new InvalidOperationException("Insufficient balance to deduct.");
            }
        }
        public List<Transactions> SortTransactionsByDate(List<Transactions> transactions)
        {
            return transactions.OrderBy(t => t.Date).ToList();
        }

        public List<Transactions> SearchTransactions(List<Transactions> transactions, string searchQuery)
        {
            return transactions.Where(t => t.Notes.Contains(searchQuery)).ToList();
        }
        public int GetTotalTransactionCount()
        {
            return transactions.Count;
        }

        public decimal GetTotalTransactionAmount()
        {
            return transactions.Sum(t => t.Amount);
        }


        public decimal GetTotalDebt()
        {
            return transactions.Where(t => t.Type == TransactionType.Debt).Sum(t => t.Amount);
        }

        public List<Transactions> GetTop5HighestInflow()
        {
            return transactions.Where(t => t.Type == TransactionType.Credit)
                               .OrderByDescending(t => t.Amount)
                               .Take(5)
                               .ToList();
        }

        public List<Transactions> GetTop5LowestInflow()
        {
            return transactions.Where(t => t.Type == TransactionType.Credit)
                               .OrderBy(t => t.Amount)
                               .Take(5)
                               .ToList();
        }

        public List<Transactions> GetTop5HighestOutflow()
        {
            return transactions.Where(t => t.Type == TransactionType.Debit)
                               .OrderByDescending(t => t.Amount)
                               .Take(5)
                               .ToList();
        }

        public List<Transactions> GetTop5LowestOutflow()
        {
            return transactions.Where(t => t.Type == TransactionType.Debit)
                               .OrderBy(t => t.Amount)
                               .Take(5)
                               .ToList();
        }

        public List<Transactions> GetTop5HighestDebt()
        {
            return transactions.Where(t => t.Type == TransactionType.Debt)
                               .OrderByDescending(t => t.Amount)
                               .Take(5)
                               .ToList();
        }

        public List<Transactions> GetTop5LowestDebt()
        {
            return transactions.Where(t => t.Type == TransactionType.Debt)
                               .OrderBy(t => t.Amount)
                               .Take(5)
                               .ToList();
        }
    }
}
