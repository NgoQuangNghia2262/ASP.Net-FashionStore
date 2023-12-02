using Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interface
{
    internal interface IValidate
    {
        bool ValidateModelData(object obj);
        bool ValidateKeyModel(IKey obj);
    }
}
