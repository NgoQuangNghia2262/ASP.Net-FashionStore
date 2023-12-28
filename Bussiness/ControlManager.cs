using Bussiness.Interface;
using DataAccess;
using Model;
using System.Data;

namespace Bussiness
{
    internal class ControlManager // Class này có nhiệm vụ điều hướng
    {
        internal struct InstanceForICRUAndBUS
        {
            internal DataAccess.Interface.ICRUD ICrud;
            internal IValidate BUS;
        }
        internal static InstanceForICRUAndBUS CreateInstanceForICRUDAndBUS(Type type)
        {
            if (type == null) { throw new ArgumentNullException("Không thể tạo instance cho 1 model null"); }
            InstanceForICRUAndBUS result = new InstanceForICRUAndBUS();
            string objType = type.Name;
            switch (objType)
            {
                case "Account":
                    {
                        result.ICrud = new Account_DAL();
                        result.BUS = new Account_BUS();
                        break;
                    }
                case "Product":
                    {
                        result.ICrud = new Product_DAL();
                        result.BUS = new Product_BUS();
                        break;
                    }
                case "Bill":
                    {
                        result.ICrud = new Bill_DAL();
                        result.BUS = new Bill_BUS();
                        break;
                    }
                default: { throw new ArgumentException($"Cannot create instance for object {objType}. Check layer Bussiness at class ControlManager in func CreateInstanceForICRUDAndBUS"); }
            }
            return result;
        }

    }
}
