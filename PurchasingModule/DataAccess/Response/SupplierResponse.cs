using PurchasingModule.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingModule.DataAccess.Response
{
    public class SupplierResponse:BaseResponse
    {
        public Supplier Supplier { get; set; }
    }
}
