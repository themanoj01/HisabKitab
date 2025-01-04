using HisabKitab.Model;

namespace HisabKitab.Services
{
    public class DebtService
    {
        private readonly JsonStorageService<Debts> _storage;

        public DebtService()
        {
            _storage = new JsonStorageService<Debts>("debts.json");
        }

        // Add a new debt
        public void AddDebt(Debts debt)
        {
            _storage.Add(debt);
        }

        // Get all debts
        public List<Debts> GetDebts()
        {
            return _storage.GetAll();
        }

        // Delete a debt by its ID
        public void DeleteDebt(Guid id)
        {
            _storage.Delete(id, debt => debt.Id);
        }

        // Clear debt (update the amount paid)
        public void ClearDebt(Guid id, decimal amount)
        {
            var debts = _storage.GetAll();
            var debt = debts.FirstOrDefault(d => d.Id == id);
            if (debt != null)
            {
                debt.AmountPaid += amount;
                _storage.SaveAll(debts); // Save updated debts
            }
        }
    }
}
