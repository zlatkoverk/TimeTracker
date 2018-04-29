using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public Activity() { }
        public Activity(string description, DateTime startTime, DateTime endTime)
        {
            Id = Guid.NewGuid();
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
