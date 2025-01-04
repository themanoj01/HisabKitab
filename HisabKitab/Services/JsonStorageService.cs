using System.Text.Json;

namespace HisabKitab.Services
{
    public class JsonStorageService<T>
    {
        private readonly string _filePath;

        public JsonStorageService(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]"); // Create an empty JSON array
            }
        }

        // Retrieve all data from the JSON file
        public List<T> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        // Save a list of items to the JSON file
        public void SaveAll(List<T> items)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        // Add a single item to the JSON file
        public void Add(T item)
        {
            var items = GetAll();
            items.Add(item);
            SaveAll(items);
        }

        // Remove an item by its ID (assumes models have a `Guid Id` property)
        public void Delete(Guid id, Func<T, Guid> idSelector)
        {
            var items = GetAll();
            var itemToRemove = items.FirstOrDefault(x => idSelector(x) == id);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
                SaveAll(items);
            }
        }
    }
}
