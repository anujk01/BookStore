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
    public class CartController : ControllerBase
    {
        private readonly ICartManager cart;

        public CartController(ICartManager cart)
        {
            this.cart = cart;
        }


        [HttpPost]
        [Route("addToCart")]

        public async Task<IActionResult> AddToCart([FromBody] CartModel addToCart)
        {
            try
            {
                var result = await this.cart.AddToCart(addToCart);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = "Added to Cart", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Not Added" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("deleteCart")]
        public async Task<IActionResult> DeleteCart([FromBody] CartModel deleteCart)
        {
            try
            {
                bool result = await this.cart.DeleteCart(deleteCart);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = "Item Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<CartModel> { Status = false, Message = "Item Not Deleted" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("updateCart")]
        public async Task<IActionResult> UpdateCart([FromBody] CartModel updateCart)
        {
            try
            {
                var result = await this.cart.UpdateCart(updateCart);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = "Book Updated Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new  { Status = false, Message = "Update Failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("getAllCart")]
        public IEnumerable<CartModel> GetAllCart()
        {
            try
            {
                var result = this.cart.GetAllCart();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
