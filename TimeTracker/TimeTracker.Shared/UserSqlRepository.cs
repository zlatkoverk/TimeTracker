using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TimeTracker.Shared
{
    public class UserSqlRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserSqlRepository(UserDbContext context)
        {
            _context = context;
        }

        public void RegisterUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool UserExists(string userName)
        {
            return _context.Users.Include(u => u.Credentials).FirstOrDefault(user => user.Credentials.Username == userName) != null;
        }

        public User GetUser(UserCredentials credentials)
        {
            return _context.Users.Include(u => u.Credentials)
                .FirstOrDefault(user => user.Credentials.Username == credentials.Username && user.Credentials.PasswordHash == credentials.PasswordHash);
        }
    }
}