using HisabKitab.Abstraction;
using HisabKitab.Model;
using HisabKitab.Services.Interface;
using Microsoft.AspNetCore.Components;


namespace HisabKitab.Services
{
    public class DebtService : DebtBase, IDebtService
    {
        private List<Debts> debts;
        private Users users;
        private TransactionService transactionService;
        public DebtService()
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
            debts = LoadDebts();
            transactionService = new TransactionService();
        }

        // Add a new debt
        public void AddDebt(Debts debt)
        {
            if (debt.Amount <= 0)
                throw new ArgumentException("Debt amount must be positive");
            debt.Status = DebtStatus.Pending;
            debts.Add(debt);
            SaveDebts(debts);
        }
        // Get all debts
        public List<Debts> GetAllDebts()
        {
            return debts;
        }
        public decimal GetTotalDebtAmount()
        {
            return debts.Sum(debt => debt.Amount);
        }

        public decimal GetClearedDebtAmount()
        {
            return debts.Where(debt => debt.Status == DebtStatus.Cleared)
                .Sum(debt => debt.Amount);
        }


        public void DeleteDebt(Guid id)
        {
            var debtToRemove = debts.FirstOrDefault(d => d.DebtId == id);
            if (debtToRemove != null)
            {
                debts.Remove(debtToRemove);
                SaveDebts(debts);
            }
        }

        public void ClearDebt(Guid id)
        {
            var debt = debts.FirstOrDefault(d => d.DebtId == id) ?? throw new ArgumentException("Debt not found");
            if (debt == null)
            {
                throw new InvalidOperationException("Debt not found.");
            }

            if (debt.Status != DebtStatus.Pending)
            {
                throw new InvalidOperationException("Debt is already cleared or does not exist.");
            }
            transactionService.DeductFromBalance(debt.Amount);
            debt.Status = DebtStatus.Cleared;
            SaveDebts(debts);
        }
        public List<Debts> GetPendingDebts()
        {
            return debts.Where(d => d.Status == DebtStatus.Pending).ToList();
        }
    
    }
}
