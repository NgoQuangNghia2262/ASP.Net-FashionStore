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
    }
}