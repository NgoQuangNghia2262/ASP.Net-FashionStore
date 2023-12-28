using System.Data;
using Bussiness.Interface;
using DataAccess;
using DataAccess.Interface;
using Model.Interface;

namespace Bussiness
{
    public class Bill_BUS : IValidate
    {
        private ICRUD ICrud = new Bill_DAL();
        public async Task<bool> ExistsModel(IKey obj)
        {
            DataTable dt = await ICrud.FindOne(obj);
            return dt.Rows.Count > 0;
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