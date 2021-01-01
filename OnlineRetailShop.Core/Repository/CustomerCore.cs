using OnlineRetailShop.Core.Interface;
using OnlineRetailShop.Data.DBContext;
using OnlineRetailShop.Model; 
using System;
using System.Data;

namespace OnlineRetailShop.Core.Repository
{
    public class CustomerCore : ICustomerCore
    {
        public OnlineRetailShopEntity dbContext;
        public CustomerCore(OnlineRetailShopEntity onlineRetailShopEntity)
        {
            dbContext = onlineRetailShopEntity;

        }
        public DataTable AddCustomer(CustomerInput customerInput, out string dbresult)
        {
            try
            {
                return null;
               
            }
            catch (Exception ex)
            {
                return CaptureError.CaptureErrorMsg(out dbresult, ex);
            }
        }
    }
}
