using System;
using System.Data;
using System.Threading.Tasks;
using DataAccess.Interface;
using Model;
using Model.Interface;

namespace DataAccess
{
    public class BillingDetail_DAL : ICRUD
    {
        public async Task Delete(IKey obj)
        {
            IKeyBillingDetail? keyBillingDetail = obj as IKeyBillingDetail;
            if (keyBillingDetail == null)
            {
                throw new InvalidCastException("dynamic obj không phải là 1 BillingDetails");
            }

            string query = $"DELETE FROM BillingDetails WHERE id = {keyBillingDetail.id}";
            await DataProvider.Instance.ExecuteNonQueryAsync(query);
        }

        public Task<DataTable> FindAll(int PageSize, int PageNumber)
        {
            string query = $"Exec GetPagedBillingDetails @PageSize = {PageSize}, @PageNumber = {PageNumber}";
            return DataProvider.Instance.ExecuteQueryAsync(query);
        }

        public Task<DataTable> FindOne(IKey obj)
        {
            IKeyBillingDetail? keyBillingDetail = obj as IKeyBillingDetail;
            if (keyBillingDetail == null)
            {
                throw new InvalidCastException("dynamic obj không phải là 1 BillingDetails");
            }

            string query = $"SELECT * FROM BillingDetails WHERE id = {keyBillingDetail.id}";
            return DataProvider.Instance.ExecuteQueryAsync(query);
        }

        public async Task Save(dynamic obj)
        {
            BillingDetail? billingDetail = obj as BillingDetail;
            if (billingDetail == null)
            {
                throw new InvalidCastException("dynamic obj không phải là 1 BillingDetails");
            }
            string query = $"EXEC UpsertBillingDetails @id = {billingDetail.id} , " +
                           $"@nameProduct = N'{billingDetail.product?.name}' , " +
                           $"@sizeProduct = N'{billingDetail.product?.size}' , " +
                           $"@colorProduct = N'{billingDetail.product?.color}' , " +
                           $"@idBill = {billingDetail.bill?.id} , " +
                           $"@quantity = {billingDetail.quantity}";

            await DataProvider.Instance.ExecuteNonQueryAsync(query);
        }
    }
}
