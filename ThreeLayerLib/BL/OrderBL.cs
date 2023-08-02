using DAL;
using Persistence;

namespace BL
{
    public class OrderBL
    {
        OrderDAL orderDAL = new OrderDAL();
        public Order GetOrderByID(int orderId)
        {
            return orderDAL.GetOrderByID(orderId);
        }
        public bool SaveOrder(Order order)
        {
            bool result = orderDAL.CreateOrder(order);
            return result;
        }
    }

}