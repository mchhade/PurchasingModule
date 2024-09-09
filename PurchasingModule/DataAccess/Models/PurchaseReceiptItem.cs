using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingModule.DataAccess.Models
{
    public class PurchaseReceiptItem
    {
        public int Id { get; set; }

        // Foreign key to PurchaseReceipt
        public int PurchaseReceiptId { get; set; }
        public PurchaseReceipt PurchaseReceipt { get; set; }

        // Foreign key to PurchaseOrderItem
        public int PurchaseOrderItemId { get; set; }
        public PurchaseOrderItem PurchaseOrderItem { get; set; }

        // Received quantity for this particular item
        public int ReceivedQuantity { get; set; }
    }
}
