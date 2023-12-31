

using System.Data;
using Bussiness.Exceptions;
using Bussiness.Helper;
using Bussiness.Interface;
using DataAccess;
using DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Model;
using Model.Interface;

namespace Bussiness
{
    public class Customer_BUS : IValidate, ICustomer_BUS
    {
        private readonly ICRUD crud = new Customer_DAL();
        private readonly ICustomer_DAL dal = new Customer_DAL();
        private string getIdCustomerFromCookies(HttpContext context)
        {
            string idCustomer = "";
            try
            {
                string token = ActionCookie.GetCookieName(context, "accessToken");
                Account account = ActionJWT.VerifyJwtToken(token);
                idCustomer = account.customer == null ? "" : account.customer.id;

            }
            catch (NotAuthenticated)
            {
                if (string.IsNullOrEmpty(context.Request.Cookies["newCustomer"]))
                {
                    throw new Exception("Không tìm thấy khách hàng");
                }
                else
                {
                    idCustomer = context.Request.Cookies["newCustomer"] ?? "";
                }

            }
            return idCustomer;
        }
        public async Task<bool> ExistsModel(IKey obj)
        {
            return true;
        }
        public async void Purchase(HttpContext context, BillingDetail detail)
        {
            string idCustomer = "";
            try
            {
                //Đã đăng nhập
                string token = ActionCookie.GetCookieName(context, "accessToken");
                Account account = ActionJWT.VerifyJwtToken(token);
                ICustomer_BUS customer_bus = new Customer_BUS();
                ResponseResult<Customer> res = await Bussiness<Customer>.FindOne(account.customer);
                idCustomer = res.Data.id;
            }
            catch (NotAuthenticated)
            {
                if (string.IsNullOrEmpty(context.Request.Cookies["newCustomer"]))
                {
                    //Chưa đăng nhập và chưa mua sản phẩm nào
                    string randomString = StringUtility.GenerateRandomString(64);
                    ActionCookie.AddCookie(context, "newCustomer", randomString);
                    idCustomer = randomString;
                    await Bussiness<Customer>.Save(new Customer(randomString));
                }
                else
                {
                    //Chưa đăng nhập và đã mua vài sản phẩm
                    idCustomer = context.Request.Cookies["newCustomer"];
                }

            }
            await dal.Purchase(detail, idCustomer);
        }
        public async void PlacingAnOrder(HttpContext context, string note)
        {
            string idCustomer = getIdCustomerFromCookies(context);
            await dal.PlacingAnOrder(idCustomer, note);
        }
        public async Task<BillingDetail[]> GetCartForCustomer(HttpContext context)
        {
            string idCustomer = getIdCustomerFromCookies(context);
            DataTable dt = await dal.GetCartForCustomer(idCustomer);
            if (dt.Rows.Count == 0) { throw new DataNotFoundException("No results found."); }
            BillingDetail[] list = new BillingDetail[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BillingDetail billing = new BillingDetail(dt.Rows[i]);
                billing.product = new Product(dt.Rows[i]);
                list[i] = billing;
            }
            return list;
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

        public async void RemoveProductsFromCart(HttpContext context, BillingDetail detail)
        {
            string idCustomer = getIdCustomerFromCookies(context);
            await dal.RemoveProductsFromCart(detail, idCustomer);
        }
    }
}