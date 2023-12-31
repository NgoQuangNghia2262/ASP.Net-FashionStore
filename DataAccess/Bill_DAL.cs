using DataAccess.Interface;
using Model;
using Model.Interface;
using System.Data;

namespace DataAccess
{
    public class Bill_DAL : ICRUD, IBill_DAL
    {
        public async Task Delete(IKey obj)
        {
            IKeyBill? keyBill = obj as IKeyBill;
            if (keyBill == null)
            {
                throw new InvalidCastException("dynamic obj không phải là 1 Bill");
            }

            string query = $"DELETE FROM Bill WHERE id = {keyBill.id}";
            await DataProvider.Instance.ExecuteNonQueryAsync(query);
        }

        public Task<DataTable> FindAll(int PageSize, int PageNumber)
        {
            string query = $"Exec GetPagedBills @PageSize = {PageSize}, @PageNumber = {PageNumber}";
            return DataProvider.Instance.ExecuteQueryAsync(query);
        }

        public Task<DataTable> FindOne(IKey obj)
        {
            IKeyBill? keyBill = obj as IKeyBill;
            if (keyBill == null)
            {
                throw new InvalidCastException("dynamic obj không phải là 1 Bill");
            }

            string query = $"SELECT * FROM Bill WHERE id = {keyBill.id}";
            return DataProvider.Instance.ExecuteQueryAsync(query);
        }

        public async Task Save(dynamic obj)
        {
            Bill? bill = obj as Bill;
            if (bill == null)
            {
                throw new InvalidCastException("dynamic obj không phải là 1 Bill");
            }
            string query = $"EXEC UpsertBill @id = {bill.id} , @date = '{bill.date.ToString("yyyy-MM-dd HH:mm:ss")}' , @status = N'{bill.status}',@discount = {bill.discount},@customer_id = '{(bill.customer?.id)}'";
            await DataProvider.Instance.ExecuteNonQueryAsync(query);
        }

        public Task<DataTable> GetBillingDetails(int idbill)
        {
            string query = $"select * from BillingDetails where idBill = {idbill}";
            return DataProvider.Instance.ExecuteQueryAsync(query);
        }
    }
}
