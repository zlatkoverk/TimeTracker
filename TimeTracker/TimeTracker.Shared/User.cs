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

        public User() { }

        public User(string name, string surname, string username, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Credentials = new UserCredentials(username, password);
        }
    }
}
