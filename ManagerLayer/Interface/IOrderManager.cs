using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IOrderManager
    {
        Task<OrderModel> OrderItem(OrderModel addItem);
        Task<bool> DeleteOrder(OrderModel deleteOrder);
        IEnumerable<OrderModel> GetAllOrder();
    }
}
