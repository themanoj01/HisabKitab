using HisabKitab.Services;
using HisabKitab.Model;
using HisabKitab.Abstraction;
using Microsoft.AspNetCore.Components;
namespace HisabKitab.Pages
{
    public partial class Transaction
    {
        private Transactions transaction = new Transactions();
        private List<Transactions> transactions = new List<Transactions>();

        private void HandleTransactionSubmit()
        {
            TransactionService.AddTransaction(transaction);
            Nav.NavigateTo("/transaction");
        }
        protected override void OnInitialized()
        {
            transactions = TransactionService.GetAllTransactions()
                .ToList(); ;
        }
   
    }

}

