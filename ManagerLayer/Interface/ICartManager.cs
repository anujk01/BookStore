using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface ICartManager
    {
        Task<CartModel> AddToCart(CartModel addToCart);
        Task<bool> DeleteCart(CartModel deleteCart);
        Task<CartModel> UpdateCart(CartModel updateCart);
        IEnumerable<CartModel> GetAllCart();
    }
}
