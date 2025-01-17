using HisabKitab.Model;
using HisabKitab.Services;
using MudBlazor;

namespace HisabKitab.Pages
{
    public partial class Dashboard
    {
        private decimal TotalInflows { get; set; }
        private decimal TotalOutflows { get; set; }
        private decimal TotalTransaction { get; set; }
        private decimal TotalDebt { get; set; }
        private decimal ClearedDebt { get; set; }
        private decimal RemainingDebt { get; set; }
        private int NoOfTotalNoOfTransaction { get; set; }
        private decimal TotalBalance {  get; set; }
        private List<Debts> pendingDebts = new List<Debts>();
        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; }


        private List<Transactions> highestInflowTransactions;
        private List<Transactions> lowestInflowTransactions;
        private List<Transactions> highestOutflowTransactions;
        private List<Transactions> lowestOutflowTransactions;
        private List<Transactions> highestDebtTransactions;
        private List<Transactions> lowestDebtTransactions;

        private int Index = -1; 
        int dataSize = 4;
        double[] data;
        string[] labels = { "Credit", "Debit", "Debt"};

        private string[] Labels;
        private double[] Values;


        protected override void OnInitialized()
        {
            TotalInflows = TransactionService.CalculateTotalInflow();
            TotalOutflows = TransactionService.CalculateTotalOutflow();
            TotalTransaction = TransactionService.GetTotalTransactionAmount();
            TotalDebt = DebtService.GetTotalDebtAmount();
            ClearedDebt = DebtService.GetClearedDebtAmount();
            RemainingDebt = TotalDebt - ClearedDebt;
            NoOfTotalNoOfTransaction = TransactionService.GetTotalTransactionCount();
            TotalBalance = TransactionService.TotalBalance();

            data = new double[] { Convert.ToDouble(TotalInflows) , Convert.ToDouble(TotalOutflows), Convert.ToDouble(TotalDebt) };

            var lowestTransactions = TransactionService.GetTop5Lowest();
            Labels = lowestTransactions.Select(t => string.IsNullOrEmpty(t.Title) ? "Unnamed" : t.Title).ToArray();
            Values = lowestTransactions.Select(t => Convert.ToDouble(t.Amount)).ToArray();

            highestInflowTransactions = TransactionService.GetTop5HighestInflow();
            lowestInflowTransactions = TransactionService.GetTop5LowestInflow();
            highestOutflowTransactions = TransactionService.GetTop5HighestOutflow();
            lowestOutflowTransactions = TransactionService.GetTop5LowestOutflow();
            highestDebtTransactions = TransactionService.GetTop5HighestDebt();
            lowestDebtTransactions = TransactionService.GetTop5LowestDebt();
            pendingDebts = DebtService.GetPendingDebts();


            
        }
        private void FilterByDateRange()
        {
            if (StartDate != default(DateTime) && EndDate != default(DateTime))
            {
                pendingDebts = DebtService.GetPendingDebts()
                    .Where(d => d.DueDate >= StartDate && d.DueDate <= EndDate)
                    .ToList();
            }
            else
            {
                DebtService.GetAllDebts();
            }
        }

    }
}
