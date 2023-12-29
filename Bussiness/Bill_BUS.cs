using System.Data;
using Bussiness.Interface;
using DataAccess;
using DataAccess.Interface;
using Model;
using Model.Interface;

namespace Bussiness
{
    public class Bill_BUS : IValidate, IBill_BUS
    {
        private ICRUD ICrud = new Bill_DAL();
        private IBill_DAL dal = new Bill_DAL();
        public async Task<bool> ExistsModel(IKey obj)
        {
            DataTable dt = await ICrud.FindOne(obj);
            return dt.Rows.Count > 0;
        }

        public async Task<BillingDetail[]> GetBillingDetails(int idbill)
        {
            DataTable dt = await dal.GetBillingDetails(idbill);
            BillingDetail[] list = Helper.Convert<BillingDetail>.DatatableToModel(dt);
            return list;
        }

        public void Purchase(BillingDetail detail, string khachhang)
        {
            IBill_DAL dal = new Bill_DAL();
            dal.Purchase(detail, khachhang);
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