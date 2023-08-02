using DAL;
using Persistence;

namespace BL
{
    public class OrderBL
    {
        OrderDAL orderDAL = new OrderDAL();

        // public bool CreateOrder(Order order)
        // {
        //     bool result = orderDAL.CreateOrder(order);
        //     return result;
        // }
        public bool SaveOrder(Order order)
        {
            bool result = orderDAL.CreateOrder(order);
            return result;
        }
        public void AddBookToOrder(Book book)
        {
            Console.WriteLine("");
        }

        public Order GetCurrentOrder(Customer? customer)
        {
            throw new NotImplementedException();
        }

    }

}