using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingModule.DataAccess.Response
{
    public class PurchaseReceiptItemResponse
    {
        public string PurchaseOrderItemName { get; set; }
        public int ReceivedQuantity { get; set; }
        public int OriginalQauntity { get; set; }
        public int RemainQuantity => OriginalQauntity - ReceivedQuantity;
    }
}
