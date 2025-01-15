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
        private readonly UserService userService;
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
        }

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

        public decimal TotalBalance()
        {
            decimal totalInflow = CalculateTotalInflow();
            decimal totalOutflow = CalculateTotalOutflow();
            decimal balance = totalInflow - totalOutflow;


            return balance;
        }

        public List<Transactions> GetAllTransactions()
        {
            return LoadTransactions();
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
            
            decimal currentBalance = TotalBalance();
            return currentBalance >= amount;
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


    }
}
