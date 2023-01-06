using WebAPI_Levinci.Models;
using WebAPI_Levinci.Models.Interface;

namespace WebAPI_Levinci.Repository
{
    public class UserRepository : IUserRepository
    {
        private LevinciContext _context;

        public UserRepository()
        {
            _context = new LevinciContext();
        }

        public List<Users> GetAll()
        {
            return _context.users.ToList();
        }
    }
}
