using DataAccess.Interface;
using Model;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Product_DAL : ICRUD
    {
        public void Delete(IKey obj)
        {
            throw new NotImplementedException();
        }
        public DataTable FindAll()
        {
            throw new NotImplementedException();
        }

        public DataTable FindOne(IKey name)
        {
            throw new NotImplementedException();
        }
        public void Save(dynamic obj)
        {
            throw new NotImplementedException();
        }
    }
}
