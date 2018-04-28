using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Shared.Model
{
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public RegisterModel() { }
        public RegisterModel(string username, string password, string name, string surname)
        {
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
        }
    }
}
