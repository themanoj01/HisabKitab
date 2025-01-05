using HisabKitab.Model;
namespace HisabKitab.Pages
{
    public partial class Inflow
    {
        private Transactions transaction = new Transactions();
        private List<Transactions> transactions = new List<Transactions>();
        private void HandleTransactionSubmit()
        {
            transaction.Type = TransactionType.Credit;
            TransactionService.AddTransaction(transaction);
            Nav.NavigateTo("/inflow");
        }
        protected override void OnInitialized()
        {
            transactions = TransactionService.GetAllTransactions().Where(txn => txn.Type == TransactionType.Credit)
                .ToList(); ;
        }
        /*private void GetAllTransactions()
        {
            transactions = TransactionService.GetAllTransactions()
                 .Where(txn => txn.Type == TransactionType.Credit)
                .ToList();
        }*/

    }
}