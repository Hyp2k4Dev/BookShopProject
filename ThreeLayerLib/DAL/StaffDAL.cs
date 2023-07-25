using MySqlConnector;
using Persistence;

namespace DAL
{
    public class StaffDAL
    {
        private string query = "";
        private MySqlConnection connection = DbConfig.GetConnection();
        public Staff GetStaffAccount(string userName)
        {
            Staff s = new Staff();
            try
            {
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
    }
}
