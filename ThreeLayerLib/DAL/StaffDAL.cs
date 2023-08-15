using System.Text;
using MySqlConnector;
using Persistence;
using System.Security.Cryptography;


namespace DAL
{
    public class StaffDAL
    {
        private List<Staff> staffList = new List<Staff>();
        private string query = "";
        private MySqlConnection connection = DbConfig.GetConnection();
        public Staff GetStaffAccount(string userName)
        {
            Staff s = new Staff();
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                {
                    query = @"select * from Staffs where user_name=@user_name;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@user_name", userName);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        s = GetStaff(reader);
                    }
                    reader.Close();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return s;
        }
        public Staff GetStaff(MySqlDataReader reader)
        {
            Staff s = new Staff();
            s.StaffID = reader.GetInt32("staff_ID");
            s.StaffName = reader.GetString("staff_Name");
            s.UserName = reader.GetString("user_Name");
            s.Password = reader.GetString("pass_Word");
            s.StaffStatus = reader.GetInt32("staff_Status");
            return s;
        }
        public string CreateMD5(string input)
        {

            // Creates an instance of the default implementation of the MD5 hash algorithm.
            using (var md5Hash = MD5.Create())
            {
                // Byte array representation of source string
                var sourceBytes = Encoding.UTF8.GetBytes(input);

                // Generate hash value(Byte Array) for input data
                var hashBytes = md5Hash.ComputeHash(sourceBytes);

                // Convert hash byte array to string
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                return hash;
            }
        }
        public Staff? GetStaffById(int staffId)
        {
            return staffList.FirstOrDefault(staff => staff.StaffID! == staffId);
        }
    }
}
