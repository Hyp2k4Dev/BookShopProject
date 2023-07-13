// using DAL;
using Persistence;

namespace BL
{
    public class StaffBL
    {
        private StaffBL cdal = new StaffBL();
        public Customer? GetById(int customerId)
        {
            return cdal.GetById(customerId);
        }

        public int AddCustomer(Customer customer)
        {
            return cdal.AddCustomer(customer);
        }
    }
}
