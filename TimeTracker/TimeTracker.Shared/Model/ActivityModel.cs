using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared.Model
{
    public class ActivityModel
    {
        public Guid ProjectId { get; set; }
        public Guid? ActivityId { get; set; }
        public string Description { get; set; }
        public bool Running { get; set; }
        public TimeSpan Duration { get; set; }
        public string Label { get; set; }
        public ActivityModel() { }
        public ActivityModel(Guid projectId)
        {
            ProjectId = projectId;
            Duration = new TimeSpan(0);
        }
        public ActivityModel(Guid projectId, TimeSpan duration, string description)
        {
            ProjectId = projectId;
            Duration = duration;
            Description = description;
        }

        public void AddSecond()
        {
            Duration = Duration.Add(new TimeSpan(0,0,1));
        }

        public override string ToString()
        {
            return $"{ProjectId} {ActivityId} {Description} {Running} {Duration}";
        }
    }
}
