using DAL;
using Persistence;

namespace BL
{
    public class StaffBL
    {
        private StaffBL staffDAL = new StaffDAL();

        public static implicit operator StaffBL(StaffDAL v)
        {
            return (new StaffBL());
        }
    }
}
