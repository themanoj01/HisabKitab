using HisabKitab.Model;
namespace HisabKitab.Pages
{
    public partial class Inflow
    {
        private Transactions transaction = new Transactions();
        private List<Transactions> transactions = new List<Transactions>();
        private string Message = string.Empty;
        private bool IsSuccess { get; set; }
        private void HandleTransactionSubmit()
        {
            try
            {
                transaction.Type = TransactionType.Credit;
                TransactionService.AddTransaction(transaction);
                IsSuccess = true;
                Message = "Transaction done successfully.";
                transaction = new Transactions();
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                Message = "Error encountered: "+ex.Message;
            }
            
        }
        protected override void OnInitialized()
        {
            transactions = TransactionService.GetAllTransactions().Where(txn => txn.Type == TransactionType.Credit)
                .ToList(); ;
        }
  
    }
}