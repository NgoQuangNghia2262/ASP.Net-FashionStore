using Model.Interface;

namespace Bussiness.Interface
{
    internal interface IValidate
    {
        bool ValidateModelData(object obj);
        bool ValidateKeyModel(IKey obj);
        Task<bool> ExistsModel(IKey obj);
    }
}
