using HisabKitab.Model;
using System.Transactions;

namespace HisabKitab.Pages
{
    public partial class Debt
    {
        private Debts debt = new Debts();  
        private List<Debts> debts = new List<Debts>();  
        private void HandleDebtSubmit()
        {
            DebtService.AddDebt(debt);
            Nav.NavigateTo("/debt");
        }
        protected override void OnInitialized()
        {
            debts = DebtService.GetAllDebts()
                .ToList();
        }
    }
}