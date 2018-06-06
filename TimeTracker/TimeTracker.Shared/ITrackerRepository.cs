using System;
using System.Collections.Generic;
using System.Text;
using TimeTracker.Shared.Model;

namespace TimeTracker.Shared
{
    public interface ITrackerRepository
    {
        void RegisterUser(User user);
        User GetUser(LoginModel credentials);
        User GetUser(Guid id);
        bool UserExists(string userName);
        void AddProject(Project project, User user);
        IList<Project> GetProjects(Guid userId);
        IList<User> GetUsers();
        Project GetProject(Guid id);
        void ModifyProject(Project project);
        void AddActivity(Guid projectId, Activity activity);
        IList<Activity> GetActivities(Guid projectId);
        void UpdateActivity(Activity activity);
        void RemoveActivity(Guid activity);
    }
}
