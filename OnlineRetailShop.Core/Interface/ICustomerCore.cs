using OnlineRetailShop.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OnlineRetailShop.Core.Interface
{
    public interface ICustomerCore
    {
        DataTable AddCustomer(CustomerInput customerInput, out string dbresult);
    }
}
