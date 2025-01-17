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
        public Guid DebtId { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Source is required")]

        public string Source { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }
        public DateTime TakenDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } 
        public decimal AmountDue { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DebtStatus Status { get; set; }
        public string Notes {  get; set; }
    }
}
