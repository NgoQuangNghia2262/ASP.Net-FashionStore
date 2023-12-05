using Bussiness.Interface;
using Model;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness
{
    internal class Product_BUS : IValidate, IProduct_BUS
    {
        public bool ValidateKeyModel(IKey obj)
        {
            Product? product = obj as Product;
            throw new NotImplementedException();
        }

        public bool ValidateModelData(object obj)
        {
            Product? product = obj as Product;
            throw new NotImplementedException();
        }
        public Product[] FindProductByWords(string word)
        {
            throw new NotImplementedException();
        }

        public bool ExistsModel(IKey obj)
        {
            throw new NotImplementedException();

        }
    }
}
