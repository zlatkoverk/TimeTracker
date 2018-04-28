﻿using Microsoft.EntityFrameworkCore;
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

    }
}