using DAL;
using Persistence;

namespace BL
{
    public class StaffBL
    {
        private StaffBL sDAL = new StaffDAL();
        public Customer? GetById(int customerId)
        {
            return sDAL.GetById(customerId);
        }

        public int AddCustomer(Customer customer)
        {
            return sDAL.AddCustomer(customer);
        }

        public static implicit operator StaffBL(StaffDAL v)
        {
            throw new NotImplementedException();
        }
    }
}
