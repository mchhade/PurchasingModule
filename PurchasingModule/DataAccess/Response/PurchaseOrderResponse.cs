using PurchasingModule.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingModule.DataAccess.Response
{
    public class PurchaseOrderResponse
    {
        public PurchaseOrderResponse()
        {
            PurchaseOrderItems = new List<PurchaseOrderItemResponse>();
        }
        public int PurchaseOrderID { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } // E.g., "Pending", "Completed", "Cancelled"
        public List<PurchaseOrderItemResponse> PurchaseOrderItems { get; set; }
        public decimal TotalPrice => PurchaseOrderItems.Sum(x=>x.TotalPrice);

    }
}
