using ManagerLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager address;

        public AddressController(IAddressManager address)
        {
            this.address = address;
        }


        [HttpPost]
        [Route("addAddress")]

        public async Task<IActionResult> AddAddress([FromBody] AddressModel addAddress)
        {
            try
            {
                var result = await this.address.AddAddress(addAddress);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Added Succesfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Address Not Added" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPut]
        [Route("updateAddress")]

        public async Task<IActionResult> UpdateAddress([FromBody] AddressModel updateAddress)
        {
            try
            {
                var result = await this.address.UpdateAddress(updateAddress);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = "Address Updated Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Update Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("getAddress")]

        public IEnumerable<AddressModel> GetAddress()
        {
            try
            {
                var result = this.address.GetAddress();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
