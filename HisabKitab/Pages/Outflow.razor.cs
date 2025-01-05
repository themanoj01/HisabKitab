using HisabKitab.Model;
using HisabKitab.Services;
using System.Linq;
namespace HisabKitab.Pages
{
    public partial class Outflow
    {
        private TransactionService transactionService;
        private Transactions transaction = new Transactions();
        private List<Transactions> transactions = new List<Transactions>();
        private string Error;
        private void HandleTransactionSubmit()
        {
            if (!transactionService.HasSufficientBalance(transaction.Amount))
            {
                Error = "Not enough balance to process this outflow.";
            }
            else
            {
                transaction.Type = TransactionType.Debit;
                TransactionService.AddTransaction(transaction);
                Nav.NavigateTo("/outflow");
            }
        }
        protected override void OnInitialized()
        {
            transactions = TransactionService.GetAllTransactions().Where(txn => txn.Type == TransactionType.Debit)
                .ToList(); ;
        }
        /*private void GetAllTransactions()
        {
            transactions = TransactionService.GetAllTransactions()
                 .Where(txn => txn.Type == TransactionType.Debit)
                .ToList();
        }*/

    }
}