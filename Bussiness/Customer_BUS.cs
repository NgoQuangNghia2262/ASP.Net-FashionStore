

using System.Data;
using Bussiness.Exceptions;
using Bussiness.Interface;
using DataAccess;
using DataAccess.Interface;
using Model;
using Model.Interface;

namespace Bussiness
{
    public class Customer_BUS : IValidate, ICustomer_BUS
    {
        private readonly ICRUD crud = new Customer_DAL();
        private readonly ICustomer_DAL dal = new Customer_DAL();
        public async Task<bool> ExistsModel(IKey obj)
        {
            return true;
        }

        public async Task<Customer> FindOneByAccount(Account account)
        {
            DataTable dt = await dal.FindOneByAccount(account);
            if (dt.Rows.Count == 0) { throw new DataNotFoundException("Không có khách hàng nào đăng ký account này"); }
            return new Customer(dt.Rows[0]);
        }

        public bool ValidateKeyModel(IKey obj)
        {
            return true;
        }

        public bool ValidateModelData(object obj)
        {
            return true;
        }
    }
}