using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string Label { get; set; }

        public Activity() { }
        public Activity(string description, TimeSpan duration, string label)
        {
            if(label==null)
            {
                label = "witoutLabel";
            }
            Id = Guid.NewGuid();
            Description = description;
            Duration = duration;
            Label = label;
        }
    }
}
