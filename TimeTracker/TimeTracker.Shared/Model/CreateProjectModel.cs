using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared.Model
{
    public class CreateProjectModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid User { get; set; }

        public CreateProjectModel() { }
        public CreateProjectModel(string name, string description, Guid user)
        {
            Name = name;
            Description = description;
            User = user;
        }
    }
}
