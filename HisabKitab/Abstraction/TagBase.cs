using HisabKitab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HisabKitab.Abstraction
{
    public abstract class TagBase
    {
        protected static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "tags.json");
        protected List<Tags> LoadTags()
        {
            if (!File.Exists(FilePath)) return new List<Tags>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Tags>>(json) ?? new List<Tags>();
        }

        protected void SaveTags(List<Tags> tags)
        {
            var json = JsonSerializer.Serialize(tags);
            File.WriteAllText(FilePath, json);
        }
    }

}
