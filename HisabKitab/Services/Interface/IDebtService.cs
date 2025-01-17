using HisabKitab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HisabKitab.Services.Interface
{
    public interface IDebtService
    {
        public void AddDebt(Debts debt);
        public List<Debts> GetAllDebts();
        public List<Debts> GetPendingDebts();
        public decimal GetTotalDebtAmount();
        public decimal GetClearedDebtAmount();
        public void DeleteDebt(Guid id);
        public void ClearDebt(Guid id);
        public void ClearDebtFromInflow(decimal amount);

    }
}
