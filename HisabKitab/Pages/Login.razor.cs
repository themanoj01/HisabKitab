using HisabKitab.Model;
using HisabKitab.Services;

namespace HisabKitab.Pages
{
    public partial class Login
    {
        private string? ErrorMessage;

         Users user = new Users();

        private void HandleLogin()
        {
            if (UserService.Login(user))
            {
                Nav.NavigateTo("/dashboard");
            }
            else
            {
                ErrorMessage = "Invalid username or password";
            }
        }
    }
}