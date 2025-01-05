using HisabKitab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HisabKitab.Abstraction
{
    public abstract class TransactionBase
    {
        protected static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "transactions.json");
        protected List<Transactions> LoadTransactions()
        {
            if (!File.Exists(FilePath)) return new List<Transactions>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Transactions>>(json) ?? new List<Transactions>();
        }

        protected void SaveTransactions(List<Transactions> transactions)
        {
            var json = JsonSerializer.Serialize(transactions);
            File.WriteAllText(FilePath, json);
        }
    }

}
