using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserCredentials Credentials { get; set; }
        public IList<Project> Projects { get; set; }

        public User() { }

        public User(string name, string surname, string username, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Credentials = new UserCredentials(username, password);
            Projects = new List<Project>();
        }

        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            User other = (User)obj;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(User u1, User u2)
        {
            if (ReferenceEquals(u1, u2))
            {
                return true;
            }
            if (u1 is null || u2 is null)
            {
                return false;
            }
            return u1.Equals(u2);
        }

        public static bool operator !=(User u1, User u2)
        {
            return !(u1 == u2);
        }
    }
}
