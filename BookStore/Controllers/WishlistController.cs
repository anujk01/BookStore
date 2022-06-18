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
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistManager wishlist;

        public WishlistController(IWishlistManager wishlist)
        {
            this.wishlist = wishlist;
        }


        [HttpPost]
        [Route("addToWishlist")]

        public async Task<IActionResult> AddToWishlist([FromBody] WishlistModel addToWishlist)
        {
            try
            {
                var result = await this.wishlist.AddToWishlist(addToWishlist);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<WishlistModel> { Status = true, Message = "Added to Wishlist", Data = result });
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
        [Route("deleteWishlist")]
        public async Task<IActionResult> DeleteWishlist([FromBody] WishlistModel deleteWishlist)
        {
            try
            {
                bool result = await this.wishlist.DeleteWishlist(deleteWishlist);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<WishlistModel> { Status = true, Message = "Item Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<WishlistModel> { Status = false, Message = "Item Not Deleted" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("getAllWishlist")]
        public IEnumerable<WishlistModel> GetAllWishlist()
        {
            try
            {
                var result = this.wishlist.GetAllWishlist();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
