using HisabKitab.Model;
using System.Text.Json;

namespace HisabKitab.Abstraction
{
    public abstract class UserBase
    {
        protected static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "users.json");
        protected List<Users> LoadUsers()
        {
            if (!File.Exists(FilePath)) return new List<Users>();
            var json = File.ReadAllText(FilePath);
            try
            {
                return JsonSerializer.Deserialize<List<Users>>(json) ?? new List<Users>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return new List<Users>();
            }
        }

        protected void SaveUsers(List<Users> users)
        {
            var json = JsonSerializer.Serialize(users);
            File.WriteAllText(FilePath, json);
        }
    }
}
