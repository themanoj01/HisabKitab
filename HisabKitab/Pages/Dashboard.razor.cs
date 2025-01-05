using HisabKitab.Model;

namespace HisabKitab.Pages
{
    public partial class Dashboard
    {
        private decimal TotalInflows { get; set; }
        private decimal TotalOutflows { get; set; }
        private decimal Balance { get; set; }
        private decimal PendingDebts { get; set; }
        private List<Transactions> RecentTransactions { get; set; } = new();
        private List<Debts> PendingDebtList { get; set; } = new();

        private int NoOfTotalNoOfTransaction { get; set; }

        private decimal TotalTransaction { get; set; }

        protected override void OnInitialized()
        {
            // Fetch data
            TotalInflows = TransactionService.CalculateTotalInflow();
            TotalOutflows = TransactionService.CalculateTotalOutflow();
            Balance = TotalInflows - TotalOutflows;
            PendingDebtList = DebtService.GetPendingDebts();
            TotalTransaction = TransactionService.GetTotalTransactionAmount();
            NoOfTotalNoOfTransaction = TransactionService.GetTotalTransactionCount();
            PendingDebts = PendingDebtList
                .Where(debt => debt.Status == DebtStatus.Pending)
                .Sum(debt => debt.Amount);

            // Fetch recent transactions
            var allTransactions = TransactionService.GetAllTransactions();
            RecentTransactions = allTransactions
                    .OrderByDescending(transaction => transaction.Amount)
                    .Take(5)
                    .ToList();

            // Fetch pending debts
            PendingDebtList = DebtService.GetPendingDebts();
        }
    }
}