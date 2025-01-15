using HisabKitab.Model;
using HisabKitab.Services;
using System.Linq;
namespace HisabKitab.Pages
{
    public partial class Outflow
    {
        private Transactions transaction = new Transactions();
        private List<Transactions> transactions = new List<Transactions>();
        private string Message { get; set; } = string.Empty;
        private bool IsSuccess { get; set; }
        private void HandleTransactionSubmit()
        {
            try
            {
                if (!TransactionService.HasSufficientBalance(transaction.Amount))
                {
                    Message = "Not enough balance to process this outflow.";
                    IsSuccess = false;
                }
                else
                {
                    transaction.Type = TransactionType.Debit;
                    Message = "Transaction done successfully!";
                    IsSuccess = true;
                    TransactionService.AddTransaction(transaction);
                    transaction = new Transactions();
                }
            }
            catch (Exception ex)
            {
                Message = "An exception encountered: " + ex.Message;
            }
        }
        protected override void OnInitialized()
        {
            transactions = TransactionService.GetAllTransactions().Where(txn => txn.Type == TransactionType.Debit)
                .ToList(); 
        }
    }
}