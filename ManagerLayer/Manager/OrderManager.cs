using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Manager
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository manager;
        public OrderManager(IOrderRepository manager)
        {
            this.manager = manager;

        }


        public async Task<OrderModel> OrderItem(OrderModel addItem)
        {
            try
            {
                return await this.manager.OrderItem(addItem);
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
                return await this.manager.DeleteOrder(deleteOrder);
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
                return this.manager.GetAllOrder();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
