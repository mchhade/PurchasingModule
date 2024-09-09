using Microsoft.AspNetCore.Mvc;
using PurchasingModule.Business.Interface;
using PurchasingModule.DataAccess.Models;
using PurchasingModule.DataAccess.Request;
using PurchasingModule.DataAccess.Response;

namespace PurchasingModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController:ControllerBase
    {
        private readonly IRepository<PurchaseOrder> _orderRepository;
        private readonly IRepository<PurchaseOrderItem> _orderItemRepository;
        private readonly IRepository<Supplier> _supplierRepository;
        public PurchaseOrdersController(IRepository<PurchaseOrder> orderRepository, IRepository<PurchaseOrderItem> orderItemRepository,IRepository<Supplier> supplierRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _supplierRepository = supplierRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(PurchaseOrderRequest request)
        {
            ResultSet<PurchaseOrderResponse> result = new();
            try
            {
                var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId);
                result.Results = new();
                if (supplier != null)
                {
                    PurchaseOrder order = new()
                    {
                        OrderDate = request.OrderDate,
                        Status = request.Status,
                        SupplierId = request.SupplierId,
                    };
                    var createdOrder = await _orderRepository.AddAsync(order);
                    result.Results.OrderDate = createdOrder.OrderDate;
                    result.Results.Status = createdOrder.Status;
                    result.Results.PurchaseOrderID = createdOrder.Id;
                    foreach (var item in request.PurchaseOrderItems)
                    {
                        PurchaseOrderItem purchaseOrderItem = new()
                        {
                            ItemName = item.ItemName,
                            PurchaseOrderId = createdOrder.Id,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                        };
                        var createdPurchaseOrderItem = await _orderItemRepository.AddAsync(purchaseOrderItem);

                        PurchaseOrderItemResponse purchaseOrderItemResponse = new()
                        {
                            Id = createdPurchaseOrderItem.Id,
                            ItemName = item.ItemName,
                            Quantity = createdPurchaseOrderItem.Quantity,
                            UnitPrice = createdPurchaseOrderItem.UnitPrice,
                        };
                        result.Results.PurchaseOrderItems.Add(purchaseOrderItemResponse);



                        result.Results.Supplier = supplier;
                    }
                    result.Results.Supplier = supplier;
                    result.Success = true;
                    result.Message = "Created Successfully";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Supplier does not exist";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error has occured " + ex.Message;
            }
            return Ok(result);
        }
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
        {
                ResultSet<PurchaseOrder> result = new();
                try
                {
                    var order = await _orderRepository.GetByIdAsync(id);
                    if (order == null){
                        result.Success=false;
                        result.Message = "Not Fount";
                    }
                    else
                    {
                        order.Status = status;
                        await _orderRepository.UpdateAsync(order);
                        result.Success = true;
                        result.Message = "Purchase Order Status Updated succefully";
                    }
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = "An error has occured " + ex.Message;
                }
                return Ok(result);
        }
    }
}
