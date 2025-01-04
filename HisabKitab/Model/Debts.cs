using System.ComponentModel.DataAnnotations;

namespace HisabKitab.Model
{
    public enum DebtStatus
    {
        Pending,
        Cleared
    }
    public class Debts
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Source { get; set; }
        public decimal AmountDue { get; set; }
        public DateTime TakenDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal AmountPaid { get; set; }
        public DebtStatus status { get; set; }
    }
}
