using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared
{
    public interface ITrackerRepository
    {
        void RegisterUser(User user);
        User GetUser(UserCredentials credentials);
        User GetUser(Guid id);
        bool UserExists(string userName);
        void AddProject(Project project, User user);
        IList<Project> GetProjects(Guid userId);
    }
}
