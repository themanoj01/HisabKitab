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
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; } 

        public DefaultTags? DefaultTags { get; set; }
        public Tags CustomTag { get; set; }
        public string Notes { get; set; }
    }
}
