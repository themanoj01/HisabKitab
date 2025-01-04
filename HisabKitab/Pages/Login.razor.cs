using HisabKitab.Model;

namespace HisabKitab.Pages
{
    public partial class Login
    {
        private Users user = new Users();
        private string loginErrorMessage = null;

        private async Task HandleLogin()
        {
            try
            {
                var Loggeduser = await UserService.LoginUserAsync(user.Username, user.Password);
                if (Loggeduser != null)
                {
                    Nav.NavigateTo("/inflow");
                    Console.WriteLine("Login successfull!");
                }
            }
            catch (Exception ex)
            {
                loginErrorMessage = ex.Message;
            }
            {
            }
        }
    }
}