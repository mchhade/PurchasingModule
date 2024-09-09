using Microsoft.AspNetCore.Mvc;
using PurchasingModule.Business.Interface;
using PurchasingModule.DataAccess.Models;
using PurchasingModule.DataAccess.Request;
using PurchasingModule.DataAccess.Response;

namespace PurchasingModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseReceiptController : ControllerBase
    {

        private readonly IRepository<PurchaseReceipt> _purchaseReceiptrepository;
        private readonly IRepository<PurchaseReceiptItem> _purchaseReceiptItemrepository;
        private readonly IRepository<PurchaseOrderItem> _purchaseOrderItemrepository;
        public PurchaseReceiptController(IRepository<PurchaseReceipt> purchaseReceiptrepository, IRepository<PurchaseReceiptItem> purchaseReceiptItemrepository, IRepository<PurchaseOrderItem> purchaseOrderItemrepository)
        {
            _purchaseReceiptrepository = purchaseReceiptrepository;
            _purchaseReceiptItemrepository = purchaseReceiptItemrepository;
            _purchaseOrderItemrepository = purchaseOrderItemrepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePurchaseReceipt([FromBody] PurchaseReceiptRequest request)
        {
            ResultSet<PurchaseReceiptResponse> result = new();
            try
            {
                
                result.Results = new();
                result.Results.PurchaseOrderId = request.PurchaseOrderId;
                
                List<PurchaseOrderItem> purchaseOrdeItems = _purchaseOrderItemrepository.GetAsync(x => x.PurchaseOrderId == request.PurchaseOrderId).Result.ToList();
                List<PurchaseReceiptItem> PurchaseReceiptItems= new();
                foreach (var item in request.ReceiptItems)
                {
                    PurchaseOrderItem purchaseOrderItem=purchaseOrdeItems.Where(x=>x.Id == item.Id).FirstOrDefault();
                    if (purchaseOrderItem!=null)
                    {
                        if (item.ReceivedQuantity > purchaseOrderItem.Quantity)
                        {
                            result.Errors.Add("Received Quantity exceeded of Original Quantity for Purchase Order Item ID :"+ item.PurchaseOrderItemId);
                        }
                        else
                        {
                            PurchaseReceiptItems.Add(new()
                            {
                                PurchaseOrderItemId = purchaseOrderItem.Id,
                                ReceivedQuantity = item.ReceivedQuantity
                            });
                            result.Results.PurchaseReceiptItemResponses.Add(new PurchaseReceiptItemResponse()
                            {
                                OriginalQauntity= purchaseOrderItem.Quantity,
                                PurchaseOrderItemName= purchaseOrderItem.ItemName,
                                ReceivedQuantity=item.ReceivedQuantity
                            });
                        }
                    }
                    else
                    {
                        result.Errors.Add("Purchase Order Item ID " + item.PurchaseOrderItemId + " Not in Purchase Order");
                    }
                }
                if(result.Errors.Count ==0 && PurchaseReceiptItems.Count > 0)
                {
                    PurchaseReceipt receipt = new()
                    {
                        PurchaseOrderId = request.PurchaseOrderId,
                        ReceiptDate = request.ReceiptDate,
                    };
                    receipt = await _purchaseReceiptrepository.AddAsync(receipt);
                    result.Results.ReceiptId = receipt.Id;
                    foreach (var PurchaseReceiptItem in PurchaseReceiptItems)
                    {
                        PurchaseReceiptItem.PurchaseReceiptId = receipt.Id;
                        PurchaseOrderItem purchaseOrderItem =await _purchaseOrderItemrepository.GetByIdAsync(PurchaseReceiptItem.PurchaseOrderItemId);
                        purchaseOrderItem.Quantity -= PurchaseReceiptItem.ReceivedQuantity;
                        await _purchaseOrderItemrepository.UpdateAsync(purchaseOrderItem);
                        await _purchaseReceiptItemrepository.AddAsync(PurchaseReceiptItem);
                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = "";
                    result.Results = new();
                }
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = "An error has occured " + ex.Message;
            }
            return Ok(result);
        }
        public async Task<IActionResult> GetReceiptDetails(int ReceiptId)
        {
            ResultSet<PurchaseReceiptDetailsResponse> result = new();
            try
            {
                PurchaseReceipt receipt = await _purchaseReceiptrepository.GetByIdAsync(ReceiptId);
                if (receipt != null)
                {
                    result.Results = new();
                    result.Results.ReceiptDate= receipt.ReceiptDate;
                    List<PurchaseReceiptItem> purchaseReceiptItems= _purchaseReceiptItemrepository.GetAsync(x=>x.PurchaseReceiptId==receipt.Id).Result.ToList();
                    List<PurchaseOrderItem> purchaseOrderItems= _purchaseOrderItemrepository.GetAsync(x=>x.PurchaseOrderId==receipt.PurchaseOrderId).Result.ToList();
                    foreach (var item in purchaseOrderItems)
                    {
                        PurchaseReceiptItem purchaseReceiptItem= purchaseReceiptItems.Where(x=>x.PurchaseOrderItemId==item.PurchaseOrderId).FirstOrDefault();
                        result.Results.PurchaseReceiptItems.Add(new()
                        {
                            PurchaseOrderItemName = item.ItemName,
                            OriginalQauntity = item.Quantity,
                            ReceivedQuantity = purchaseReceiptItem != null && purchaseReceiptItem.Id>0? purchaseReceiptItem.ReceivedQuantity:0
                        });
                    }
                    
                }
                else
                {
                    result.Success = false;
                    result.Message = "Receipt Not Found";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error has occured " + ex.Message;
            }
            return Ok(new { ReceiptId = ReceiptId });
        }
    }
}
