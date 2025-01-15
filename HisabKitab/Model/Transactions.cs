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
        [Required(ErrorMessage = "Amount is required")]
        public string Title {  get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType Type { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DefaultTags? DefaultTags { get; set; }

        public string CustomTags {  get; set; }
        public string Notes { get; set; }
    }
}
