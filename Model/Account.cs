using Model.Interface;
using System.Data;

namespace Model
{
   
    public class Account : IKeyAccount
    {
        private string _username;
        private string? _password;
        private string? _permissions;
        

      
        public string username 
        {
            get => _username;
            set
            {
                if(value == null) { throw new ArgumentNullException(nameof(value)); }
                _username = value.Trim();
            }
        }
        public string? password { get => _password; set => _password = value?.Trim(); }
        public string? permissions { get => _permissions; set => _permissions = value?.Trim(); }
        public Account()
        {
            username = "";
        }

        public Account(string username, string? password, string? permissions)
        {
            this.username = username;
            this.password = password;
            this.permissions = permissions;
        }
    }
}
