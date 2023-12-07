using Model.Interface;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Model
{

    public class Account : IKeyAccount
    {
        [Required]
        private string _username;
        private string? _password;
        private string? _permissions;


        [Required]
        public string username
        {
            get => _username;
            set
            {
                if (value == null) { throw new ArgumentNullException(nameof(value)); }
                _username = value.Trim();
            }
        }
        public string? password { get => _password; set => _password = value?.Trim(); }
        public string? permissions { get => _permissions; set => _permissions = value?.Trim(); }
        public Account()
        {
            _username = "";
        }

        public Account(string username, string? password, string? permissions)
        {
            this._username = username;
            this.password = password;
            this.permissions = permissions;
        }
    }
}
