using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared.Model
{
    public class ModifyProjectModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        public ModifyProjectModel() { }
        public ModifyProjectModel(Guid id, string name, string description, bool active)
        {
            Id = id;
            Name = name;
            Description = description;
            Active = active;
        }
    }
}
