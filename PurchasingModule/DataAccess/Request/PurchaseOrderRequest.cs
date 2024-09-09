using PurchasingModule.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingModule.DataAccess.Request
{
    public class PurchaseOrderRequest
    {
        public int SupplierId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } // E.g., "Pending", "Completed", "Cancelled"
        public List<PurchaseOrderItemRequest> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItemRequest>();
    }
}
