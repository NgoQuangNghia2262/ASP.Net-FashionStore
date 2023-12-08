using Bussiness.Exceptions;
using Model;
using Model.Interface;
using System.Data;

namespace Bussiness
{
    public class Bussiness<T> where T : IKey// Class này có nhiệm vụ convert và Validate dữ liệu từ DAL -> GUI , class này đại diện cho layer BUS để giao tiếp với GUI , đã xong không sửa nữa
    {
        public static void Save(object obj)
        {
            try
            {
                ControlManager.InstanceForICRUAndBUS instance = ControlManager.CreateInstanceForICRUDAndBUS(typeof(T));
                instance.BUS.ValidateModelData(obj);
                instance.ICrud.Save(obj);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("object obj không phải là 1 Model" + ex.Message);
            }
        }

        public static async void Delete(IKey obj)
        {
            try
            {
                ControlManager.InstanceForICRUAndBUS instance = ControlManager.CreateInstanceForICRUDAndBUS(typeof(T));
                instance.BUS.ValidateKeyModel(obj);
                if (!await instance.BUS.ExistsModel(obj))
                {
                    throw new DataConstraintViolationException("Obj chưa có trong cơ sở dữ liệu , không thể xóa");
                }
                instance.ICrud.Delete(obj);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("object obj không phải là 1 Model" + ex.Message);
            }
        }
        public static async Task<ResponseResult<T[]>> FindAll(int PageSize, int PageNumber)
        {
            try
            {
                ResponseResult<T[]> result = new ResponseResult<T[]>();
                DataAccess.Interface.ICRUD instance = ControlManager.CreateInstanceForICRUDAndBUS(typeof(T)).ICrud;
                DataTable data = await instance.FindAll(PageSize, PageNumber);
                if (data.Rows.Count == 0) { throw new DataNotFoundException("No datas found."); }
                int totalRows = int.Parse(data.Rows[0]["TotalRows"]?.ToString() ?? "");
                result.Data = Middleware.Convert<T>.DatatableToModel(data);
                result.TotalRows = totalRows;
                return result;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }
        public static async Task<ResponseResult<T>> FindOne(IKey obj)
        {
            try
            {
                ResponseResult<T> result = new ResponseResult<T>();
                ControlManager.InstanceForICRUAndBUS instance = ControlManager.CreateInstanceForICRUDAndBUS(typeof(T));
                instance.BUS.ValidateKeyModel(obj);
                DataTable data = await instance.ICrud.FindOne(obj);
                if (data.Rows.Count == 0) { throw new DataNotFoundException("No datas found."); }
                result.Data = Middleware.Convert<T>.DataRowToModel(data.Rows[0]);
                return result;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("object obj không phải là 1 Model" + ex.Message);
            }
        }
        public static void Test()
        {
            _ = DataAccess.DataProvider.Instance.ExecuteQueryAsync("select * from Account");
        }
    }
}
