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
        private Customer? _customer;
        public string username
        {
            get => _username;
            set => _username = value;
        }
        public string? password { get => _password; set => _password = value?.Trim(); }
        public string? permissions { get => _permissions; set => _permissions = value?.Trim(); }
        public Customer? customer { get => _customer; set => _customer = value; }

        public Account(DataRow row)
        {
            if (row == null)
            {
                return;
            }

            if (row.Table.Columns.Contains("username"))
            {
                _username = row["username"].ToString().Trim();
            }

            if (row.Table.Columns.Contains("password"))
            {
                _password = row["password"].ToString().Trim();
            }

            if (row.Table.Columns.Contains("permissions"))
            {
                _permissions = row["permissions"].ToString().Trim();
            }
            if (row.Table.Columns.Contains("customer"))
            {
                _customer = new Customer(row["customer"].ToString() ?? "");
            }
        }
        public Account() { }
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
