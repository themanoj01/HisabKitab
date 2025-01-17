using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HisabKitab.Model
{
    public enum TransactionType
    {
        Credit,
        Debit,
        Debt
    }
    public enum DefaultTags
    {
        Yearly,
        Monthly,
        Food,
        Drinks,
        Clothes,
        Gadgets,
        Miscellaneous,
        Fuel,
        Rent,
        EMI,
        Party
    }

    public class Transactions
    {
        public Guid TransactionId { get; set; } = Guid.NewGuid();
        [StringLength(100, ErrorMessage = "Title must be between 3 and 100 characters", MinimumLength = 3)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType Type { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DefaultTags? DefaultTags { get; set; }

        public string CustomTag { get; set; }
        public string Notes { get; set; }

        // For debts 
        public string SourceOfDebt { get; set; } 
        public DateTime? DueDate { get; set; } 
    }
}
