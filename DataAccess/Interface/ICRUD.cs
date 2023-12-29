using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface ICRUD
    {
        Task<DataTable> FindAll(int PageSize, int PageNumber);
        Task<DataTable> FindOne(IKey name);
        Task Save(dynamic obj);
        Task Delete(IKey obj);
    }
}
