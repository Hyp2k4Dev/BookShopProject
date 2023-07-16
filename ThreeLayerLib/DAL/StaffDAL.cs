using MySqlConnector;
using Persistence;

namespace DAL
{
    public class StaffDAL
    {
        private string query = "";
        private MySqlConnection connection = DbConfig.GetConnection();
        public Staff GetStaffAccount(string staffName)
        {
            Staff s = new Staff();
            try
            {
                query = @"select * from Staffs where staff_name=@staffname;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@staffname", staffName);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    s = GetStaff(reader);
                }
                reader.Close();
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
            s.UserName = reader.GetString("user_Name");
            s.Password = reader.GetString("Pass_word");
            return staff;
        }
    }
}
