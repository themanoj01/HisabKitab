using HisabKitab.Abstraction;
using HisabKitab.Model;
using HisabKitab.Services.Interface;
using System.Text.Json;

namespace HisabKitab.Services
{
    public class UserService : UserBase,IUserService
    {
        private Users users;
        public UserService()
        {
            // Ensure the directory exists
            var directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create the file if it doesn't exist
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]");
            }

        }       

        public async Task RegisterUserAsync(Users user)
        {
            var users = await LoadUsersAsync();

            if (users.Any(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Username already exists.");

            users.Add(user);
            await SaveUsersAsync();
        }

        public async Task<Users> LoginUserAsync(string username, string password)
        {
            var users = await LoadUsersAsync();
            var user = users.FirstOrDefault(u =>
                    u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password == password);

            return user == null ? throw new Exception("Invalid username or password.") : user;
        }

        public async Task<bool> IsUserRegisteredAsync(string username)
        {
            var users = await LoadUsersAsync();
            return users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        private async Task<List<Users>> LoadUsersAsync()
        {
            var jsonData = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<Users>>(jsonData) ?? new List<Users>();
        }

        private async Task SaveUsersAsync()
        {
            var jsonData = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, jsonData);
        }
    }
}
