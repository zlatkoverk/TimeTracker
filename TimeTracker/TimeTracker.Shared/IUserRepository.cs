using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared
{
    public interface IUserRepository
    {
        void RegisterUser(User user);

        User GetUser(UserCredentials credentials);

        bool UserExists(string userName);
    }
}
