using Microsoft.AspNetCore.Mvc;
using PurchasingModule.Business.Interface;
using PurchasingModule.DataAccess.Models;
using PurchasingModule.DataAccess.Request;
using PurchasingModule.DataAccess.Response;

namespace PurchasingModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IRepository<Supplier> _supplierRepository;
        public SuppliersController(IRepository<Supplier> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            ResultSet<List<Supplier>> result=new ResultSet<List<Supplier>>();
            try
            {
                result.Results = new List<Supplier>();
                var suppliers = await _supplierRepository.GetAllAsync();
                result.Results = suppliers.ToList();
                
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error has occured "+ex.Message;
            }
            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplier(int id)
        {
            ResultSet<Supplier> result = new ResultSet<Supplier>();
            try
            {
                result.Results = new Supplier();
                var supplier = await _supplierRepository.GetByIdAsync(id);
                if (supplier == null)
                {
                    result.Success=false;
                    result.Message = "Not Found";
                }
                else
                {
                    result.Success=true;
                    result.Results = supplier;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error has occured " + ex.Message;
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier(SupplierRequest request)
        {
            ResultSet<Supplier> result = new ResultSet<Supplier>();
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var suppliers = await _supplierRepository.GetAsync(s => s.Phone == request.Phone || s.Email == request.Email);
                if (suppliers == null && suppliers.Count() == 0)
                {
                    var supplier = new Supplier()
                    {
                        Name = request.Name,
                        Email = request.Email,
                        Address = request.Address,
                        Phone = request.Phone,
                    };
                    var createdSupplier = await _supplierRepository.AddAsync(supplier);
                    result.Results = createdSupplier;
                    result.Message = "Create Successfully";
                    result.Success = true;
                }
                else
                {
                    result.Message = "A User exist with a same phone number or email";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = "An error has occured " + ex.Message;
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, SupplierRequest request)
        {
            ResultSet<Supplier> result = new ResultSet<Supplier>();
            try
            {
                var supplier = await _supplierRepository.GetByIdAsync(id);
                if (supplier == null)
                {
                    result.Success = false;
                    result.Message = "Supplier Not Found";
                }
                else
                {
                    var suppliers = await _supplierRepository.GetAsync(s => (s.Phone == request.Phone || s.Email == request.Email) && s.Id == id);
                    if (suppliers == null && suppliers.Count() == 0)
                    {
                        supplier.Name = request.Name;
                        supplier.Email = request.Email;
                        supplier.Address = request.Address;
                        supplier.Phone = request.Phone;
                        await _supplierRepository.UpdateAsync(supplier);
                        result.Success = true;
                        result.Message = "Update Succesfully";
                    }
                    else
                    {
                        result.Message = "a User exist with a same phone number or email";
                        result.Success = false;
                    }
                }
                
               
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error has occured " + ex.Message;
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            ResultSet<object> result=new ResultSet<object>();
            try
            {
                var deleted = await _supplierRepository.DeleteAsync(id);
                if (!deleted)
                {
                    result.Success = false;
                    result.Message = "User Not Found";
                }
                else {
                    result.Success = false;
                    result.Message = "SupplierDeleted Succesufully";
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
