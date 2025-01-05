using HisabKitab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HisabKitab.Abstraction
{
    public abstract class DebtBase
    {
        protected static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "debts.json");
        protected List<Debts> LoadDebts()
        {
            if (!File.Exists(FilePath)) return new List<Debts>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Debts>>(json) ?? new List<Debts>();
        }

        protected void SaveDebts(List<Debts> debts)
        {
            var json = JsonSerializer.Serialize(debts);
            File.WriteAllText(FilePath, json);
        }
    }

}
