using DAL;
using Persistence;

namespace BL
{
    public class OrderBL
    {
        OrderDAL orderDAL = new OrderDAL();
        public bool CreateOrder(Order order)
        {
            bool result = orderDAL.CreateOrder(order);
            return result;
        }
    }

}