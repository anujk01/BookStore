using Microsoft.Extensions.Configuration;
using ModelLayer;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<CartModel> Cart;

        private readonly IConfiguration configuration;

        public CartRepository(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            Cart = database.GetCollection<CartModel>("Cart");
        }


        public async Task<CartModel> AddToCart(CartModel addToCart)
        {
            try
            {
                var result = await this.Cart.Find(x => x.CartID == addToCart.CartID).SingleOrDefaultAsync();
                if (result == null)
                {
                    await this.Cart.InsertOneAsync(addToCart);
                    return addToCart;
                }
                return null;
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
                var result = await this.Cart.FindOneAndDeleteAsync(x => x.BookID == deleteCart.BookID);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CartModel> UpdateCart(CartModel updateCart)
        {
            try
            {
                var result = await this.Cart.Find(x => x.CartID == updateCart.CartID).FirstOrDefaultAsync();
                if (result != null)
                {
                    await this.Cart.UpdateOneAsync(x => x.CartID == updateCart.CartID,
                       Builders<CartModel>.Update.Set(x => x.Quantity, updateCart.Quantity));
                    return result;
                }
                return null;
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
                return Cart.Find(FilterDefinition<CartModel>.Empty).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
