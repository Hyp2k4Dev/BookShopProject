using System.Collections.Generic;
using DAL;
using Persistence;

namespace BL
{
    public class CustomerBL
    {
        CustomerDAL customerDAL = new CustomerDAL();
        public Customer GetCustomerById(int customerId)
        {
            return customerDAL.GetCustomerById(customerId);
        }

        public int AddCustomer(Customer customer)
        {
            return customerDAL.AddCustomer(customer);
        }
    }
}