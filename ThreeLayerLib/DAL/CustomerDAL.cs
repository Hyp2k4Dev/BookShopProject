using MySqlConnector;
using Persistence;

namespace DAL
{
    public class CustomerDAL
    {
        private string query;
        private MySqlConnection connection;

        public CustomerDAL()
        {
            connection = DbConfig.GetConnection();
            query = "";
        }
        public Customer? GetCustomerById(int customerID)
        {
            Customer? c = null;
            try
            {
                query = @"select customer_ID, customer_name, phoneNumber,
                        if null(address, '') as address
                        from Customers where customer_ID=" + customerID + ";";
                MySqlDataReader reader = (new MySqlCommand(query, connection)).ExecuteReader();
                if (reader.Read())
                {
                    c = GetCustomer(reader);
                }
                reader.Close();
            }
            catch { }
            return c;
        }
        internal Customer GetCustomer(MySqlDataReader reader)
        {
            Customer c = new Customer();
            c.CustomerId = reader.GetInt32("customer_ID");
            c.CustomerName = reader.GetString("customer_name");
            c.CustomerAddress = reader.GetString("customer_address");
            return c;
        }
        public int AddCustomer(Customer c)
        {
            int result = 0;
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand("sp_createCustomer", connection);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerName", c.CustomerName);
                cmd.Parameters["@customerName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@customerPhone", c.phoneNumber);
                cmd.Parameters["@customerPhone"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@customerAddress", c.CustomerAddress);
                cmd.Parameters["@customerAddress"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@customerId", MySqlDbType.Int32);
                cmd.Parameters["@customerID"].Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = (int?)cmd.Parameters["@customerId"].Value ?? 0;
            }
            catch { }
            finally
            {
                connection.Close();
            }
            return result;
        }
    }
}