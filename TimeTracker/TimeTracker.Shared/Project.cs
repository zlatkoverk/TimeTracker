using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public IList<TimeInterval> WorkIntervals { get; set; }
        public bool Active { get; set; }
        public Project() { }
        public Project(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Active = true;
            WorkIntervals = new List<TimeInterval>();
        }
    }
}
