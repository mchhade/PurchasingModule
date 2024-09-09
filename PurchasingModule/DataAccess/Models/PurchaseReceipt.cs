using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingModule.DataAccess.Models
{
    public class PurchaseReceipt
    {
        public int Id { get; set; }

        // Foreign key to PurchaseOrder
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        // Receipt date (default to current time)
        public DateTime ReceiptDate { get; set; } = DateTime.Now;

        // One-to-many relationship with PurchaseReceiptItem
        public List<PurchaseReceiptItem> PurchaseReceiptItems { get; set; } = new List<PurchaseReceiptItem>();
    }
}
