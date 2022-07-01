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
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<OrderModel> Order;

        private readonly IConfiguration configuration;

        public OrderRepository(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            Order = database.GetCollection<OrderModel>("Order");
        }

        public async Task<OrderModel> OrderItem(OrderModel orderItem)
        {
            try
            {
                var ifExists = await this.Order.Find(x => x.OrderID == orderItem.OrderID).SingleOrDefaultAsync();
                if (ifExists == null)
                {
                    await this.Order.InsertOneAsync(orderItem);
                    return orderItem;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteOrder(OrderModel deleteOrder)
        {
            try
            {
                var ifExists = await this.Order.FindOneAndDeleteAsync(x => x.OrderID == deleteOrder.OrderID);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<OrderModel> GetAllOrder()
        {
            try
            {
                return Order.Find(FilterDefinition<OrderModel>.Empty).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
