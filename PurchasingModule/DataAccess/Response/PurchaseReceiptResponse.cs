using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingModule.DataAccess.Response
{
    public class PurchaseReceiptResponse
    {
        public PurchaseReceiptResponse()
        {
            PurchaseReceiptItemResponses = new List<PurchaseReceiptItemResponse>();
        }
        public int ReceiptId { get; set; }
        public int PurchaseOrderId { get; set; }
        public List<PurchaseReceiptItemResponse> PurchaseReceiptItemResponses { get; set; }
    }
}
