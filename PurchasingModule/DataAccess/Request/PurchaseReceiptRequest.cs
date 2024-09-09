using PurchasingModule.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingModule.DataAccess.Request
{
    public class PurchaseReceiptRequest
    {
        public PurchaseReceiptRequest()
        {
            ReceiptItems = new List<PurchaseReceiptItemRequest>();
        }
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public DateTime ReceiptDate { get; set; } = DateTime.Now;
        public List<PurchaseReceiptItemRequest> ReceiptItems { get; set; }
    }
}
