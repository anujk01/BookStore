using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IOrderRepository
    {
        Task<OrderModel> OrderItem(OrderModel orderItem);
        Task<bool> DeleteOrder(OrderModel deleteOrder);
        IEnumerable<OrderModel> GetAllOrder();
    }
}
