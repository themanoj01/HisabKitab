using HisabKitab.Model;

namespace HisabKitab.Pages
{
    public partial class Registration
    {
        private Users user = new Users();
        private string errormessage = null; 
        private async Task HandleRegistration()
        {
            try
            {
                user.Balance = 0;
                await UserService.RegisterUserAsync(user);
                Nav.NavigateTo("/login");
                Console.WriteLine("Registration successfull!");
            }
            catch (Exception ex)
            {
                errormessage = ex.Message;
            }
        }
    }
}