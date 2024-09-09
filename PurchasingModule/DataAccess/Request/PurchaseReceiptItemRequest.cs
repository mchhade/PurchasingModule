using PurchasingModule.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingModule.DataAccess.Request
{
    public class PurchaseReceiptItemRequest
    {
        public int Id { get; set; }
        public int PurchaseOrderItemId { get; set; }
        public int ReceivedQuantity { get; set; }
    }
}
