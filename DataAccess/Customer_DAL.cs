using System.Data;
using DataAccess.Interface;
using Model;
using Model.Interface;

namespace DataAccess
{
    public class Customer_DAL : ICRUD, ICustomer_DAL
    {
        public Task Delete(IKey obj)
        {
            throw new NotImplementedException();
        }
        public Task<DataTable> FindAll(int PageSize, int PageNumber)
        {
            throw new NotImplementedException();
        }
        public Task<DataTable> FindOne(IKey name)
        {
            IKeyCustomer? customer = name as IKeyCustomer;
            if (customer == null) { throw new InvalidCastException("dynamic obj không phải là 1 customer"); }
            return DataProvider.Instance.ExecuteQueryAsync($"select * from customer where id = '{customer.id}'");
        }
        public async Task Save(dynamic obj)
        {
            Customer? customer = obj as Customer;
            if (customer == null) { throw new InvalidCastException("dynamic obj không phải là 1 customer"); }
            string customerQuery = $@"EXEC UpsertCustomer 
            @id = '{customer.id}',
            @phone = '{customer.phone}',
            @name = N'{customer.name}',
            @gmail = '{customer.gmail}',
            @address = N'{customer.address}'";
            await DataProvider.Instance.ExecuteNonQueryAsync(customerQuery);
        }
        public Task<DataTable> FindOneByAccount(Account account)
        {
            return DataProvider.Instance.ExecuteQueryAsync($"select * from Customer where account = '{account.username}'");
        }
        public async Task PlacingAnOrder(string idCustomer, string note)
        {
            string query = $"update Bill set note = N'{note}', status = 'Delivering' From Bill " +
            $"inner join Customer on Customer.id = Bill.customer" +
            $" where customer = '{idCustomer}' and status = N'UnPaid'";
            await DataProvider.Instance.ExecuteNonQueryAsync(query);
        }
        public Task<DataTable> GetCartForCustomer(string idCustomer)
        {
            string query = $"select BillingDetails.* , Product.* from BillingDetails " +
            "inner join Bill on BillingDetails.idBill = Bill.id " +
            "inner join Product on Product.name = BillingDetails.nameProduct and Product.color = BillingDetails.colorProduct and Product.size = BillingDetails.sizeProduct " +
            $"where customer = '{idCustomer}' and status = N'UnPaid'";
            return DataProvider.Instance.ExecuteQueryAsync(query);
        }

        public async Task RemoveProductsFromCart(BillingDetail detail, string idCustomer)
        {
            string query = $"delete BillingDetails from BillingDetails " +
            "inner join Bill on BillingDetails.idBill = Bill.id " +
            "inner join Customer on Customer.id = Bill.customer " +
            $"where nameProduct = N'{detail.product.name}' and sizeProduct = '{detail.product.size}' and colorProduct = N'{detail.product.color}' " +
            $"and customer = '{idCustomer}' and status = N'UnPaid'";
            await DataProvider.Instance.ExecuteNonQueryAsync(query);
        }
        public async Task Purchase(BillingDetail detail, string khachhang)
        {
            string query = $@"EXEC Purchase
            @name = N'{detail.product.name}',
            @color = N'{detail.product.color}',
            @size = '{detail.product.size}',
            @quantity= {detail.quantity},
            @customer = '{khachhang}'";
            await DataProvider.Instance.ExecuteNonQueryAsync(query);
        }

    }
}