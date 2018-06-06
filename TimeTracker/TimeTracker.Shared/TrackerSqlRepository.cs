using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeTracker.Shared.Model;

namespace TimeTracker.Shared
{
    public class TrackerSqlRepository : ITrackerRepository
    {
        private readonly TrackerDbContext _context;

        public TrackerSqlRepository(TrackerDbContext context)
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
            return _context.Users.FirstOrDefault(user => user.Username == userName) != null;
        }

        public User GetUser(LoginModel credentials)
        {
            return _context.Users.FirstOrDefault(user => user.Username == credentials.Username && user.PasswordHash == HashUtil.GetHash(credentials.Password));
        }

        public IList<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(Guid id)
        {
            return _context.Users.Find(id);
        }

        public void AddProject(Project project, User user)
        {
            _context.Projects.Add(project);
            _context.Users.Include(u => u.Projects).FirstOrDefault(u => u.Id == user.Id).Projects.Add(project);

            _context.SaveChanges();
        }

        public IList<Project> GetProjects(Guid userId)
        {
            return _context.Users.Include(u => u.Projects).FirstOrDefault(user => user.Id == userId).Projects;
        }

        public Project GetProject(Guid id)
        {
            return _context.Projects.Include(p => p.Activities).FirstOrDefault(p => p.Id == id);
        }

        public void ModifyProject(Project project)
        {
            var p = _context.Projects.Find(project.Id);
            p.Name = project.Name;
            p.Description = project.Description;
            p.Active = project.Active;
            _context.SaveChanges();
        }

        public void AddActivity(Guid projectId, Activity activity)
        {
            var p = _context.Projects.Include(project => project.Activities).FirstOrDefault(project => project.Id == projectId);
            p.Activities.Add(activity);
            _context.SaveChanges();
        }

        public IList<Activity> GetActivities(Guid projectId)
        {
            var p = _context.Projects.Include(project => project.Activities).FirstOrDefault(project => project.Id == projectId);
            return p.Activities.ToList();
        }

        public void UpdateActivity(Activity activity)
        {
            var a = _context.Activities.SingleOrDefault(ac => ac.Id == activity.Id);
            a.Label = activity.Label;
            a.Description = activity.Description;
            a.Duration = activity.Duration;
            _context.SaveChanges();
        }

        public void RemoveActivity(Guid activity)
        {
            var a = _context.Activities.SingleOrDefault(ac => ac.Id == activity);
            _context.Activities.Remove(a);
            _context.SaveChanges();
        }
    }
}