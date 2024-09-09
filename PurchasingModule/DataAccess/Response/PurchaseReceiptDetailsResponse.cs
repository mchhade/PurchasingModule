using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingModule.DataAccess.Response
{
    public class PurchaseReceiptDetailsResponse
    {
        public PurchaseReceiptDetailsResponse()
        {
            PurchaseReceiptItems = new();
        }
        public DateTime ReceiptDate { get; set; }
        public List<PurchaseReceiptItemResponse> PurchaseReceiptItems { get; set; }
    }
}
