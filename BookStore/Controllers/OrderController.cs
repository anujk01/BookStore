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
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager order;

        public OrderController(IOrderManager order)
        {
            this.order = order;
        }


        [HttpPost]
        [Route("OrderItem")]

        public async Task<IActionResult> OrderItem([FromBody] OrderModel orderItem)
        {
            try
            {
                var result = await this.order.OrderItem(orderItem);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = "Order Placecd" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Order Not Placed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("deleteOrder")]
        public async Task<IActionResult> DeleteOrder(OrderModel deleteOrder)
        {
            try
            {
                bool result = await this.order.DeleteOrder(deleteOrder);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<OrderModel> { Status = true, Message = "Order Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<OrderModel> { Status = false, Message = "Order Not Deleted" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("getAllOrders")]

        public IEnumerable<OrderModel> GetAllOrder()
        {
            try
            {
                var result = this.order.GetAllOrder();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
