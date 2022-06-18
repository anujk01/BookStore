using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Manager
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepository manager;
        public CartManager(ICartRepository manager)
        {
            this.manager = manager;

        }

        public async Task<CartModel> AddToCart(CartModel addToCart)
        {
            try
            {
                return await this.manager.AddToCart(addToCart);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteCart(CartModel deleteCart)
        {
            try
            {
                return await this.manager.DeleteCart(deleteCart);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CartModel> UpdateCart(CartModel updateCart)
        {
            try
            {
                return await this.manager.UpdateCart(updateCart);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<CartModel> GetAllCart()
        {
            try
            {
                return this.manager.GetAllCart();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
