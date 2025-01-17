using HisabKitab.Abstraction;
using HisabKitab.Model;
using HisabKitab.Services.Interface;
using System.Text.Json;

namespace HisabKitab.Services
{
    public class UserService : UserBase, IUserService
    {
        private List<Users> _users;
        public const string SeedUsername = "Manoj";
        public const string SeedPassword = "manoj123";
        public const string SeedCurrency = "NPR";
        public UserService()
        {
            _users = LoadUsers();
            if (!_users.Any())
            {
                _users.Add(new Users(SeedUsername, SeedPassword, SeedCurrency));
                SaveUsers(_users);
            }
        }
        public Users GetCurrentUser()
        {
            var users = LoadUsers();
            return users.First();
        }
        public bool Login(Users user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }
            return _users.Any(u => u.Username == user.Username && u.Password == user.Password);
        }

    }
}
