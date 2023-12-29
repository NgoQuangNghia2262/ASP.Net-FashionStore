using Bussiness.Interface;
using Model.Interface;

namespace Bussiness
{
    public class BillingDetail_BUS : IValidate
    {
        public async Task<bool> ExistsModel(IKey obj)
        {
            return true;
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