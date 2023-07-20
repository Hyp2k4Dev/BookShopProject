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
                query = @"select customer_ID, customer_name, phone,
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
            c.CustomerAddress = reader.GetString("address");
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
                cmd.Parameters.AddWithValue("@customer_Name", c.CustomerName);
                cmd.Parameters["@customer_Name"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@Phone", c.phoneNumber);
                cmd.Parameters["@Phone"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@Address", c.CustomerAddress);
                cmd.Parameters["@Address"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@customer_ID", MySqlDbType.Int32);
                cmd.Parameters["@customer_ID"].Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = (int?)cmd.Parameters["@customer_ID"].Value ?? 0;
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