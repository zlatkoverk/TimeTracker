using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared.Model
{
    public class CreateActivityModel
    {
        public Guid ProjectId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Description { get; set; }
        public CreateActivityModel() { }
        public CreateActivityModel(Guid projectId, DateTime startTime)
        {
            ProjectId = projectId;
            StartTime = startTime;
        }
        public CreateActivityModel(Guid projectId, DateTime startTime, DateTime endTime, string description)
        {
            ProjectId = projectId;
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
        }
    }
}
