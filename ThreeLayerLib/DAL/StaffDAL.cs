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
            Staff staff = new Staff();
            try
            {
                query = @"select * from Staffs where staff_name=@staffname;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@staffname", staffName);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    staff = GetStaff(reader);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return staff;
        }
        public Staff GetStaff(MySqlDataReader reader)
        {
            Staff staff = new Staff();
            staff.StaffID = reader.GetInt32("staff_ID");
            staff.UserName = reader.GetString("user_Name");
            staff.Password = reader.GetString("Pass_word");
            return staff;
        }
    }
}
