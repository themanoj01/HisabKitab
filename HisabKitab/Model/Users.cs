using System.ComponentModel.DataAnnotations;

namespace HisabKitab.Model
{
    public class Users
    {
        public string Username { get; set; }
       
        public string Password { get; set; }
   
        public string Currency {  get; set; }

        public Users() 
        { 

        }
        public Users(string Username, string Password, string Currency)
        {
            this.Username = Username;
            this.Password = Password;
            this.Currency = Currency;
        }

    }
}
