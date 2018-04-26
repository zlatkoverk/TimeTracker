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
    }
}
