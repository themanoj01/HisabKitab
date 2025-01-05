using HisabKitab.Abstraction;
using HisabKitab.Model;
using HisabKitab.Services.Interface;

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
        }

        // Add a new debt
        public void AddDebt(Debts debt)
        {
            if (debt.Amount <= 0)
                throw new ArgumentException("Debt amount must be positive");
            debt.Status = DebtStatus.Pending;
            debt.AmountDue = debt.Amount;
            debts.Add(debt);
            SaveDebts(debts);
        }

        // Get all debts
        public List<Debts> GetAllDebts()
        {
            return debts;
        }      

        // Delete a debt by its ID
        public void DeleteDebt(Guid id)
        {
            var debtToRemove = debts.FirstOrDefault(d => d.Id == id);
            if (debtToRemove != null)
            {
                debts.Remove(debtToRemove);
                SaveDebts(debts);
            }
        }

        // Clear debt (update the amount paid)
        // logic is not clear
        public void ClearDebt(Guid id)
        {
            var debt = debts.FirstOrDefault(d => d.Id == id) ?? throw new ArgumentException("Debt not found");
            decimal totalBalance = transactionService.TotalBalance();
            if (totalBalance < debt.AmountDue)
            {
                debt.AmountDue -= totalBalance;
                throw new InvalidOperationException("Not enough balance to clear debt but partially paid from balance");
            }

            if (debt.AmountDue == 0)
            {
                debt.Status = DebtStatus.Cleared;
            }
            SaveDebts(debts);
        }
        public List<Debts> GetPendingDebts()
        {
            return debts.Where(d => d.Status == DebtStatus.Pending).ToList();
        }

        public void ClearDebtFromInflow(decimal amount)
        {
            var pendingDebts = GetPendingDebts(); 
            foreach (var debt in pendingDebts)
            {
                if (amount >= debt.Amount)
                {
                    amount -= debt.Amount;
                    debt.Status = DebtStatus.Cleared;  
                }
                else
                {
                    debt.Amount -= amount;
                    break;
                }
            }
        }
        

    }
}
