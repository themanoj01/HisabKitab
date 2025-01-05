using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        public decimal Amount { get; set; }
        public DateTime TakenDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public decimal AmountDue { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DebtStatus Status { get; set; }
        public string Notes {  get; set; }
    }
}
