using Bussiness.Exceptions;
using Model.Interface;
using System.Data;

namespace Bussiness
{
    public class Bussiness<T> // Class này có nhiệm vụ convert và Validate dữ liệu từ DAL -> GUI , class này đại diện cho layer BUS để giao tiếp với GUI , đã xong không sửa nữa
    {
        public static void Save(object obj)
        {
            try
            {
                ControlManager.InstanceForICRUAndBUS instance = ControlManager.CreateInstanceForICRUDAndBUS(typeof(T));
                instance.BUS.ValidateModelData(obj);
                instance.ICrud.Save(obj);
            }
            catch (ArgumentNullException ex) {
                throw ex;
            }
            catch (InvalidCastException ex) {
                throw new InvalidCastException("object obj không phải là 1 Model" + ex.Message);
            }
        }

        public static void Delete(IKey obj)
        {
            try
            {
                ControlManager.InstanceForICRUAndBUS instance = ControlManager.CreateInstanceForICRUDAndBUS(typeof(T));
                instance.BUS.ValidateKeyModel(obj);
                if(!instance.BUS.ExistsModel(obj)){
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
        public static T[] FindAll()
        {
            try
            {
                DataAccess.Interface.ICRUD instance = ControlManager.CreateInstanceForICRUDAndBUS(typeof(T)).ICrud;
                DataTable result = new DataTable();
                result = instance.FindAll();
                if (result.Rows.Count == 0) { throw new DataNotFoundException("No results found."); }
                return Middleware.Convert<T>.DatatableToModel(result);
            }catch (ArgumentException ex)
            {
                throw ex;
            }
        }
        public static T FindOne(IKey obj)
        {
            try
            {
                ControlManager.InstanceForICRUAndBUS instance = ControlManager.CreateInstanceForICRUDAndBUS(typeof(T));
                instance.BUS.ValidateKeyModel(obj);
                DataTable result = instance.ICrud.FindOne(obj);
                if (result.Rows.Count == 0) { throw new DataNotFoundException("No results found."); }
                return Middleware.Convert<T>.DataRowToModel(result.Rows[0]);
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
    }
}
