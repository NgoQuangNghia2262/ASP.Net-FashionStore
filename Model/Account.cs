using Model.Interface;
using System.ComponentModel.DataAnnotations;
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
        }
        public string? password { get => _password; set => _password = value?.Trim(); }
        public string? permissions { get => _permissions; set => _permissions = value?.Trim(); }
        public Account(DataRow row)
        {
            _username = row["username"].ToString();
            _password = row["password"].ToString();
            _permissions = row["permissions"].ToString();
        }
        public Account(string username)
        {
            _username = username;
        }
        public Account(string username, string? password, string? permissions)
        {
            this._username = username;
            this.password = password;
            this.permissions = permissions;
        }
    }
}
